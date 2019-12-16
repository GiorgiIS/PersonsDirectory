using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc;

namespace TBCPersonsDirectory.Common.Api
{
    public class BaseAPiController : Controller
    {
        protected IActionResult TransformServiceErrorToHttpStatusCode(ServiceErrorMessage serviceErrorMessage)
        {
            if (serviceErrorMessage.Code == ErrorStatusCodes.NOT_FOUND)
            {
                return NotFound(serviceErrorMessage);
            }

            if (serviceErrorMessage.Code == ErrorStatusCodes.ALREADY_EXISTS)
            {
                return Conflict(serviceErrorMessage);
            }

            if (serviceErrorMessage.Code == ErrorStatusCodes.INVALID_VALUE)
            {
                return UnprocessableEntity(serviceErrorMessage);
            }

            return BadRequest(serviceErrorMessage);
        }
    }
}
