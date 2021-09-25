using System;

namespace GsaTest.Core.Model
{
    public class Pnl
    {
        public int StrategyId { get; set; }
        public DateTime Date { get; set; }
        public decimal? Amount { get; set; }
    }
}
