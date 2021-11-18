using CsvPnl.Database;
using System;
using System.Collections.Generic;
using System.Text;

namespace CsvPnl.Factory
{
    public static class DataFactory
    {
        public static IMyData Create(string type, DateTime date, decimal amount, Strategy strat)
        {
            switch (type)
            {
                case "pnl":
                    return new Pnl(date, amount, strat);
                case "capital":
                    return new Capital(date, amount, strat);
                default:
                    return null;
            }
        }
    }
}
