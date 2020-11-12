﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models.Tasks
{
    public class FileDownload
    {
        [Required]
        public string name { get; set; }

        [Required]
        public string fileType { get; set; }
        [Required]
        public string contentType { get; set; }
    }
}
