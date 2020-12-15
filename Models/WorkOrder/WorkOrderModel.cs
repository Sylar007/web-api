using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models.WorkOrder
{
    public class WorkOrderModel
    {
        [Required]
        public int id { get; set; }
        [Required]
        public string wo_name { get; set; }
        [Required]
        public int wo_type_id { get; set; }
        [Required]
        public int equipment_id { get; set; }
        [Required]
        public int asignee_user_id { get; set; }
        [Required]
        public int wo_priority_id { get; set; }
        [Required]
        public int notification_id { get; set; }
        [Required]
        public string dt_start_planned { get; set; }
        [Required]
        public string dt_end_planned { get; set; }
        public string remarks { get; set; }
    }
}
