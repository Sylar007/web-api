using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Entities
{
    public class wo_part
    {
        public int id { get; set; }
        public int wo_id { get; set; }
        public int part_id { get; set; }
        public string wo_part_type { get; set; }
    }
}
