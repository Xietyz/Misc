using System;
using System.Collections.Generic;
using System.Linq;
using GsaTest.Core;
using GsaTest.Core.Model;
using NUnit.Framework;

namespace GsaTest.Tests
{
    [TestFixture]
    public class CumulativePnlTests
    {

        [Test]
        public void CanAggregatePnlByDate()
        {
            var aggregator = new PnlAggregator(null);
            
            var result = aggregator.CumulativePnl(JustOnePnl(), Strategies()).ToList();

            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result.First().Region, Is.EqualTo("EU"));
            Assert.That(result.First().Date, Is.EqualTo("2018-01-04"));
            Assert.That(result.First().CumulativePnl, Is.EqualTo(4000));
        }

        [Test]
        public void CanAggregatePnlForRegionForOneDay()
        {
            var aggregator = new PnlAggregator(new InMemoryStore());

            var result = aggregator.CumulativePnl(PnlForOneDay(), Strategies()).ToList();

            Assert.That(result.Count, Is.EqualTo(2));
            Assert.That(result.First().Region, Is.EqualTo("EU"));
            Assert.That(result.First().Date, Is.EqualTo("2018-01-01"));
            Assert.That(result.First().CumulativePnl, Is.EqualTo(-6000));  // 1000 -7000

            var firstApPnl = result.Single(x => x.Date == "2018-01-01" && x.Region == "AP");
            Assert.That(firstApPnl.CumulativePnl, Is.EqualTo(3243));
        }

        [Test]
        public void WhenDatesDifferentButSameRegion_DifferentPnlDataPoints()
        {
            var aggregator = new PnlAggregator(null);

            var result = aggregator.CumulativePnl(PnlSameRegionDifferentDates(), Strategies()).ToList();

            Assert.That(result.Count, Is.EqualTo(2));
            Assert.That(result.Last().Region, Is.EqualTo("EU"));
            Assert.That(result.Last().Date, Is.EqualTo("2018-01-05"));

            Assert.That(result.First().Date, Is.EqualTo("2018-01-03"));
            Assert.That(result.First().CumulativePnl, Is.EqualTo(3000));
        }

        [Test]
        public void WhenNoDataReturnEmptyList()
        {
            var aggregator = new PnlAggregator(null);

            var result = aggregator.CumulativePnl(new Pnl[] { }, Strategies()).ToList();

            Assert.That(result.Count, Is.EqualTo(0));
        }

        [Test]
        public void WhenMultipleDaysSameStrategyPnlCumulative()
        {
            var aggregator = new PnlAggregator(null);

            var result = aggregator.CumulativePnl(PnlForOneStrategy(), Strategies()).ToList();

            Assert.That(result.Count, Is.EqualTo(4));
            Assert.That(result.Last().Date, Is.EqualTo("2018-01-04"));
            Assert.That(result.Last().CumulativePnl, Is.EqualTo(6500));
        }

        [Test]
        public void WhenMultipleDaysDifferentStrategiesPnlCumulative()
        {
            var aggregator = new PnlAggregator(null);

            var result = aggregator.CumulativePnl(PnlForAll(), Strategies()).ToList();

            Assert.That(result.Count, Is.EqualTo(5));
            Assert.That(result.Last().Region, Is.EqualTo("EU"));
            Assert.That(result.Last().Date, Is.EqualTo("2018-01-05"));
            Assert.That(result.Last().CumulativePnl, Is.EqualTo(-4000));
        }

        public IEnumerable<Strategy> Strategies()
        {
            yield return new Strategy() { StrategyId = 1, Region = "EU", StratName="Strategy1" };
            yield return new Strategy() { StrategyId = 2, Region = "EU", StratName = "Strategy2" };
            yield return new Strategy() { StrategyId = 3, Region = "AP", StratName = "Strategy3" };
        }

        public IEnumerable<Pnl> JustOnePnl()
        {
            yield return new Pnl() { Amount = 4000, Date = DateTime.Parse("2018-01-04"), StrategyId = 1 };
        }

        public IEnumerable<Pnl> PnlForOneStrategy()
        {
            yield return new Pnl() { Amount = 1000, Date = DateTime.Parse("2018-01-01"), StrategyId = 1 };
            yield return new Pnl() { Amount = 2000, Date = DateTime.Parse("2018-01-02"), StrategyId = 1 };
            yield return new Pnl() { Amount = -500, Date = DateTime.Parse("2018-01-03"), StrategyId = 1 };
            yield return new Pnl() { Amount = 4000, Date = DateTime.Parse("2018-01-04"), StrategyId = 1 };
        }

        public IEnumerable<Pnl> PnlForOneDay()
        {
            yield return new Pnl() { Amount = 1000, Date = DateTime.Parse("2018-01-01"), StrategyId = 1 };
            yield return new Pnl() { Amount = -7000, Date = DateTime.Parse("2018-01-01"), StrategyId = 2 };
            yield return new Pnl() { Amount = 3243, Date = DateTime.Parse("2018-01-01"), StrategyId = 3 };
        }

        public IEnumerable<Pnl> PnlForAll()
        {
            yield return new Pnl() { Amount = 1000, Date = DateTime.Parse("2018-01-01"), StrategyId = 1 };
            yield return new Pnl() { Amount = 2000, Date = DateTime.Parse("2018-01-02"), StrategyId = 1 };
            yield return new Pnl() { Amount = 3000, Date = DateTime.Parse("2018-01-03"), StrategyId = 1 };
            yield return new Pnl() { Amount = 4000, Date = DateTime.Parse("2018-01-04"), StrategyId = 1 };

            yield return new Pnl() { Amount = -7000, Date = DateTime.Parse("2018-01-01"), StrategyId = 2 };
            yield return new Pnl() { Amount = -10000, Date = DateTime.Parse("2018-01-02"), StrategyId = 2 };
            yield return new Pnl() { Amount = 3000, Date = DateTime.Parse("2018-01-05"), StrategyId = 2 };
        }

        public IEnumerable<Pnl> PnlSameRegionDifferentDates()
        {
            yield return new Pnl() { Amount = 3000, Date = DateTime.Parse("2018-01-03"), StrategyId = 1 };
            yield return new Pnl() { Amount = 2000, Date = DateTime.Parse("2018-01-05"), StrategyId = 2 };
        }
    }
}
