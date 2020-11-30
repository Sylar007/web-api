using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models.Equipment
{
    public class EquipmentModel
    {
        [Required]
        public int id { get; set; }
        [Required]
        public int model_id { get; set; }
        [Required]
        public int status_id { get; set; }
        [Required]
        public string serial_no { get; set; }
 
        public int manufacture_year { get; set; }
        [Required]
        public string acquisitionDate { get; set; }

        public string warrantyDate { get; set; }

        public string deliveryDate { get; set; }
        public string installationDate { get; set; }
        public string commissioningDate { get; set; }
        public string sales_name { get; set; }
        public string sales_no { get; set; }
        public string support_name { get; set; }
        public string support_no { get; set; }
    }
}
