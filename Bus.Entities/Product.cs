﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TVHS.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ParentId { get; set; }
    }
}
