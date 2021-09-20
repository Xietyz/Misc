using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CsvPnl
{
    public static class StrategyService
    {
        public static IEnumerable<string> PrintRegionCumulativePnl(string region, StrategyList list)
        {
            var stratsByRegion = list.List.Where(x => x.Region.Equals(region)).ToList();
            int amountOfPnls = stratsByRegion.First().Pnls.Count();
            string toReturn = "";
            for (int x = 0; x < amountOfPnls; x++)
            {
                // every date (pnl)
                decimal totalPnlOnDate = 0;
                foreach (StrategyPnl strat in stratsByRegion)
                {
                    // every strategy
                    totalPnlOnDate += strat.Pnls[x].Amount;
                }
                toReturn = "Date: " + stratsByRegion.First().Pnls[x].Date + ", cumulative Pnl: " + totalPnlOnDate;
                yield return toReturn;
            }
        }
        public static IEnumerable<string> PrintStrategyPnls(int strategyNumber, List<StrategyPnl> list)
        {
            if (strategyNumber > list.Count)
            {
                throw new Exception("Out of bounds");
            }
            foreach (Pnl pnl in list[strategyNumber - 1].Pnls)
            {
                Console.WriteLine(pnl.ToString());
                yield return pnl.ToString();
            }
        }
        public static IEnumerable<string> PrintStrategyCapitals(string strategies, StrategyList list)
        {
            string[] stratNameArray = strategies.Split(",");
            foreach (string stratName in stratNameArray)
            {
                // input
                foreach (StrategyPnl strategy in list.List)
                {
                    // strats
                    if (stratName.Equals(strategy.Strategy))
                    {
                        foreach (Capital cap in strategy.Capitals)
                        {
                            // get capitals
                            yield return strategy.Strategy + ": " + cap.ToString();
                        }
                    }
                }
            }
        }
    }
}
