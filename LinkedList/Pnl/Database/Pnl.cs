using CsvPnl.Factory;
using System;
using System.Collections.Generic;

#nullable disable

namespace CsvPnl.Database
{
    // id
    // amount
    // strat id
    public partial class Pnl : IMyData
    {
        public Pnl(DateTime date, decimal amount, Strategy strat)
        {
            Amount = amount;
            PnlDate = date;
            Strategy = strat;
            StrategyId = strat.Id;
        }
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public DateTime? PnlDate { get; set; }
        public int? StrategyId { get; set; }

        public virtual Strategy Strategy { get; set; }
        public override string ToString()
        {
            return "Date: " + PnlDate + " Value: " + Amount;
        }
    }
}
