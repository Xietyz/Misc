using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using WebAPI.Services;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api")]
    public class CsvPnlController : ControllerBase
    {
        private readonly ILogger<CsvPnlController> _logger;
        private readonly CsvPnlService _pnlService;

        public CsvPnlController(ILogger<CsvPnlController> logger, CsvPnlService pnlService)
        {
            _pnlService = pnlService;
            _logger = logger;
        }

        [HttpGet]
        [Route("monthly-capital/{strategies}")]
        public List<MonthlyReturn> GetMonthlyCapital(string strategies)
        {
            var monthlyReturnList = _pnlService.CalculateMonthlyCapital(strategies);
            return monthlyReturnList.ToList();
        }

        [HttpGet]
        [Route("cumulative-pnl/{region}/{date}")]
        public List<PnlReturn> GetCumulativePnl(string region, string date)
        {
            return _pnlService.CalculateCumulativePnl(region, date);
        }

        [HttpGet]
        [Route("compound-daily-returns/{strategy}")]
        public List<DailyCompoundReturn> GetCompoundDailyReturns(string strategy)
        {
            return _pnlService.CalculateCompoundDailyReturns(strategy);
        }
    }
}
