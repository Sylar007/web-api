using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Entities
{
    public partial class equipment_model_link
    {
        public int id { get; set; }
        public int equipment_model_id { get; set; }
        public string link { get; set; }
        public string link_type { get; set; }
    }
}
