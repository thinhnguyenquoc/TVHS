using System;
using System.Collections.Generic;
using System.Linq;

namespace TVHS.Entities
{
    public partial class Program
    {
        public int Id { get; set; }
        public string ProgramCode { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public int? Price { get; set; }
        public string Note { get; set; }
        public string Duration { get; set; }
        public int ProductId { get; set; }
    }
}
