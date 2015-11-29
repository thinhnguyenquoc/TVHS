using System;
using System.Collections.Generic;
using System.Linq;

namespace TVHS.Entities
{
    public partial class Sale
    {
        public int Id { get; set; }
        public int ProductCode { get; set; }
        public int Quantity { get; set; }
        public System.DateTime Date { get; set; }
    }
}
