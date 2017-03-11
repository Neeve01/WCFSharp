using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using WCFSharp.Types;
using WCFSharp.Types.ClientEvents;

namespace WCFSharp
{
    public static partial class Commfort
    {
        internal static uint pluginID = 0;
        
        public static uint PluginID
        {
            get
            {
                return pluginID;
            }
        }

        public static bool IsServer
        {
            get
            {
                return false; // to do
            }
        }
        internal static List<Delegate> Subscriptions;
        internal static TaskScheduler OutEventsScheduler;
        internal static CancellationTokenSource TokenSource;
        internal static List<object> Handlers;

        unsafe internal static void Process(uint OutEvent, IntPtr Buffer, uint BufferSize)
        {
            Export.CFProcess(Commfort.PluginID, OutEvent, (byte*)Buffer, BufferSize); 
        }

        internal static void Process(Client.ProcedureType OutEvent, OutBuffer Buffer)
        {
            Process((uint)OutEvent, Buffer.MemoryPointer, (uint)Buffer.Size);
        }

        /// <summary>
        /// DONT FREAKING FORGET TO DEALLOCATE RETURNED INTPTR GOD DAMMIT.
        /// Also, there's no reason to use this.
        /// Note for myself.
        /// </summary>
        unsafe internal static IntPtr GetData(uint FuncID, OutBuffer Buffer, out uint Size)
        {
            Size = Export.CFGetData(PluginID, FuncID, null, 0, (byte*)((Buffer != null) ? Buffer.MemoryPointer : IntPtr.Zero), (uint)(Buffer != null ? Buffer.Size : 0));

            IntPtr dataPtr;
            if (Size != 0)
                dataPtr = Marshal.AllocHGlobal((int)Size);
            else
                throw new AccessViolationException();

            Export.CFGetData(PluginID, FuncID, (byte*)dataPtr, Size, (byte*)((Buffer != null) ? Buffer.MemoryPointer : IntPtr.Zero), (uint)(Buffer != null ? Buffer.Size : 0));

            return dataPtr;
        }

        /// <summary>
        /// DONT FREAKING FORGET TO DEALLOCATE RETURNED INTPTR GOD DAMMIT.
        /// </summary>
        internal static IntPtr GetData(Client.GetterType Type, OutBuffer Buffer, out uint Size)
        {
            return GetData((uint)Type, Buffer, out Size);
        }

        internal static void DebugMessage(object anything)
        {
            if (Commfort.IsServer)
            {
                throw new NotImplementedException();
            }
            else
            {
                Commfort.Client.PrintMessageToEvents($"[WCFSharp] {anything.ToString()}", WriteToHistory: false);
            }
        }

