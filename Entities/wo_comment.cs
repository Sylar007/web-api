using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Entities
{
    public class wo_comment
    {
        public int id { get; set; }
        public int wo_id { get; set; }
        public string comment { get; set; }
        public string comment_type { get; set; }
        public Nullable<System.DateTime> dt_created { get; set; }
        public Nullable<System.DateTime> dt_modified { get; set; }
        public Nullable<System.DateTime> dt_deleted { get; set; }
        public Nullable<int> created_by { get; set; }
        public Nullable<int> modified_by { get; set; }
        public Nullable<int> deleted_by { get; set; }
    }
}
