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
        // https://stackoverflow.com/questions/62411410/is-there-any-better-way-to-add-the-dbcontext-to-a-asp-core-mvc-controller
        private readonly ILogger<CsvPnlController> _logger;
        private readonly PnldbContext _dbContext;

        public CsvPnlController(ILogger<CsvPnlController> logger, PnldbContext context)
        {
            _logger = logger;
            _dbContext = context;
        }

        [HttpGet]
        [Route("cumulative-pnl/{region}/{date}")]
        public List<PnlReturn> Get(string region, string date)
        {
            var specifiedDate = DateTime.ParseExact(date, "yyyy-MM-dd", null);
            var strats = _dbContext.Strategies.Where(x => x.Region.Equals(region));
            var pnlEntries = _dbContext.Pnls.Where(x => x.StrategyId == 0).Where(x => x.PnlDate >= specifiedDate);
            var pnlReturnList = new List<PnlReturn>();

            foreach (var entry in pnlEntries)
            {
                var newPnl = new PnlReturn(region, entry.PnlDate);
                pnlReturnList.Add(newPnl);
            }
            foreach (var strategy in strats)
            {
                var pnls = _dbContext.Pnls.Where(x => x.StrategyId == strategy.Id).Where(x => x.PnlDate >= specifiedDate);
                foreach (var pnl in pnls)
                {
                    var current = pnlReturnList.Where(x => x._date == pnl.PnlDate);
                    current.First().cumulativePnl += pnl.Amount;
                }
            }
            return pnlReturnList.ToList();
        }
    }
    public class PnlReturn
    {
        public PnlReturn(string region, DateTime date)
        {
            _region = region;
            _date = date;
            cumulativePnl = 0;
        }
        public string _region { get; set; }
        public DateTime _date { get; set; }
        public decimal cumulativePnl { get; set; }
    }
}
