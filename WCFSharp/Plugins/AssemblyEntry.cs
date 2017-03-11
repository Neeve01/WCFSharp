using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace WCFSharp.Plugins
{
    internal class AssemblyEntry
    {
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "enabled")]
        public bool Enabled = false;

        [JsonIgnore]
        public Assembly Assembly = null;

        [JsonIgnore]
        public List<object> Handlers { get; set; } = new List<object>();

        [JsonIgnore]
        public bool Loaded = false;

        public override string ToString()
        {
            return Assembly.GetName().Name;
        }
    }
}
