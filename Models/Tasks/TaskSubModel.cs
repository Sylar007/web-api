using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models.Tasks
{
    public class TaskSubModel
    {
        [Required]
        public int equipmentid { get; set; }

        [Required]
        public int wotypeid { get; set; }
    }
}
