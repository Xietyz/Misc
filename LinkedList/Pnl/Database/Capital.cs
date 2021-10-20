using System;
using System.Collections.Generic;

#nullable disable

namespace CsvPnl.Database
{
    public partial class Capital
    {
        public Capital()
        {

        }
        public Capital(DateTime date, decimal amount, Strategy strat)
        {
            Amount = amount;
            CapitalDate = date;
            Strategy = strat;
            StrategyId = strat.Id;
        }
        public int Id { get; set; }
        public decimal? Amount { get; set; }
        public DateTime? CapitalDate { get; set; }
        public int? StrategyId { get; set; }

        public virtual Strategy Strategy { get; set; }
    }
}
