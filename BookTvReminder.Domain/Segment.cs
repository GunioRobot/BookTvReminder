using System.Collections.Generic;
using System.Linq;
using HtmlAgilityPack;

namespace BookTvReminder.Domain
{
    public class Segment
    {
        public string Title { get; set; }
        public string Author { get; set; }

        public string ImageUrl { get; set; }

        public string Day { get; set; }
        public string Time { get; set; }
        public string Duration { get; set; }
        public int DurationInMinutes { get; set; }

        public string SegmentDetailUrl { get; set; }
        public SegmentDetail SegmentDetail { get; set; }
    }

    public class SegmentDetail
    {
        public string Series { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public string AuthorNames { get; set; }
        public IEnumerable<SegmentAuthor> Authors { get; set; }
    }

    public class SegmentAuthor
    {
        public string AuthorName { get; set; }
        public string AuthorBio { get; set; }

        public string LookupHtml { get; set; }
        public string ISBN { get; set; }

        public string AmazonUrl { get; set; }
        public int AmazonRating { get; set; }
        public List<string> AmazonTags { get; set; }
    }
}