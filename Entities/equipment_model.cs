using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Entities
{
    public partial class equipment_model
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public string name { get; set; }
        public int equipment_type_id { get; set; }
        public string process_name { get; set; }
        public string model_name { get; set; }
        public string model_no { get; set; }
        public string mfg_name { get; set; }
        public string sales_contact_name { get; set; }
        public string sales_contact_no { get; set; }
        public string support_contact_name { get; set; }
        public string support_contact_no { get; set; }
        public string remark { get; set; }
        public sbyte is_deleted { get; set; }
        public Nullable<System.DateTime> dt_deleted { get; set; }
        public Nullable<System.DateTime> dt_created { get; set; }
        public Nullable<System.DateTime> dt_modified { get; set; }
        public Nullable<int> created_by { get; set; }
        public Nullable<int> modified_by { get; set; }
        public Nullable<int> deleted_by { get; set; }
    }
}
