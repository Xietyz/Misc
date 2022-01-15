using CsvPnl.Database;
using System;
using System.Collections.Generic;
using System.Text;

namespace CsvPnl.Factory
{
    public enum FactoryDataType
    {
        Pnl,
        Capital
    }
    public static class DataFactory
    {
        public static IMyData Create(FactoryDataType type, DateTime date, decimal amount, Strategy strat)
        {
            switch (type)
            {
                case FactoryDataType.Pnl:
                    return new Pnl(date, amount, strat);
                case FactoryDataType.Capital:
                    return new Capital(date, amount, strat);
                default:
                    return null;
            }
        }
    }
}
