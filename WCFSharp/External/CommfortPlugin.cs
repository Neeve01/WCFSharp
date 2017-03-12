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

namespace WCFSharp.External
{
    public static class CommfortPlugin
    {
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        internal unsafe delegate void CommfortProcessDelegate(uint PluginID, uint FuncID, byte* OutBuffer, uint OutBufferSize);
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        internal unsafe delegate uint CommfortGetDataDelegate(uint PluginID, uint FuncID, byte* InBuffer, uint InBufferSize, byte* OutBuffer, uint OutBufferSize);

        internal static CommfortProcessDelegate CommfortProcess;
        internal static CommfortGetDataDelegate CommfortGetData;

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
                CommfortProcess = (CommfortProcessDelegate)Marshal.GetDelegateForFunctionPointer((IntPtr)Process, typeof(CommfortProcessDelegate));
                CommfortGetData = (CommfortGetDataDelegate)Marshal.GetDelegateForFunctionPointer((IntPtr)GetData, typeof(CommfortGetDataDelegate));

                PluginsHandler.Reload();

                // Create form
                Globals.ConfigForm = new ConfigForm();
                Globals.ConfigForm.HandleCreated += (object sender, EventArgs e) =>
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
            Globals.SaveConfig();

            if (Globals.ConfigForm.IsHandleCreated)
                Globals.ConfigForm.Close();

            Globals.ConfigForm.Dispose();

            GC.WaitForPendingFinalizers();
            GC.Collect();
        }

        unsafe public static void Process(uint ID, void* Buffer, uint BufferSize)
        {
            Commfort.ProcessIncomingData(ID, (IntPtr)Buffer, BufferSize);
        }

        public static void ShowOptions()
        {
            Globals.ConfigForm?.Show();
        }

        public static void ShowAbout() { }
    }
}
