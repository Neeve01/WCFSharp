using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WCFSharp.Types
{
    [AttributeUsage(AttributeTargets.Method, Inherited = false)]
    public class PluginStartAttribute : Attribute
    {
    }

    [AttributeUsage(AttributeTargets.Method, Inherited = false)]
    public class PluginStopAttribute : Attribute
    {
    }

    public delegate void PluginStartDelegate();
}
