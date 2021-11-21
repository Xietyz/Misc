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
            context = _dbContext;
        }

        [HttpGet]
        [Route("cumulative-pnl")]
        public IEnumerable<Pnl> Get()
        {
            
        }
    }
}
