using CsvPnl.Database;
using CsvPnl.Factory;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CsvPnl
{
    public class StrategyReader
    {
        PnlDbService _service;
        public string CapitalDataFile = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + "/Data/capital.csv";
        public string PnlDataFile = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + "/Data/pnl.csv";
        public string RegionDataFile = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + "/Data/properties.csv";
        public StrategyReader(PnlDbService service)
        {
            _service = service;
        }
        public IEnumerable<StrategyPnl> InitStrategyList(List<string[]> data)
        {
            int counter = 0;
            foreach (string[] row in data.Skip(1))
            {
                StrategyPnl newStrat = new StrategyPnl(row[0]);
                newStrat.Id = counter;
                counter++;
                yield return newStrat;
            }
        }
        public List<string[]> ReadCsv(string file)
        {
            // return data of csv
            string[] csvRows = System.IO.File.ReadAllLines(file).ToArray();
            var rowsToReturn = new List<string[]>();
            foreach (var row in csvRows)
            {
                rowsToReturn.Add(row.Split(","));
            }
            return rowsToReturn;
        }
        public List<StrategyPnl> ReadCapital(List<string[]> data, List<StrategyPnl> list)
        {
            // returns list of capitals to fill in
            foreach (var row in data.Skip(1))
            {
                DateTime currentDate = DateTime.Parse(row[0]);
                for (int x = 1; x < row.Length; x++)
                {
                    var strat = list[x - 1];
                    var convertedStrat = _service.StrategyPnlToEntity(strat);
                    Capital newCap = (Capital) DataFactory.Create(FactoryDataType.Capital, currentDate, decimal.Parse(row[x]), convertedStrat);
                    strat.Capitals.Add(newCap);
                }
            }
            return list;
        }
        public List<StrategyPnl> ReadRegions(List<string[]> data, List<StrategyPnl> list)
        {
            foreach (var row in data.Skip(1))
            {
                var strat = row[0];
                var newRegion = row[1];
                list.First(x => x.Strategy.Equals(strat)).Region = newRegion;
            }
            return list;
        }
        public List<StrategyPnl> ReadPnls(List<string[]> data, List<StrategyPnl> list)
        {
            // populate strategy's pnls
            foreach (var row in data.Skip(1))
            {
                DateTime currentDate = DateTime.Parse(row[0]);
                for (int x = 1; x < row.Length; x++)
                {
                    var strat = list[x - 1];
                    var convertedStrat = _service.StrategyPnlToEntity(strat);
                    Pnl newPnl = (Pnl) DataFactory.Create(FactoryDataType.Pnl, currentDate, decimal.Parse(row[x]), convertedStrat);
                    strat.Pnls.Add(newPnl);
                }
            }
            return list;
        }
    }
}
