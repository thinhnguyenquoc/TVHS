using System;
using System.Collections.Generic;
using System.Linq;

namespace TVHS.ViewModels
{
    public partial class ViewModelProgram
    {
        public int Id { get; set; }
        public string ProgramCode { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public int? Price { get; set; }
        public string Note { get; set; }
        public string Duration { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public List<ViewModelQuantity> quantityList { get; set; } 
    }

    public class ViewModelQuantity
    {
        public int NoTimes {get;set;}
        public int quantity {get; set;}
    }
}
