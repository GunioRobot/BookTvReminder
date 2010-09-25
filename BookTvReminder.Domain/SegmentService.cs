using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Xml.Linq;
using BookTvReminder.Domain.Models;
using BookTvReminder.Domain.Parsers;
using BookTvReminder.Domain.Utility;
using HtmlAgilityPack;

namespace BookTvReminder.Domain
{
    public class SegmentService
    {
        private readonly ConfigurationManager configurationManager;
        private readonly SegmentParser segmentParser;
        private readonly HtmlLoader htmlLoader;

        public SegmentService()
        {
            configurationManager = new ConfigurationManager();
            segmentParser = new SegmentParser();
            htmlLoader = new HtmlLoader();
        }

        //protected string GetScheduleHtml()
        //{            
            //var html = new WebClient().DownloadString(@"http://www.booktv.org/Schedule.aspx");
            //File.WriteAllText(@"C:\booktv.txt", html);

            //var htmlFromFile = File.ReadAllText(@"C:\booktv.txt");

            //return htmlFromFile;
        //}

        public List<Segment> GetSegments()
        {
            var html = htmlLoader.LoadUrl(configurationManager.BookTvScheduleUrl);
            var doc = new HtmlDocument().LoadFromHtml(html);
            return segmentParser.ParseSegments(doc);
        }

        public SegmentDetail GetSegmentDetail(Segment segment)
        {
            var html = htmlLoader.LoadUrl(segment.SegmentDetailUrl);
            return segmentParser.GetSegmentDetail(new HtmlDocument().LoadFromHtml(html).DocumentNode);
        }

        [Obsolete]
        public void LoadSegmentDetails(IEnumerable<Segment> segments)
        {
            foreach (var segment in segments)
            {
                segment.SegmentDetail = GetSegmentDetail(segment);
            }
        }

    }
}
