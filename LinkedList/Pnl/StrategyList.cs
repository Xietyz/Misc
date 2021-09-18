using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CsvPnl
{
    public class StrategyList : List<StrategyPnl>
    {
        // dont store list, pass on as parameter (?) keep for now
        public string capitalDataFile = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + "/Data/capital.csv";
        public string pnlDataFile = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + "/Data/pnl.csv";
        public string regionDataFile = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + "/Data/properties.csv";

        public List<StrategyPnl> List;
        public StrategyList()
        {
            List = InitStrategyList(pnlDataFile).ToList();
        }
        public IEnumerable<StrategyPnl> InitStrategyList(string data)
        {
            //string[] columnHeaders = streamReader.ReadLine().Split(",");
            string[] columnHeaders = System.IO.File.ReadAllLines(data)[0].Split(",");
            foreach (string column in columnHeaders.Skip(1))
            {
                yield return new StrategyPnl(column);
            }
        }
        public void PopulateStrategyListRegions(string data)
        {
            // populate strat's regions
            // populate strategy's pnls
            string[] csvRows = System.IO.File.ReadAllLines(data).Skip(1).ToArray();
            for (int rowNumber = 0; rowNumber < csvRows.Length; rowNumber++)
            {
                var values = csvRows[rowNumber].Split(",");
                List[rowNumber].Region = values[1];
            }
        }
        public void PopulateStrategyListCapital(string data)
        {
            // populate strategy's Capital
            string[] csvRows = System.IO.File.ReadAllLines(data).Skip(1).ToArray();
            foreach (string row in csvRows)
            {
                var values = row.Split(",");
                DateTime currentDate = DateTime.Parse(values[0]);
                for (int x = 1; x < values.Length; x++)
                {
                    Capital newCap = new Capital(currentDate, decimal.Parse(values[x]));
                    List[x - 1].Capitals.Add(newCap);
                }
            }
        }
        public void PopulateStrategyListPnls(string data)
        {
            // populate strategy's pnls
            string[] csvRows = System.IO.File.ReadAllLines(data).Skip(1).ToArray();
            foreach (string row in csvRows)
            {
                var values = row.Split(",");
                DateTime currentDate = DateTime.Parse(values[0]);
                for (int x = 1; x < values.Length; x++)
                {
                    Pnl newPnl = new Pnl(currentDate, decimal.Parse(values[x]));
                    List[x - 1].Pnls.Add(newPnl);
                }
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