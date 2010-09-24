using System.Text.RegularExpressions;
using BookTvReminder.Domain.AWS;
using NUnit.Framework;

namespace BookTvReminder.Domain.Test
{
    /// <summary>
    /// Summary description for DomainTests
    /// </summary>
    [TestFixture]
    public class
      DomainTests
    {

        [Ignore]
        [Test]
        public void TestMethod1()
        {
            var s = new SegmentService().GetSegments();
            s.Count.ToString();
        }

        [Test]
        public void AwsTest()
        {
            ItemLookupSample.Test(new AwsKeyHelper());
        }
   
    }
}
