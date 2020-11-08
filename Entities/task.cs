using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Entities
{
    public class task
    {
        public int id { get; set; }
        public string task_no { get; set; }
        public string name { get; set; }
        public int equipment_model_id { get; set; }
        public int wo_type_id { get; set; }
        public sbyte is_deleted { get; set; }
        public Nullable<System.DateTime> dt_created { get; set; }
        public Nullable<System.DateTime> dt_modified { get; set; }
        public Nullable<System.DateTime> dt_deleted { get; set; }
        public Nullable<int> created_by { get; set; }
        public Nullable<int> modified_by { get; set; }
        public Nullable<int> deleted_by { get; set; }
    }
}
