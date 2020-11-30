﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models.Tasks
{
    public class TaskSubs
    {
        public string text { get; set; }
        public string value { get; set; }
        public bool @checked { get; set; }
        public bool @disabled { get; set; }
        public List<children> children { get; set; }
    }
    public class children
    {
        public string text { get; set; }
        //public string value { get; set; }
        public string before { get; set; }
        public List<value> value { get; set; }
        public bool @checked { get; set; }
        public bool @disabled { get; set; }
    }
    public class value
    {
        public string id { get; set; }
        public string url { get; set; }       
    }
}
