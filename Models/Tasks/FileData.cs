using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models.Tasks
{
    public class FileData
    {
        [Required]
        public IFormFile file { get; set; }
        [Required]
        public int id { get; set; }
        
        public string fileType { get; set; }
    }
}
