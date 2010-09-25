using System.Collections.Generic;
using BookTvReminder.Domain.Models;
using BookTvReminder.Domain.Utility;
using HtmlAgilityPack;

namespace BookTvReminder.Domain.Parsers
{
    public class SegmentParser
    {

        private readonly ConfigurationManager configurationManager;
        private readonly SegmentDayParser dayParser;
        private readonly SegmentDurationParser durationParser;

        public SegmentParser()
        {
            configurationManager = new ConfigurationManager();
            dayParser = new SegmentDayParser();
            durationParser = new SegmentDurationParser();
        }

        public List<Segment> ParseSegments(HtmlDocument doc)
        {
            var segments = new List<Segment>();

            var bottom = doc.DocumentNode.SelectSingleNode("//div[@class='sectionBottom']");

            var days = bottom.SelectNodes("./div/div[@class='scheduleColumn']");

            foreach (var node in days)
            {
                //Skip any nodes without valid children
                if (node.ChildNodes.Count <= 2)
                {
                    continue;
                }

                string day = node.GetChildById("span", "lblHeading").InnerText;
                var segmentDivs = node.SelectNodes("./div[@class='programList']");

                foreach (var segmentDiv in segmentDivs)
                {
                    segments.Add(GetSegment(segmentDiv, day));
                }
            }

            return segments;
        }

        public List<Segment> ParseSegmentsByDay(HtmlDocument doc)
        {
            var segments = new List<Segment>();

            var bottom = doc.DocumentNode.SelectSingleNode("//div[@class='sectionBottom']");

            var days = bottom.SelectNodes("./div/div[@class='scheduleColumn']");

            foreach (var node in days)
            {
                //Skip any nodes without valid children
                if (node.ChildNodes.Count <= 2)
                {
                    continue;
                }

                string day = node.GetChildById("span", "lblHeading").InnerText;
                var segmentDivs = node.SelectNodes("./div[@class='programList']");

                foreach (var segmentDiv in segmentDivs)
                {
                    segments.Add(GetSegment(segmentDiv, day));
                }
            }

            return segments;
        }

        public Segment GetSegment(HtmlNode segmentDiv, string day)
        {
            var imgDiv = segmentDiv.SelectSingleNode("./div[@class='programListContentImgArea']");
            var txtDiv = segmentDiv.SelectSingleNode("./div[@class='programListContentTextArea scheduleColumnTextArea']");

            var anchor = txtDiv.GetChildById("hypProgramTxt");
            var authorNode = txtDiv.GetChildById("lblAuthor");
            var imageNode = imgDiv.SelectSingleNode(".//img");

            //TODO: Move url building (below) into UrlBuilder class

            var segment = new Segment
            {
                DayDescription = day,
                Title = anchor.DecodeHtml(),
                SegmentDetailUrl = configurationManager.BookTvDomain + anchor.GetAttributeValue("href", ""),
                Time = txtDiv.GetChildById("lblAirTime").DecodeHtml(),
                Duration = txtDiv.GetChildById("lblLength").DecodeHtml(),
                Author = (authorNode == null) ? "" : authorNode.DecodeHtml(),
                ImageUrl = configurationManager.BookTvDomain + imageNode.Attributes["src"].Value,
            };
                        
            segment.DurationInMinutes = durationParser.GetDurationInMinutes(segment.Duration);
            segment.Date = dayParser.Parse(segment.DayDescription);

            return segment;
        }

        public SegmentDetail GetSegmentDetail(HtmlNode segmentDetailNode)
        {
            var segmentDetail = new SegmentDetail();

            var mainSection = segmentDetailNode.GetChildById("contentAreaInnerMain");
            var seriesNode = mainSection.GetChildById("lblSeries");
            var titleNode = mainSection.GetChildById("lblTitle");
            var authorNamesNode = mainSection.GetChildById("lblAuthors").SelectSingleNode(".//h3");
            var descriptionNode = mainSection.GetChildById("lblDescription").SelectSingleNode(".//p");
            var authorsNode = mainSection.SelectSingleNode("./div[@class='sectionBottom']").SelectSingleNode("./blockquote");

            segmentDetail.Series = seriesNode.DecodeHtml();
            segmentDetail.Title = titleNode.DecodeHtml();
            segmentDetail.Description = descriptionNode != null ? descriptionNode.DecodeHtml() : "";
            segmentDetail.AuthorNames = authorNamesNode != null ? authorNamesNode.DecodeHtml() : "";
            segmentDetail.Authors = GetSegmentAuthors(authorsNode);

            return segmentDetail;
        }

        public IEnumerable<SegmentAuthor> GetSegmentAuthors(HtmlNode authorsNode)
        {
            var authors = new List<SegmentAuthor>();

            var nameNodes = authorsNode.SelectNodes("./h3");
            var descriptionNodes = authorsNode.SelectNodes("./p/p");
            var buyNodes = authorsNode.SelectNodes("./p/span");

            //Skip processing if no author nodes exist
            if (nameNodes == null || nameNodes.Count == 0)
            {
                return authors;
            }

            for (int idx = 0; idx < nameNodes.Count; idx++)
            {
                var author = new SegmentAuthor
                {
                    AuthorName = nameNodes.DecodeHtml(idx),
                    AuthorBio = descriptionNodes.DecodeHtml(idx),
                    LookupHtml = buyNodes.DecodeHtml(idx)
                };

                if (!string.IsNullOrEmpty(author.LookupHtml))
                {
                    const string ISBN_QUERY = "isbn=";
                    var isbnStart = author.LookupHtml.IndexOf(ISBN_QUERY) + ISBN_QUERY.Length;
                    var isbnEnd = author.LookupHtml.IndexOf("\"", isbnStart);

                    author.ISBN = author.LookupHtml.Substring(isbnStart, isbnEnd - isbnStart);
                }

                authors.Add(author);
            }

            return authors;
        }

    }
}
