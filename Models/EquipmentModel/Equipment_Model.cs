using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models.EquipmentModel
{
    public class Equipment_Model
    {
        [Required]
        public int id { get; set; }
        [Required]
        public string equipmentName { get; set; }
        [Required]
        public int equipmentTypeId { get; set; }
        [Required]
        public string processName { get; set; }
        public string modelName { get; set; }
        public string modelNo { get; set; }
        public string manufacturer { get; set; }
        public string sales_contact_name { get; set; }
        public string sales_contact_no { get; set; }
        public string support_contact_name { get; set; }
        public string support_contact_no { get; set; }
        public string remarks { get; set; }
    }
}
