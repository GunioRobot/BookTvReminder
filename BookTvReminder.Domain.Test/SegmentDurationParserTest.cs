using System;
using NUnit.Framework;

namespace BookTvReminder.Domain.Test
{
    [TestFixture]
    public class SegmentDurationParserTest
    {

        [TestCase("Approx. 1 hr. 24 min.", Result = 84)]        
        [TestCase("Approx. 1 hr. 1 min.", Result = 61)]
        [TestCase("Approx. 1 hr. 0 min.", Result = 60)]
        [TestCase("Approx. 1 hr. ", Result = 60)]
        [TestCase("Approx. 1 hr.", Result = 60)]
        [TestCase("Approx. 10 hr. 59 min.", Result = 659)]
        [TestCase("Approx. 8 hr. 30 min.", Result = 510)]
        [TestCase("Approx. 15 hr. ", Result = 900)]
        [TestCase("Approx. 1 min. ", Result = 1)]
        [TestCase("Approx. 1 min.", Result = 1)]
        [TestCase("Approx. 1 min.", Result = 1)]
        [TestCase("Approx. 0 min. ", Result = 0)]
        public int DurationRegexTest(string duration)
        {
            return new SegmentDurationParser().GetDurationInMinutes(duration);
        }

        [TestCase("abcdef")]
        [TestCase("Approx. min. ")]
        [ExpectedException(typeof(ArgumentException))]
        public void DurationRegex_NonmatchingString_Test(string duration)
        {
            new SegmentDurationParser().GetDurationInMinutes(duration);
        }

    }
}
