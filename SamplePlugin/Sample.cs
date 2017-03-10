using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WCFSharp;
using WCFSharp.Types;

namespace SamplePlugin
{
    public static class Plugin
    {
        [PluginStart]
        public static void OnStart()
        {
            Commfort.Events.RegisterEventHandler(new CommfortEventHandler());
        }
    }
}
