using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Entities
{
    public partial class equipment_part
    {
        public int id { get; set; }
        public int equipment_id { get; set; }
        public int part_id { get; set; }
    }
}
