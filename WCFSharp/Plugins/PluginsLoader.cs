using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WCFSharp.GUI;
using WCFSharp.Types;

namespace WCFSharp.Plugins
{
    internal static class PluginsHandler
    {
        public static void Enable(Plugin Entry)
        {
            Entry.Enabled = true;

            var methods = Entry.Assembly.GetTypes()
                                        .SelectMany(t => t.GetMethods())
                                        .Where(method => method.GetCustomAttributes(typeof(PluginStartAttribute), false).Length > 0)
                                        .ToList();

            methods.ForEach(method =>
            {
                var del = (PluginStartDelegate)method.CreateDelegate(typeof(PluginStartDelegate));
                del?.Invoke();
            });
        }

        public static void Disable(Plugin Entry)
        {
            Entry.Enabled = false;

            var methods = Entry.Assembly.GetTypes()
                                        .SelectMany(t => t.GetMethods())
                                        .Where(method => method.GetCustomAttributes(typeof(PluginStopDelegate), false).Length > 0)
                                        .ToList();

            methods.ForEach(method =>
            {
                var del = (PluginStopDelegate)method.CreateDelegate(typeof(PluginStopDelegate));
                del?.Invoke();
            });

            Entry.Handlers.ToList().ForEach(handler =>
            {
                Commfort.Events.UnregisterEventHandler(handler);
            });

            Entry.Handlers.Clear();
        }

        public static void Reload()
        {
            Globals.ConfigPath = Path.Combine(Commfort.Client.GetSuggestedTempPath(), "wcfsharp.json");

            if (File.Exists(Globals.ConfigPath))
                Globals.Config = JsonConvert.DeserializeObject<Config>(File.ReadAllText(Globals.ConfigPath));
            else
                Globals.Config = new Config();

            Commfort.Handlers = new List<object>();

            if (!Directory.Exists("NetPlugins"))
                Directory.CreateDirectory("NetPlugins");

            Directory.GetFiles("NetPlugins", "*.dll", SearchOption.AllDirectories).ToList().ForEach(file =>
            {
                Assembly assembly = null;
                Plugin plugin = null;
                try
                {
                    assembly = Assembly.LoadFrom(file);

                    if (!Globals.Config.Plugins.Any(x => x.Name == assembly.FullName))
                        Globals.Config.Plugins.Add(new Plugin(assembly.FullName));

                    plugin = Globals.Config.Plugins.FirstOrDefault(x => x.Name == assembly.FullName);
                    if (plugin.Loaded)
                        throw new Exception($"Assembly is already loaded. Possible duplicate?");

                    plugin.Assembly = assembly;

                    if (plugin.Enabled)
                        Enable(plugin);

                    plugin.Loaded = true;
                }
                catch
                {
                    if (assembly != null)
                    {
                        if (plugin != null)
                            Disable(plugin);
                        else
                            Globals.Config.Plugins.RemoveAll(x => x.Name == assembly.FullName);
                    }
                }
            });
            Globals.SaveConfig();

            FillConfigWindow();
        }

        public static void FillConfigWindow()
        {
            if (Globals.ConfigForm != null && Globals.ConfigForm.IsHandleCreated)
            {
                var cboxes = Globals.ConfigForm.PluginsCheckboxes;
                cboxes.BeginInvoke(new Action(() =>
                {
                    cboxes.Nodes.Clear();
                    foreach (var entry in Globals.Config.Plugins)
                    {
                        var node = cboxes.Nodes.Add(entry.Assembly.FullName, entry.Assembly.GetName().Name);
                        node.Checked = entry.Enabled;
                    }
                }));
            }
        }
    }
}
