﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Entities
{
    public class wo_task_sub_file
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public int wo_id { get; set; }
        public int task_sub_id { get; set; }
        public int media_id { get; set; }
        public string file_type { get; set; }
        public string content_type { get; set; }
        public int upload_type { get; set; }
    }
}
