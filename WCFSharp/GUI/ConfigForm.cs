using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WCFSharp.Plugins;

namespace WCFSharp.GUI
{
    internal partial class ConfigForm : Form
    {
        public ConfigForm()
        {
            InitializeComponent();
        }

        private void PluginsCheckboxes_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            /* if (e.NewValue == CheckState.Checked)
                PluginsHandler.Enable(PluginsCheckboxes.Items[e.Index] as AssemblyEntry);
            else if (e.NewValue == CheckState.Unchecked)
                PluginsHandler.Disable(PluginsCheckboxes.Items[e.Index] as AssemblyEntry);
            ConfigContainer.Save(); */
        }

        private void ReloadBtn_Click(object sender, EventArgs e)
        {
            PluginsHandler.Reload();
        }

        private void PluginsCheckboxes_AfterCheck(object sender, TreeViewEventArgs e)
        {
            var name = e.Node.Name;
            if (e.Action != TreeViewAction.ByMouse && e.Action != TreeViewAction.ByKeyboard)
                return;

            var assembly = ConfigContainer.Config.Assemblies.FirstOrDefault(x => name == x.Assembly.FullName);
            if (e.Node.Checked)
                PluginsHandler.Enable(assembly);
            else
                PluginsHandler.Disable(assembly);
            ConfigContainer.Save();
        }

        private void PluginsCheckboxes_BeforeCheck(object sender, TreeViewCancelEventArgs e)
        {

        }
    }
}
