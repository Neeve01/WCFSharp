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
using WCFSharp.GUI;
using Newtonsoft.Json;
using WCFSharp.Plugins;

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
                Commfort.pluginID = ThisPluginID;
                CFProcess = (CommfortProcess)Marshal.GetDelegateForFunctionPointer((IntPtr)Process, typeof(CommfortProcess));
                CFGetData = (CommfortGetData)Marshal.GetDelegateForFunctionPointer((IntPtr)GetData, typeof(CommfortGetData));

                PluginsHandler.Reload();

                // Create form
                GUIContainer.ConfigForm = new ConfigForm();
                GUIContainer.ConfigForm.HandleCreated += (sender, e) =>
                {
                    PluginsHandler.FillConfigWindow();
                };

                var oldcontext = SynchronizationContext.Current;
                var sync = new SynchronizationContext();
                SynchronizationContext.SetSynchronizationContext(sync);
                Commfort.Scheduler = TaskScheduler.FromCurrentSynchronizationContext();
                Commfort.TokenSource = new CancellationTokenSource();
                SynchronizationContext.SetSynchronizationContext(oldcontext);
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
            ConfigContainer.Save();
            GUIContainer.ConfigForm.Close();
            GUIContainer.ConfigForm.Dispose();
            GC.WaitForPendingFinalizers();
            GC.Collect();
        }

        unsafe public static void Process(uint ID, void* Buffer, uint BufferSize)
        {
            Commfort.ProcessIncomingData(ID, (IntPtr)Buffer, BufferSize);
        }

        public static void ShowOptions()
        {
            GUIContainer.ConfigForm?.Show();
        }

        public static void ShowAbout() { }
    }
}
