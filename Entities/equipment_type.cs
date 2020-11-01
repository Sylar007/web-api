using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Entities
{
    public partial class equipment_type
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public Nullable<System.DateTime> dt_created { get; set; }
        public Nullable<System.DateTime> dt_modified { get; set; }
        public Nullable<int> created_by { get; set; }
        public Nullable<int> modified_by { get; set; }
    }
}
