using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models.Tasks
{
    public class TaskSubs
    {
        public string text { get; set; }
        public string value { get; set; }
        public List<children> children { get; set; }
    }
    public class children
    {
        public string text { get; set; }
        public string value { get; set; }
    }
}
