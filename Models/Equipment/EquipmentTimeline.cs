using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models.Equipment
{
    public class EquipmentTimeline
    {
        public Title titles { get; set; }
        [Required]
        public string date { get; set; }
        [Required]
        public string title { get; set; }
        [Required]
        public string content { get; set; }
    }
    public class Title
    {
        [Required]
        public string title { get; set; }       
    }
}
