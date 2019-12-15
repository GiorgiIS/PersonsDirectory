using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TBCPersonsDirectory.Services.Interfaces;
using TBCPersonsDirectory.Services.Models;

namespace TBCPersonsDirectory.Api.Controllers
{
    [Route("api/report/")]
    [ApiController]
    public class ReportController : Controller
    {
        private readonly IReportService _reportService;

        public ReportController(IReportService reportService)
        {
            _reportService = reportService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var resp = _reportService.GetPersonReport();

            return Ok(resp);
        }
    }
}
