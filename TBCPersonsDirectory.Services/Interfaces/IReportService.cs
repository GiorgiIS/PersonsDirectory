using System.Collections.Generic;
using TBCPersonsDirectory.Services.Models;

namespace TBCPersonsDirectory.Services.Interfaces
{
    public interface IReportService
    {
        PersonReportResponseModel GetPersonReport();
    }
}
