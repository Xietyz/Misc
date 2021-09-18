using System;
using System.Collections.Generic;
using System.Text;

namespace CsvPnl
{
    public static class StrategyService
    {
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
    }
}
