using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using GsaTest.Seeder;
using NUnit.Framework;

namespace GsaTest.Tests
{
    [TestFixture]
    public class CsvInputerTests
    {
        [Test]
        public void CanParseTextFile()
        {
            var applicationDirectory = new Uri(Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase)).LocalPath;
            var fileLoc = applicationDirectory + @"\Resources\properties.csv";

            var reader = new CsvInputer();
            var result = reader.ReadLines(fileLoc).ToList();
            Assert.That(result.Count, Is.EqualTo(16));
        }

        [Test]
        public void HasHeaderLine()
        {
            var applicationDirectory = new Uri(Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase)).LocalPath;
            var fileLoc = applicationDirectory + @"\Resources\properties.csv";

            var reader = new CsvInputer();
            var result = reader.ReadLines(fileLoc);
            Assert.That(result.First(), Is.EqualTo("StratName,Region"));
        }

        [Test]
        public void CanParseValidStrategies()
        {
            var lines = new[] { "Strategy1,AP", "Strategy2,EU" };

            var reader = new CsvInputer();
            var result = reader.ParseStrategies(lines).ToList();
            Assert.That(result.Count, Is.EqualTo(2));
            Assert.That(result.First().StratName, Is.EqualTo("Strategy1"));
            Assert.That(result[1].Region, Is.EqualTo("EU"));
        }

        [Test]
        public void CanParseValidCapital()
        {
             var reader = new CsvInputer();
            var result = reader.ParseCapital(ShortCapitalLines());
            Assert.That(result.Count, Is.EqualTo(2));
            var strat1 = result["Strategy1"];
            Assert.That(strat1.First().Amount, Is.EqualTo(120500000));
            var strat2 = result["Strategy1"];
            Assert.That(strat2[1].Date, Is.EqualTo(DateTime.Parse("2010-02-01")));
        }

        [Test]
        public void CanParseValidPnl()
        {
            var reader = new CsvInputer();
            var result = reader.ParsePnl(PnlLines());
            Assert.That(result.Count, Is.EqualTo(15));
            var strat1 = result["Strategy1"];
            Assert.That(strat1.First().Amount, Is.EqualTo(95045));
            var strat2 = result["Strategy15"];
            Assert.That(strat2[1].Date, Is.EqualTo(DateTime.Parse("2010-02-22")));
            Assert.That(strat2[1].Amount, Is.EqualTo(115264));
        }

        private static List<string> ShortCapitalLines()
        {
            return new List<string>() {
                "Date,Strategy1,Strategy2",
                "2010-01-01,120500000,118000000",
                "2010-02-01,199500000,124000000"
            };
        }

        private static List<string> PnlLines()
        {
            return new List<string>() {
                "Date,Strategy1,Strategy2,Strategy3,Strategy4,Strategy5,Strategy6,Strategy7,Strategy8,Strategy9,Strategy10,Strategy11,Strategy12,Strategy13,Strategy14,Strategy15",
                "2010-01-01,95045,501273,429834,-352913,-69905,-188487,-179959,242415,269510,587689,-283773,-144819,125358,236732,-26555",
                "2010-02-22,-1357,-205783,-98682,164888,259109,-59668,647376,371943,82839,-387025,313148,-73757,35300,-300587,115264"
            };
        }

    }
}
