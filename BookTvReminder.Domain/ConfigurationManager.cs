using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BookTvReminder.Domain
{
    public class ConfigurationManager
    {
        private const string bookTvDomain = @"http://www.booktv.org";
        private const string bookTvSchedulePage = @"Schedule.aspx";
        private const string bookTvScheduleUrl = bookTvDomain + @"/" + bookTvSchedulePage;

        public string BookTvDomain
        {
            get { return bookTvDomain; }
        }

        public string BookTvScheduleUrl
        {
            get { return bookTvScheduleUrl; }
        }
    }
}
