using CsvPnl.Database;
using System;
using System.Collections.Generic;
using System.Linq;

namespace WebAPI.Services
{
    public interface ICsvPnlService
    {
        public List<MonthlyReturn> CalculateMonthlyCapital(string strategies);
    }
    public class CsvPnlService
    {
        private readonly PnldbContext _dbContext;
        public CsvPnlService(PnldbContext context)
        {
            _dbContext = context;
        }
        public List<MonthlyReturn> CalculateMonthlyCapital(string strategies)
        {
            var capitals = _dbContext.Capitals;
            var monthlyReturnList = new List<MonthlyReturn>();
            string[] strats = strategies.Split(',');

            foreach (var strat in strats)
            {
                foreach (var capital in capitals)
                {
                    string stratName = capital.Strategy.StrategyName;
                    if (strat == stratName)
                    {
                        var toReturn = new MonthlyReturn(stratName, capital.CapitalDate, capital.Amount);
                        monthlyReturnList.Add(toReturn);
                    }
                }
            }
            return monthlyReturnList;
        }
        public List<PnlReturn> CalculateCumulativePnl(string region, string date)
        {
            var specifiedDate = DateTime.ParseExact(date, "yyyy-MM-dd", null);
            var pnlReturnList = new List<PnlReturn>();

            var groupedFilteredPnls = _dbContext.Pnls
                .Where(x => x.Strategy.Region.Equals(region))
                .Where(x => x.PnlDate >= specifiedDate)
                .AsEnumerable()
                .GroupBy(x => x.PnlDate);

            decimal cumulativePnl = 0;
            foreach (var group in groupedFilteredPnls)
            {
                var toReturn = new PnlReturn(group.Key);
                foreach (var pnl in group)
                {
                    cumulativePnl += pnl.Amount;
                }
                toReturn.DateAmount = cumulativePnl;
                pnlReturnList.Add(toReturn);
            }
            return pnlReturnList.ToList();
        }
        public List<DailyCompoundReturn> CalculateCompoundDailyReturns(string strategy)
        {
            var listToReturn = new List<DailyCompoundReturn>();
            int strategyToFind;
            if (int.TryParse(strategy.Last().ToString(), out strategyToFind))
            {
                var pnls = _dbContext.Pnls.Where(x => x.StrategyId.Equals(strategyToFind)).AsEnumerable().OrderBy(x => x.PnlDate).GroupBy(x => new { x.PnlDate.Year, x.PnlDate.Month });
                var groupedCaps = _dbContext.Capitals.Where(x => x.StrategyId.Equals(strategyToFind)).AsEnumerable().GroupBy(x => new { x.CapitalDate.Year, x.CapitalDate.Month });

                foreach (var pnlGroup in pnls)
                {
                    decimal cumulativePnl = 0;
                    decimal capital = groupedCaps.Where(x => x.Key.Month == pnlGroup.Key.Month)
                                        .First(x => x.Key.Year == pnlGroup.Key.Year)
                                        .First()
                                        .Amount;
                    foreach (var pnl in pnlGroup)
                    {
                        cumulativePnl += pnl.Amount;
                        var compound = cumulativePnl / capital;
                        var compoundToReturn = new DailyCompoundReturn(pnl.StrategyId.ToString(), pnl.PnlDate, compound);
                        listToReturn.Add(compoundToReturn);
                    }
                    cumulativePnl = 0;
                }
            }
            return listToReturn;
        }

    }
    public class MonthlyReturn
    {
        public MonthlyReturn(string strategy, DateTime date, decimal capital)
        {
            Strategy = strategy;
            Date = date;
            Capital = capital;
        }
        public string Strategy { get; set; }
        public DateTime Date { get; set; }
        public decimal Capital { get; set; }
    }
    public class PnlReturn
    {
        public PnlReturn(DateTime date)
        {
            Date = date;
            DateAmount = 0;
        }
        public DateTime Date { get; set; }
        public decimal DateAmount { get; set; }
    }
    public class DailyCompoundReturn
    {
        public DailyCompoundReturn(string strategy, DateTime date, decimal compoundReturn)
        {
            Strategy = "Strategy " + strategy;
            Date = date;
            CompoundReturn = compoundReturn;
        }
        public string Strategy { get; set; }
        public DateTime Date { get; set; }
        public decimal CompoundReturn { get; set; }
    }
}
