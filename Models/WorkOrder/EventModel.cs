using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Models.WorkOrder
{
    public class EventModel
    {
        [Required]
        public string id { get; set; }
        public string url { get; set; }
    }
}
