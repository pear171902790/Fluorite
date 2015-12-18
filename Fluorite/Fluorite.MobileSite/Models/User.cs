﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fluorite.MobileSite.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }

    public class Seller
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}