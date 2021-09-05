using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CsvPnl
{
    public class Program
    {
        static void Main(string[] args)
        {
            // strategy columns (strat name + all Pnls in them) = list in StrategyList (stratpnl)
            // Pnls have a date + value
            // var streamReader = File.OpenText("pnl.csv");
            //StrategyList = InitStrategyList(streamReader).ToList();
            //PopulateStrategyList(streamReader);
            //PrintStrategyPnls(15);
            //StrategyList StratList = new StrategyList();
            StrategyList stratList = new StrategyList();
            stratList.PopulateStrategyList(stratList.CsvReader);
        }
    }
}
