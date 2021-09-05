using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CsvPnl
{
    public class StrategyList : List<StrategyPnl>
    {
        public List<StrategyPnl> List;
        public StreamReader CsvReader;
        public StrategyList()
        {
            CsvReader = File.OpenText("pnl.csv");
            List = InitStrategyList(CsvReader).ToList();
        }
        public void PopulateStrategyList(StreamReader streamReader)
        {
            while (!streamReader.EndOfStream)
            {
                string currentLine = streamReader.ReadLine();
                var values = currentLine.Split(",");
                DateTime currentDate = DateTime.Parse(values[0]);
                for (int x = 1; x < values.Length; x++)
                {
                    Pnl newPnl = new Pnl(currentDate, decimal.Parse(values[x]));
                    List[x - 1].Pnls.Add(newPnl);
                }
            }
        }
        public IEnumerable<StrategyPnl> InitStrategyList(StreamReader streamReader)
        {
            string[] columnHeaders = streamReader.ReadLine().Split(",");
            foreach (string column in columnHeaders.Skip(1))
            {
                yield return new StrategyPnl(column);
            }
        }

        public IEnumerable<string> PrintStrategyPnls(int strategyNumber)
        {
            if (strategyNumber > List.Count())
            {
                throw new Exception("Out of bounds");
            }
            foreach (Pnl pnl in List[strategyNumber - 1].Pnls)
            {
                Console.WriteLine(pnl.ToString());
                yield return pnl.ToString();
            }
        }
    }
}