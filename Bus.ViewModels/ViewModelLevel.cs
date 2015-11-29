using System;
using System.Collections.Generic;
using System.Linq;

namespace TVHS.ViewModels
{
    public class ViewModelLevel 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Nullable<int> Min { get; set; }
        public Nullable<int> Max { get; set; }
    }
}
