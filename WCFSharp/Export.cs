using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Reflection;
using System.Windows.Forms;
using WCFSharp.Types;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using System.IO;

namespace WCFSharp
{
    public static class Export
    {
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        internal unsafe delegate void CommfortProcess(uint PluginID, uint FuncID, byte* OutBuffer, uint OutBufferSize);
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        internal unsafe delegate uint CommfortGetData(uint PluginID, uint FuncID, byte* InBuffer, uint InBufferSize, byte* OutBuffer, uint OutBufferSize);

        internal static CommfortProcess CFProcess;
        internal static CommfortGetData CFGetData;

        /* unsafe public static byte[] GetData(uint FuncID, byte[] Buffer = null)
        {
            IntPtr UnmanagedArray = IntPtr.Zero;
            if (Buffer != null)
            {
                UnmanagedArray = (IntPtr)Unmanaged.NewAndInit<byte>(Buffer.Length);
                Marshal.Copy(Buffer, 0, UnmanagedArray, Buffer.Length);
            }

            uint Length = CFGetData(PluginID, FuncID, null, 0, Buffer != null ? (byte*)UnmanagedArray : null, Buffer != null ? (uint)Buffer.Length : 0);       

            IntPtr ReturnArray = (IntPtr)Unmanaged.NewAndInit<byte>((int)Length);
            CFGetData(PluginID, FuncID, (byte*)ReturnArray, Length, Buffer != null ? (byte*)UnmanagedArray : null, Buffer != null ? (uint)Buffer.Length : 0);

            if (Buffer != null)
                Unmanaged.Free((void*)UnmanagedArray);

            byte[] Result = new byte[Length];
            Marshal.Copy(ReturnArray, Result, 0, (int)Length);

            return Result;
        } */

        unsafe public static uint GetData(uint ID, void* InBuffer, uint InBufferSize, void* OutBuffer, uint OutBufferSize)
        {
            int Offset = 0;

            switch (ID)
            {
                case 2800:
                    if (OutBufferSize == 0)
                        return 4;

                    StaticPointerWriter.WriteInteger((IntPtr)OutBuffer, ref Offset, 2);
                    return 4;
                case 2810:
                    string Name = "WCFSharp";
                    uint Size = (uint)Name.Length * 2 + 4;

                    if (OutBufferSize == 0)
                        return Size;

                    StaticPointerWriter.WriteString((IntPtr)OutBuffer, ref Offset, Name);
                    return Size;
            }

            return 0;
        }

        unsafe public static bool Start(uint ThisPluginID, void* Process, void* GetData)
        {
            try
            {
                var sync = new SynchronizationContext();

                Commfort.pluginID = ThisPluginID;
                CFProcess = (CommfortProcess)Marshal.GetDelegateForFunctionPointer((IntPtr)Process, typeof(CommfortProcess));
                CFGetData = (CommfortGetData)Marshal.GetDelegateForFunctionPointer((IntPtr)GetData, typeof(CommfortGetData));

                Commfort.Subscriptions = new List<Delegate>();
                Commfort.Handlers = new List<object>();

                var oldcontext = SynchronizationContext.Current;

                SynchronizationContext.SetSynchronizationContext(sync);
                Commfort.OutEventsScheduler = TaskScheduler.FromCurrentSynchronizationContext();
                Commfort.TokenSource = new CancellationTokenSource();
                SynchronizationContext.SetSynchronizationContext(oldcontext);

                if (!Directory.Exists("NetPlugins"))
                    Directory.CreateDirectory("NetPlugins");

                var files = Directory.GetFiles("NetPlugins", "*.dll", SearchOption.AllDirectories);
                foreach (var file in files)
                {
                    try
                    {
                        Commfort.DebugMessage($"Loading assembly {Path.GetFileName(file)}...");
                        var assembly = Assembly.LoadFrom(file);
                        var methods = assembly.GetTypes()
                                .SelectMany(t => t.GetMethods())
                                .Where(method => method.GetCustomAttributes(typeof(PluginStartAttribute), false).Length > 0)
                                .ToList();

                        foreach (var method in methods)
                        {
                            var del = (PluginStartDelegate)method.CreateDelegate(typeof(PluginStartDelegate));
                            del?.Invoke();
                        }
                    }
                    catch (Exception E)
                    {
                        Commfort.DebugMessage($"Couldn't load assembly {Path.GetFileName(file)}!\n{E.Message}");
                    }
                }
            }
            catch (Exception E)
            {
                MessageBox.Show(E.Message);
                return false;
            }
            return true;
        }

        public static void Stop()
        {
            Commfort.TokenSource.Cancel();
        }

        unsafe public static void Process(uint ID, void* Buffer, uint BufferSize)
        {
            Commfort.ProcessIncomingData(ID, (IntPtr)Buffer, BufferSize);
        }

        public static void ShowOptions() { }

        public static void ShowAbout() { }
    }
}
