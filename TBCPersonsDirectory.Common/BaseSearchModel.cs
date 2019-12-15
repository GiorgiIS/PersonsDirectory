using System;
using System.Collections.Generic;
using System.Text;

namespace TBCPersonsDirectory.Common
{
    public class BaseSearchModel<T> where T : IComparable
    {
        public T Id { get; set; }
        public DateTime? CreatedFrom { get; set; }
        public DateTime? CreatedTo { get; set; }
        public DateTime? UpdatedFrom { get; set; }
        public DateTime? UpdatedTo { get; set; }
    }
}
