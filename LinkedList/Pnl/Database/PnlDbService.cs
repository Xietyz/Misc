using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CsvPnl.Database
{
    public class PnlDbService
    {
        private readonly PnldbContext _dbcontext;
        public PnlDbService(PnldbContext context)
        {
            _dbcontext = context;
        }
        public Strategy StrategyPnlToEntity(StrategyPnl strat)
        {
            Strategy converted = new Strategy();
            converted.Region = strat.Region;
            converted.StrategyName = strat.Strategy;
            converted.Id = strat.Id;
            return converted;
        }
        public void StoreToDb(List<Pnl> pnls, List<Strategy> strats, List<Capital> capitals)
        {
            // change dbcontext to truncate table if it exists first
            int counter = 0;
            using (var dbcontext = new PnldbContext())
            {
               foreach (var strat in strats)
               {
                   dbcontext.Strategies.Add(strat);
                   counter++;
               };
               dbcontext.SaveChanges();
            }
            using (var dbcontext = new PnldbContext())
            {
                counter = 0;
                foreach (var pnl in pnls)
                {
                    pnl.Id = counter;
                    pnl.StrategyId = pnl.Strategy.Id;
                    pnl.Strategy = null;
                    dbcontext.Pnls.Add(pnl);
                    counter++;
                };
                dbcontext.SaveChanges();
            }

            using (var dbcontext = new PnldbContext())
            {
                counter = 0;
                foreach (var cap in capitals)
                {
                    cap.Id = counter;
                    cap.StrategyId = cap.Strategy.Id;
                    cap.Strategy = null;
                    dbcontext.Capitals.Add(cap);
                    counter++;
                };
                dbcontext.SaveChanges();
            }
        }
    }
}
