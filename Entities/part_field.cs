﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Entities
{
    public class part_field
    {
        public int id { get; set; }
        public int part_id { get; set; }
        public string name { get; set; }
        public string field_value { get; set; }
        public string field_type { get; set; }
    }
}