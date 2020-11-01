using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Entities
{
    public partial class equipment_model_part_model
    {
        public int id { get; set; }
        public int equipment_model_id { get; set; }
        public int part_model_id { get; set; }
    }
}
