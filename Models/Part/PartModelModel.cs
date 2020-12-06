using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models.Part
{
    public class PartModelModel
    {
        public int id { get; set; }
        public string partName { get; set; }
        public string partCode { get; set; }
        public int partTypeId { get; set; }
        public string part_model { get; set; }
        public string partType { get; set; }
        public string modelName { get; set; }
        public string modelNo { get; set; }
        public string manufacture { get; set; }        
        public string remarks { get; set; }
        public string sales_contact_name { get; set; }
        public string sales_contact_no { get; set; }
        public string support_contact_name { get; set; }
        public string support_contact_no { get; set; }
    }
}
