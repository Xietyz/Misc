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
}
