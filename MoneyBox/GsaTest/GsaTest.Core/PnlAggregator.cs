using System;
using System.Collections.Generic;
using System.Linq;
using GsaTest.Core.Model;

namespace GsaTest.Core
{
    public interface IPnlAggregator
    {
        IEnumerable<CumPnl> CumulativePnl(string region, DateTime? startDate);
        IEnumerable<CumPnl> CumulativePnl(IEnumerable<Pnl> pnls, IEnumerable<Strategy> strategies);
    }

    public class PnlAggregator : IPnlAggregator
    {
        private readonly InMemoryStore _store;

        public PnlAggregator(InMemoryStore store)
        {
            _store = store;
        }

        public IEnumerable<CumPnl> CumulativePnl(string region, DateTime? startDate)
        {
            if (!"AP".Equals(region, StringComparison.CurrentCultureIgnoreCase) 
                    &&  !"US".Equals(region, StringComparison.CurrentCultureIgnoreCase))
                region = "EU";

            var strategies = _store.Strategy.Where(x => x.Region == region).ToList();
            var strategyIds = strategies.Select(x => x.StrategyId).ToList();
            var pnlQuery = _store.Pnl.Where(x => strategyIds.Contains(x.StrategyId));
            if (startDate != null)
                pnlQuery = pnlQuery.Where(x => x.Date >= startDate);
            var pnls = pnlQuery.ToList();

            return CumulativePnl(pnls, strategies);
        }

        // Date should be already filtered out
        public IEnumerable<CumPnl> CumulativePnl(IEnumerable<Pnl> pnls, IEnumerable<Strategy> strategies)
        {
            var stratLookup = strategies.ToDictionary(x => x.StrategyId, y => y.Region);

            var groupings = pnls
                .GroupBy(x => new { x.Date, Region = stratLookup[x.StrategyId] })
                .Select(x => new { x.Key.Region, x.Key.Date, DateSum = x.Sum(s => s.Amount).Value })
                .GroupBy(x => x.Region);

            foreach(var group in groupings)
            {
                var sum = 0m;
                foreach (var item in group)
                {
                    sum += item.DateSum;
                    yield return new CumPnl(item.Region, item.Date, sum);
                }
            }
        }
    }
}
