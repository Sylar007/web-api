using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Entities
{
    public class wo_file
    {
        public int id { get; set; }
        public int wo_id { get; set; }
        public int media_id { get; set; }
        public string file_type { get; set; }
    }
}
