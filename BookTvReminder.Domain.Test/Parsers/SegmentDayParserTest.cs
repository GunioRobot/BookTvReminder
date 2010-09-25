using System;
using BookTvReminder.Domain.Parsers;
using NUnit.Framework;

namespace BookTvReminder.Domain.Test.Parsers
{
    [TestFixture]
    public class SegmentDayParserTest
    {
        [Test]        
        public void ParseTest()
        {
            string datePattern;
            DateTime result;
            int currentYear = DateTime.Today.Year;
            var parser = new SegmentDayParser(2010);

            datePattern = "Monday, September 6th";
            result = parser.Parse(datePattern);
            Assert.AreEqual(new DateTime(currentYear, 9, 6), result);

            datePattern = "Tuesday, September 14th";
            result = parser.Parse(datePattern);
            Assert.AreEqual(new DateTime(currentYear, 9, 14), result);

            datePattern = "Wednesday, September 1st";
            result = parser.Parse(datePattern);
            Assert.AreEqual(new DateTime(currentYear, 9, 1), result);

            datePattern = "Thursday, September 23rd";
            result = parser.Parse(datePattern);
            Assert.AreEqual(new DateTime(currentYear, 9, 23), result);

            datePattern = "Friday, September 10th";
            result = parser.Parse(datePattern);
            Assert.AreEqual(new DateTime(currentYear, 9, 10), result);

            datePattern = "Saturday, September 25th";
            result = parser.Parse(datePattern);
            Assert.AreEqual(new DateTime(currentYear, 9, 25), result);

            datePattern = "Sunday, October 3rd";
            result = parser.Parse(datePattern);
            Assert.AreEqual(new DateTime(currentYear, 10, 3), result);

            datePattern = "Monday, August 2nd";
            result = parser.Parse(datePattern);
            Assert.AreEqual(new DateTime(currentYear, 8, 2), result);
        }

    }
}