        internal static void ProcessIncomingData(uint ID, IntPtr DataBuffer, uint DataBufferSize)
        {
            int Position = 0;
            if (!Commfort.IsServer)
            {
                switch ((Commfort.Client.InEventType)ID)
                {
                    case Commfort.Client.InEventType.Message:
                        {
                            var Username = StaticPointerReader.ReadString(DataBuffer, ref Position);
                            var IP = StaticPointerReader.ReadString(DataBuffer, ref Position);
                            var Icon = (UserIcon)StaticPointerReader.ReadInteger(DataBuffer, ref Position);
                            var Channel = StaticPointerReader.ReadString(DataBuffer, ref Position);
                            var Type = (MessageType)StaticPointerReader.ReadInteger(DataBuffer, ref Position);
                            var Message = StaticPointerReader.ReadString(DataBuffer, ref Position);

                            if (Type == MessageType.Picture)
                            {
                                var Data = StaticPointerReader.ReadData(DataBuffer, ref Position);

                                using (var stream = new MemoryStream(Data))
                                {
                                    Image picture;
                                    picture = Image.FromStream(stream);

                                    Events.PushEvent(new PictureEvent(new User(Username, Icon, IP), new Channel(Channel), picture));
                                }
                            }
                            else
                            {
                                Events.PushEvent(new MessageEvent(new User(Username, Icon, IP), new Channel(Channel), Type, Message));
                            }
                        }
                        break;

                    case Commfort.Client.InEventType.ConnectionStatusChange:
                        {
                            var NewState = (ConnectionState)StaticPointerReader.ReadInteger(DataBuffer, ref Position);

                            Events.PushEvent(new ConnectionChangeEvent(NewState));
                        }
                        break;

                    case Commfort.Client.InEventType.UserConnected:
                        {
                            var Username = StaticPointerReader.ReadString(DataBuffer, ref Position);
                            var IP = StaticPointerReader.ReadString(DataBuffer, ref Position);
                            var Icon = (UserIcon)StaticPointerReader.ReadInteger(DataBuffer, ref Position);

                            Events.PushEvent(new UserConnectedEvent(new User(Username, Icon, IP)));
                        }
                        break;

                    case Commfort.Client.InEventType.UserDisconnected:
                        {
                            var Username = StaticPointerReader.ReadString(DataBuffer, ref Position);
                            var IP = StaticPointerReader.ReadString(DataBuffer, ref Position);
                            var Icon = (UserIcon)StaticPointerReader.ReadInteger(DataBuffer, ref Position);

                            Events.PushEvent(new UserDisconnectedEvent(new User(Username, Icon, IP)));
                        }
                        break;

                    case Commfort.Client.InEventType.LocalUserChannelJoin:
                        {
                            var Channel = StaticPointerReader.ReadString(DataBuffer, ref Position);

                            Events.PushEvent(new LocalUserChannelJoinEvent(new Channel(Channel)));
                        }
                        break;

                    case Commfort.Client.InEventType.LocalUserChannelLeave:
                        {
                            var Channel = StaticPointerReader.ReadString(DataBuffer, ref Position);
                            Events.PushEvent(new LocalUserChannelLeaveEvent(new Channel(Channel)));
                        }
                        break;

                    case Commfort.Client.InEventType.UserChannelJoin:
                        {
                            var Channel = StaticPointerReader.ReadString(DataBuffer, ref Position);
                            var Username = StaticPointerReader.ReadString(DataBuffer, ref Position);
                            var IP = StaticPointerReader.ReadString(DataBuffer, ref Position);
                            var Icon = (UserIcon)StaticPointerReader.ReadInteger(DataBuffer, ref Position);

                            Events.PushEvent(new UserChannelJoinEvent(new Channel(Channel), new User(Username, Icon, IP)));
                        }
                        break;

                    case Commfort.Client.InEventType.UserChannelLeave:
                        {
                            var Channel = StaticPointerReader.ReadString(DataBuffer, ref Position);
                            var Username = StaticPointerReader.ReadString(DataBuffer, ref Position);
                            var IP = StaticPointerReader.ReadString(DataBuffer, ref Position);
                            var Icon = (UserIcon)StaticPointerReader.ReadInteger(DataBuffer, ref Position);
                            Events.PushEvent(new UserChannelLeaveEvent(new Channel(Channel), new User(Username, Icon, IP)));
                        }
                        break;

                    case Commfort.Client.InEventType.ChannelTopicChange:
                        {
                            var Username = StaticPointerReader.ReadString(DataBuffer, ref Position);
                            var IP = StaticPointerReader.ReadString(DataBuffer, ref Position);
                            var Icon = (UserIcon)StaticPointerReader.ReadInteger(DataBuffer, ref Position);
                            var Channel = StaticPointerReader.ReadString(DataBuffer, ref Position);
                            var NewTopic = StaticPointerReader.ReadString(DataBuffer, ref Position);

                            Events.PushEvent(new ChannelTopicChangeEvent(new Channel(Channel), NewTopic, new User(Username, Icon, IP)));
                        }
                        break;
                }
            }
        }

        public static class Events
        {
            public static void RegisterEventHandler(object Handler)
            {
                if (!Handlers.Contains(Handler))
                    Handlers.Add(Handler);
            }

            public static void UnregisterEventHandler(object Handler)
            {
                Handlers.RemoveAll(x => x == Handler);
            }

            internal static void PushEvent(object Event)
            {
                Handlers.ForEach(handler =>
                {
                    var methods = handler.GetType()
                                   .GetMethods().Where
                                    (mi =>
                                        mi.ReturnType == typeof(Task)
                                        && mi.GetParameters()
                                             .Select(pi => pi.ParameterType)
                                             .SequenceEqual(new Type[] { Event.GetType() })
                                    ).ToList();
                    methods.ForEach(x => x?.Invoke(handler, new object[] { Event }));
                });
            }
        }
    }
}
