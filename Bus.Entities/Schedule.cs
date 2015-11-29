using System;
using System.Collections.Generic;
using System.Linq;

namespace TVHS.Entities
{
    public partial class Schedule
    {
        public int Id { get; set; }
        public string ProgramCode { get; set; }
        public System.DateTime Date { get; set; }
    }
}
