using System;
using System.Collections.Generic;
using System.Text;

namespace TBCPersonsDirectory.Core
{
    public class City : BaseEntity<int>
    {
        public City() : base() {}

        public City(int id, string name) : base()
        {
            Id = id;
            Name = name;
        }
        public string Name { get; set; }
    }
}
