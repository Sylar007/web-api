using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace WebApi.Entities
{
    public class part_type
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public System.DateTime dt_created { get; set; }
        public Nullable<System.DateTime> dt_modified { get; set; }
        public int created_by { get; set; }
        public Nullable<int> modified_by { get; set; }
    }
}
