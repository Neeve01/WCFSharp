using System;

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
    public delegate void PluginStopDelegate();
}
