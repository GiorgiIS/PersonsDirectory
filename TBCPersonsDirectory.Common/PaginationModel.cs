using System;
using System.Collections.Generic;
using System.Text;

namespace TBCPersonsDirectory.Common
{
    public class PaginationModel
    {
        public int TargetPage { get; set; } = 1;
        public int CountPerPage { get; set; } = 50;
    }
}
