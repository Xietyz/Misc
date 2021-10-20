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
            return converted;
        }
        public void StoreToDb(List<Pnl> pnls, List<Strategy> strats, List<Capital> capitals)
        {
            int counter = 0;
            //using (var dbcontext = new PnldbContext())
            //{
            //    //strats[0].Id = 10;
            //    //dbcontext.Strategies.Add(strats[0]);

            //    foreach (var strat in strats)
            //    {
            //        strat.Id = counter;
            //        dbcontext.Strategies.Add(strat);
            //        counter++;
            //    };
            //    dbcontext.SaveChanges();
            //}
            using (var dbcontext = new PnldbContext())
            {
                counter = 0;
                foreach (var pnl in pnls)
                {
                    pnl.Id = counter;
                    dbcontext.Pnls.Add(pnl);
                    counter++;
                };
            }

            using (var dbcontext = new PnldbContext())
            {
                counter = 0;
                foreach (var cap in capitals)
                {
                    cap.Id = counter;
                    dbcontext.Capitals.Add(cap);
                    counter++;
                };
            }

            _dbcontext.SaveChanges();
        }
    }
}
