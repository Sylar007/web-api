using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models.WorkOrder
{
    public class WOExecutionModel
    {
        [Required]
        public int id { get; set; }
        public string action { get; set; }
        public int status_id { get; set; }
        public int assign_user { get; set; }
        public string dateFinish { get; set; }
    }
}
