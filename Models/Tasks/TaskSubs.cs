using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models.Tasks
{
    [JsonObject(NamingStrategyType = typeof(LowercaseNamingStrategy))]
    public class TaskSubs
    {
        public string text { get; set; }
        public string value { get; set; }
        public bool Checked{ get; set; }
        public bool @disabled { get; set; }
        public List<children> children { get; set; }
    }
    [JsonObject(NamingStrategyType = typeof(LowercaseNamingStrategy))]
    public class children
    {
        public string text { get; set; }
        public List<value> value { get; set; }
        public bool Checked { get; set; }
        public bool @disabled { get; set; }
    }
    public class value
    {
        public string id { get; set; }
        public string url { get; set; }       
    }
    public class LowercaseNamingStrategy : NamingStrategy
    {
        protected override string ResolvePropertyName(string name)
        {
            return name.ToLowerInvariant();
        }
    }
}
