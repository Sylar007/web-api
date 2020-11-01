using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Entities
{
    public class wo_task_check
    {
        public int id { get; set; }
        public int wo_id { get; set; }
        public int task_check_id { get; set; }
        public string wo_task_type { get; set; }
    }
}
