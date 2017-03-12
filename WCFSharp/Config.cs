using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WCFSharp.Plugins;
using WCFSharp.GUI;

namespace WCFSharp
{
    internal class Config
    {
        [JsonProperty(PropertyName = "assemblies")]
        public List<Plugin> Plugins = new List<Plugin>();
    }

    internal static class Globals
    {
        public static ConfigForm ConfigForm = null;

        public static Config Config;
        public static string ConfigPath;

        public static void SaveConfig()
        {
            File.WriteAllText(ConfigPath, JsonConvert.SerializeObject(Config, Formatting.Indented));
        }
    }
}
