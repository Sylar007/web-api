﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Entities
{
    public class qrcode_link
    {
        public int id { get; set; }
        public string qr_id { get; set; }
        public string qr_type { get; set; }
    }
}