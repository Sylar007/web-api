using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Entities
{
    public class current_nav_equipment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public int equipment_id { get; set; }
        public string depreciation_st_yrmth { get; set; }
        public string depreciation_ed_yrmth { get; set; }
        public int depreciation_year { get; set; }
        public decimal depreciation_value { get; set; }
        public decimal acc_depn_value { get; set; }
        public decimal current_nav { get; set; }
    }
}
