using NUnit.Framework;
using System;
using CsvPnl;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace PnlTests
{
    public class PnlTests1
    {
        [Test]
        public void PnlClassWorks()
        {
            Pnl testPnl = new Pnl(DateTime.Now, Convert.ToDecimal(123.12));
            string expected = "Date: " + DateTime.Now.ToShortDateString() + " Value: 123.12";

            Assert.AreEqual(expected, testPnl.ToString());
        }
        [Test]
        public void StrategyListCanInitialiseUsingCsv()
        {
            StrategyList stratList = new StrategyList();
            Assert.AreEqual(stratList.List[0].Strategy, "Strategy1");
        }
        [Test]
        public void StrategyListCanPopulateUsingCsv()
        {
            StrategyList stratList = new StrategyList();

            stratList.PopulateStrategyList(stratList.CsvReader);

            Assert.IsNotNull(stratList.List[1].Pnls);
        }
        [Test]
        public void StrategyListCanPrintPnlsForStrategy()
        {
            StrategyList stratList = new StrategyList();
            stratList.PopulateStrategyList(stratList.CsvReader);

            string actualString = stratList.PrintStrategyPnls(1).ToList()[1];
            string expectedString = "Date: 04/01/2010 Value: -140135";
            Assert.AreEqual(actualString, expectedString);
        }
        [Test]
        public void StrategyListDoesNotPrintOutOfBounds()
        {
            StrategyList stratList = new StrategyList();
            stratList.PopulateStrategyList(stratList.CsvReader);

            try
            {
                List<string> s = stratList.PrintStrategyPnls(200).ToList();
                Assert.Fail();
            }
            catch {}
            
        }
    }
}