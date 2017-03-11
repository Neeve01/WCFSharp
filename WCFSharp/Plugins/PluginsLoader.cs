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
        public static void Enable(AssemblyEntry Entry)
        {
            Entry.Enabled = true;
            Commfort.DebugMessage($"Enabling assembly {Entry.Assembly.GetName().Name}");

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

        public static void Disable(AssemblyEntry Entry)
        {
            Entry.Enabled = false;
            Commfort.DebugMessage($"Disabling assembly {Entry.Assembly.GetName().Name}");

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
            ConfigContainer.ConfigPath = $"{Commfort.Client.GetSuggestedTempPath()}wcfsharp.json";

            if (File.Exists(ConfigContainer.ConfigPath))
                ConfigContainer.Config = JsonConvert.DeserializeObject<Config>(File.ReadAllText(ConfigContainer.ConfigPath));
            else
                ConfigContainer.Config = new Config();

            Commfort.Handlers = new List<object>();

            if (!Directory.Exists("NetPlugins"))
                Directory.CreateDirectory("NetPlugins");

            var files = Directory.GetFiles("NetPlugins", "*.dll", SearchOption.AllDirectories);
            foreach (var file in files)
            {
                Assembly assembly = null;
                var filename = Path.GetFileName(file);
                try
                {
                    Commfort.DebugMessage($"Loading assembly {filename}...");
                    assembly = Assembly.LoadFrom(file);

                    if (!ConfigContainer.Config.Assemblies.Any(x => x.Name == assembly.FullName))
                    {
                        ConfigContainer.Config.Assemblies.Add(new AssemblyEntry()
                        {
                            Enabled = false,
                            Loaded = false,
                            Name = assembly.FullName
                        });
                    }

                    var entry = ConfigContainer.Config.Assemblies.FirstOrDefault(x => x.Name == assembly.FullName);
                    if (entry.Loaded)
                        throw new Exception($"{assembly.FullName} is already loaded! Possible duplicate?");

                    entry.Loaded = true;
                    entry.Assembly = assembly;

                    if (entry.Enabled)
                        Enable(entry);
                }
                catch (Exception E)
                {
                    Commfort.DebugMessage($"Couldn't load assembly {Path.GetFileName(file)}!\n{E.Message}");
                    if (assembly != null)
                        ConfigContainer.Config.Assemblies.RemoveAll(x => x.Name == assembly.FullName);
                }
            }
            ConfigContainer.Save();

            FillConfigWindow();
        }

        public static void FillConfigWindow()
        {
            if (GUIContainer.ConfigForm != null && GUIContainer.ConfigForm.IsHandleCreated)
            {
                var cboxes = GUI.GUIContainer.ConfigForm.PluginsCheckboxes;
                cboxes.BeginInvoke(new Action(() =>
                {
                    try
                    {
                        cboxes.Nodes.Clear();
                        foreach (var entry in ConfigContainer.Config.Assemblies)
                        {
                            var node = cboxes.Nodes.Add(entry.Assembly.FullName, entry.Assembly.GetName().Name);
                            node.Checked = entry.Enabled;
                        }
                    } catch (Exception E)
                    {
                        Commfort.DebugMessage(E.Message);
                    }
                }));
            }
        }
    }
}
