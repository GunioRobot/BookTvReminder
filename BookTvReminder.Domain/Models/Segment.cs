using System;

namespace BookTvReminder.Domain.Models
{
    public class Segment
    {
        public string Title { get; set; }
        public string Author { get; set; }

        public string ImageUrl { get; set; }

        public string DayDescription { get; set; }
        public DateTime Date { get; set; }
        public string Time { get; set; }
        public string Duration { get; set; }
        public int DurationInMinutes { get; set; }

        public string SegmentDetailUrl { get; set; }
        public SegmentDetail SegmentDetail { get; set; }
    }
 
}