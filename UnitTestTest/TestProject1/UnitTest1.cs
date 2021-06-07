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
            var testEvent = new AnalyticEvents(0, IDEvent.Create);
            var data = new List<AnalyticEvents>();
            data.Add(new AnalyticEvents(0, IDEvent.Create));

            Assert.AreEqual(data[0].Event.ToString(), "Create");
            Assert.AreEqual(data[0].Id, testEvent.Id);
        }

        [Test]
        public void TestDataHasMoreThan3Events()
        {
            Assert.IsTrue(TestData.Basic.Count() > 3);
        }

        [Test]
        public void ThreeID0Events()
        {
            Assert.AreEqual(TestData.Basic.Count(x => x.Id == 0), 3);
        }

        [Test]
        public void GettingLastEventOfOneID()
        {
            Assert.AreEqual(TestData.Basic.Last(x => x.Id == 0).Event.ToString(), "Action");
        }
        [Test]
        public void GettingLastEventOfAllIDsInSequence()
        {
            var data = new Analytics().CalculateProgress(TestData.Basic);

            Assert.IsTrue(data.Select(x => x.Event.ToString())
                .SequenceEqual(new string[] { "Action", "End", "End", "Error" }));
        }

        [Test]
        public void ArrayToArrayTest()
        {
            IEnumerable<AnalyticEvents> test = new Analytics().CalculateProgress(TestData.Basic);
            Assert.IsTrue(test.First(x => x.Id == 0).Id == (new AnalyticEvents[] { TestData.Basic[4] }).First(x => x.Id == 0).Id);
        }

        [Test]
        public void GetLastOfOneIDProper()
        {
            var testData = new Analytics().GetLatestEvents(TestData.Basic);
            Assert.AreEqual(testData[0].Event, IDEvent.Action);
        }
    }
}