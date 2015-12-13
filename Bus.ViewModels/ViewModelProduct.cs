using System;
using System.Collections.Generic;
using System.Linq;

namespace TVHS.ViewModels
{
    public partial class ViewModelProduct
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ParentId { get; set; }
        public string ParentName { get; set; }
    }
}
