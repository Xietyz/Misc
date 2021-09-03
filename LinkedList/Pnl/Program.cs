using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Pnl
{
    class Program
    {
        static List<StrategyPnl> StrategyList;
        static void Main(string[] args)
        {
            // strategy columns (strat name + all Pnls in them) = list in StrategyList (stratpnl)
            // Pnls have a date + value
            var streamReader = File.OpenText("pnl.csv");
            StrategyList = InitStrategyList(streamReader).ToList();
            PopulateStrategyList(streamReader);
            PrintStrategyPnls(15);
        }

        private static void PopulateStrategyList(StreamReader streamReader)
        {
            while (!streamReader.EndOfStream)
            {
                string currentLine = streamReader.ReadLine();
                var values = currentLine.Split(",");
                DateTime currentDate = DateTime.Parse(values[0]);
                for (int x = 1; x < values.Length; x++)
                {
                    Pnl newPnl = new Pnl(currentDate, decimal.Parse(values[x]));
                    StrategyList[x - 1].Pnls.Add(newPnl);
                }
            }
        }
        private static IEnumerable<StrategyPnl> InitStrategyList(StreamReader streamReader)
        {
            string[] columnHeaders = streamReader.ReadLine().Split(",");
            foreach (string column in columnHeaders.Skip(1))
            {
                yield return new StrategyPnl(column);
            }
        }

        public static void PrintStrategyPnls(int strategyNumber)
        {
            if (strategyNumber > 15)
            {
                throw new Exception("Out of bounds");
            }
            foreach (Pnl pnl in StrategyList[strategyNumber - 1].Pnls)
            {
                Console.WriteLine(pnl.ToString());
            }
        }
    }
}
