using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WCFSharp.Plugins;

namespace WCFSharp
{
    internal class Config
    {
        [JsonProperty(PropertyName = "assemblies")]
        public List<AssemblyEntry> Assemblies = new List<AssemblyEntry>();
    }

    internal static class ConfigContainer
    {
        public static Config Config;

        public static string ConfigPath;

        public static void Save()
        {
            File.WriteAllText(ConfigPath, JsonConvert.SerializeObject(Config, Formatting.Indented));
        }
    }
}
