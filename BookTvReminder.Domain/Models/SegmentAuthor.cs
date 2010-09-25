using System.Collections.Generic;

namespace BookTvReminder.Domain.Models
{
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
