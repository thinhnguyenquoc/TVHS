using System;
using System.Collections.Generic;
using System.Linq;

namespace TVHS.Entities
{
    public partial class Cycle
    {
        public int Id { get; set; }
        public DateTime Begin { get; set; }
        public DateTime End { get; set; }
    }
}
