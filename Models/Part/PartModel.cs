using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models.Part
{
    public class PartModel
    {
        public int id { get; set; }
        public int model_id { get; set; }
        public string part_model { get; set; }
        public int mfg_year { get; set; }
        public string serial_no { get; set; }
        public string acquisition_date { get; set; }
        public string warranty_date { get; set; }
        public string installation_date { get; set; }
        public string certificate_no { get; set; }
        public string remarks { get; set; }
        public string certificate_date { get; set; }
        public string sales_contact_name { get; set; }
        public string sales_contact_no { get; set; }
        public string support_contact_name { get; set; }
        public string support_contact_no { get; set; }
    }
}
