using System.Collections.Generic;

namespace BookTvReminder.Domain.Models
{
    public class SegmentDetail
    {
        public string Series { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public string AuthorNames { get; set; }
        public IEnumerable<SegmentAuthor> Authors { get; set; }
    }
}
