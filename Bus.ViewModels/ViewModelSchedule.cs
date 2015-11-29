using System;
using System.Collections.Generic;
using System.Linq;

namespace TVHS.ViewModels
{
    public partial class ViewModelSchedule
    {
        public int Id { get; set; }
        public string ProgramCode { get; set; }
        public System.DateTime Date { get; set; }
    }
}
