using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Entities
{
    public partial class estimated_nav
    {
        public int id { get; set; }
        public int equipment_id { get; set; }
        public System.DateTime dt_installation { get; set; }
        public System.DateTime dt_operation { get; set; }
        public System.DateTime dt_purchase { get; set; }
        public System.DateTime dt_obsolete { get; set; }
        public decimal purchase_value { get; set; }
        public Nullable<System.DateTime> dt_invoice { get; set; }
        public string invoice_no { get; set; }
        public int is_deleted { get; set; }
        public Nullable<System.DateTime> dt_created { get; set; }
        public Nullable<System.DateTime> dt_modified { get; set; }
        public Nullable<int> created_by { get; set; }
        public Nullable<int> modified_by { get; set; }
    }
}
