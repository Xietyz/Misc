using System;
using System.Collections.Generic;
using System.Text;

namespace CsvPnl
{
    public class StrategyPnl
    {
        public StrategyPnl(string newStrategy)
        {
            Strategy = newStrategy;
            Pnls = new List<Pnl>();
        }
        public string Strategy { get; set; }
        public List<Pnl> Pnls { get; set; }
    }
}
