using CsvPnl.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api")]
    public class CsvPnlController : ControllerBase
    {
        private readonly ILogger<CsvPnlController> _logger;
        private readonly PnldbContext _dbContext;

        public CsvPnlController(ILogger<CsvPnlController> logger, PnldbContext context)
        {
            _logger = logger;
            _dbContext = context;
        }

        [HttpGet]
        [Route("monthly-capital/{strategies}")]
        public List<MonthlyReturn> GetMonthlyCapital(string strategies)
        {
            var monthlyReturnList = new List<MonthlyReturn>();
            var capitals = _dbContext.Capitals;
            string[] strats = strategies.Split(',');

            //var capitals = _dbContext.Capitals
            //    .Where(x => x.Strategy.StrategyName.Equals(strat))

            foreach (var strat in strats)
            {
                char[] split = strat.ToCharArray();
                int stratNum = (int)Char.GetNumericValue(split[8]) - 1;

                foreach (var capital in capitals)
                {
                    int? stratId = capital.StrategyId;
                    if (stratNum == stratId)
                    {
                        var toReturn = new MonthlyReturn(strat, capital.CapitalDate, capital.Amount);
                        monthlyReturnList.Add(toReturn);
                    }
                }
            }
            return monthlyReturnList.ToList();
        }

        [HttpGet]
        [Route("cumulative-pnl/{region}/{date}")]
        public List<PnlReturn> GetCumulativePnl(string region, string date)
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
        [HttpGet]
        [Route("compound-daily-returns/{strategy}")]
        public List<DailyCompoundReturn> GetCompoundDailyReturns(string strategy)
        {
            var listToReturn = new List<DailyCompoundReturn>();
            int strategyToFind;
            if (int.TryParse(strategy.Last().ToString(), out strategyToFind))
            {
                var pnls = _dbContext.Pnls.Where(x => x.StrategyId.Equals(strategyToFind)).AsEnumerable().GroupBy(x => new { x.PnlDate.Year, x.PnlDate.Month });
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
        [HttpGet]
        [Route("monthly-capital/{strategies}")]
        public List<MonthlyReturn> GetMonthlyCapital(string strategies)
        {
            var monthlyReturnList = new List<MonthlyReturn>();
            var capitals = _dbContext.Capitals;
            string[] strats = strategies.Split(',');

            //var capitals = _dbContext.Capitals
            //    .Where(x => x.Strategy.StrategyName.Equals(strat))

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
            return monthlyReturnList.ToList();
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
