using System;
using System.Collections.Generic;
using System.Linq;

namespace TVHS.ViewModels
{
    public partial class ViewModelSale
    {
        public int Id { get; set; }
        public string ProgramCode { get; set; }
        public int Quantity { get; set; }
        public System.DateTime Date { get; set; }
    }
}
