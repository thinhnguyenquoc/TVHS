using System;
using System.Collections.Generic;
using System.Linq;

namespace TVHS.Entities
{
    public class Level 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Nullable<int> Min { get; set; }
        public Nullable<int> Max { get; set; }
    }
}
