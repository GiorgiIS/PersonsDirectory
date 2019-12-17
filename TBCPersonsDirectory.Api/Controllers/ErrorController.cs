using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TBCPersonsDirectory.Common.Api;

namespace TBCPersonsDirectory.Api.Controllers
{
    [ApiController]
    [Route("Error")]
    public class ErrorController : BaseAPiController
    {
        public async Task Get()
        {
            throw new Exception("Exception to test logger");
        }
    }
}
