using System.Linq;
using GsaTest.Core;

namespace GsaTest.Seeder
{
    public class DatabaseSeeder
    {
        private string _inputFilesDirectory;
        private readonly IDataInputer dataInputer;
        private readonly InMemoryStore _store;

        public DatabaseSeeder(string inputFilesDirectory, IDataInputer dataInputer, InMemoryStore store)
        {
            this._inputFilesDirectory = inputFilesDirectory;
            this.dataInputer = dataInputer;
            _store = store;
        }

        public void SeedStrategies()
        {
            var strategies = dataInputer.ReadStrategies(_inputFilesDirectory + @"\properties.csv").ToList();
            var pnl = dataInputer.ReadPnl(_inputFilesDirectory + @"\pnl.csv");
            var capital = dataInputer.ReadCapital(_inputFilesDirectory + @"\capital.csv");

            foreach(var strategy in strategies)
            {
                if(capital.ContainsKey(strategy.StratName))
                    strategy.Capital = capital[strategy.StratName];

                if (pnl.ContainsKey(strategy.StratName))
                    strategy.Pnl = pnl[strategy.StratName];
            }

            _store.Strategy = strategies;
            _store.Pnl = strategies.SelectMany(x => x.Pnl).ToList();


        }
    }
}
