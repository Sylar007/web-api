using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Entities
{
    public partial class media
    {
        public int id { get; set; }
        public string file_name { get; set; }
        public Nullable<System.DateTime> dt_created { get; set; }
        public Nullable<int> created_by { get; set; }
    }
}
