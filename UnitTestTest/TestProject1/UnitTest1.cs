using ConsoleApp2;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace TestProject1
{
    public class Tests
    {
        [Test]
        public void AnalyticsEventsClassTest()
        {
            var testEvent = new AnalyticEvents(0, "start");
            var data = new List<AnalyticEvents>();
            data.Add(new AnalyticEvents(0, "start"));

            Assert.AreEqual(data[0].Event, "start");
            Assert.AreEqual(data[0].Id, testEvent.Id);
        }

        [Test]
        public void TestDataHasMoreThan3Events()
        {
            Assert.IsTrue(TestData.Basic.Count() > 3);
        }

        [Test]
        public void Three0Events()
        {
            Assert.AreEqual(TestData.Basic.Count(x => x.Id == 0), 3);
        }

        [Test]
        public void GettingLastEventOfOneID()
        {
            Assert.AreEqual(TestData.Basic.Last(x => x.Id == 0).Event, "action");
        }
        [Test]
        public void GettingLastEventOfAllIDsInSequence()
        {
            var data = Analytics.CalculateProgress(TestData.Basic);

            Assert.IsTrue(data.Select(x => x.Event)
                .SequenceEqual(new string[] { "action", "end", "end", "error" }));
        }

        [Test]
        public void ArrayToArrayTest()
        {
            IEnumerable<AnalyticEvents> test = Analytics.CalculateProgress(TestData.Basic);
            Assert.IsTrue(test.First(x => x.Id == 0).Id == (new AnalyticEvents[] { TestData.Basic[4] }).First(x => x.Id == 0).Id);
        }
    }
}