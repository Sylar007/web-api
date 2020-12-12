using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Entities
{
    public partial class equipment_model_file
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public int equipment_model_id { get; set; }
        public int media_id { get; set; }
        public string file_type { get; set; }

        public string content_type { get; set; }
    }
}
