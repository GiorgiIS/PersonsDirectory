using System;
using System.Collections.Generic;
using System.Text;

namespace TBCPersonsDirectory.Core
{
    public abstract class BaseEntity<K> where K : IComparable
    {
        public BaseEntity()
        {
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }

        public K Id { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
