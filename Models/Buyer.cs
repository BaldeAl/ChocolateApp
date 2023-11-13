﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Buyer
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string FirstName { get; set; }
        public string Address { get; set; }
        public int Telephone { get; set; }
    }
}
