using System;
using System.Collections.Generic;
using System.Text;

namespace CsvPnl.Factory
{
    public interface IMyData
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public int? StrategyId { get; set; }
    }
}
