using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Entities
{
    public class wo_link
    {
        public int id { get; set; }
        public int wo_id { get; set; }
        public string link { get; set; }
        public string link_type { get; set; }
    }
}
