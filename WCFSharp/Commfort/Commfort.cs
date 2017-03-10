using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WCFSharp.Types;
using WCFSharp.Types.ClientEvents;

namespace WCFSharp
{
    public static partial class Commfort
    {
        public static readonly bool IsServer = false;
        internal static List<Delegate> Subscriptions;
        internal static TaskScheduler OutEventsScheduler;
        internal static CancellationTokenSource TokenSource;
        internal static List<object> Handlers;

        unsafe internal static void Process(uint OutEvent, IntPtr Buffer, uint BufferSize)
        {
            Export.CFProcess(Export.PluginID, OutEvent, (byte*)Buffer, BufferSize); 
        }

        internal static void Process(Client.OutEventType OutEvent, OutBuffer Buffer)
        {
            Process((uint)OutEvent, Buffer.MemoryPointer, (uint)Buffer.Size);
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

        internal static void ProcessIncomingData(uint ID, IntPtr Buffer, uint BufferSize)
        {
            int Offset = 0;

            if (!Commfort.IsServer)
            {
                var BufPtr = (IntPtr)Buffer;
                switch ((Commfort.Client.InEventType)ID)
                {
                    case Commfort.Client.InEventType.Message:
                        {
                            var Username = ManagedIO.ReadString(BufPtr, ref Offset);
                            var IP = ManagedIO.ReadString(BufPtr, ref Offset);
                            var Icon = (UserIcon)ManagedIO.ReadInteger(BufPtr, ref Offset);
                            var Channel = ManagedIO.ReadString(BufPtr, ref Offset);
                            var Type = (MessageType)ManagedIO.ReadInteger(BufPtr, ref Offset);
                            var Message = ManagedIO.ReadString(BufPtr, ref Offset);

                            if (Type == MessageType.Picture)
                            {
                                var Data = ManagedIO.ReadData(BufPtr, ref Offset);

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
                            var NewState = (ConnectionState)ManagedIO.ReadInteger(BufPtr, ref Offset);

                            Events.PushEvent(new ConnectionChangeEvent(NewState));
                        }
                        break;

                    case Commfort.Client.InEventType.UserConnected:
                        {
                            var Username = ManagedIO.ReadString(BufPtr, ref Offset);
                            var IP = ManagedIO.ReadString(BufPtr, ref Offset);
                            var Icon = (UserIcon)ManagedIO.ReadInteger(BufPtr, ref Offset);

                            Events.PushEvent(new UserConnectedEvent(new User(Username, Icon, IP)));
                        }
                        break;

                    case Commfort.Client.InEventType.UserDisconnected:
                        {
                            var Username = ManagedIO.ReadString(BufPtr, ref Offset);
                            var IP = ManagedIO.ReadString(BufPtr, ref Offset);
                            var Icon = (UserIcon)ManagedIO.ReadInteger(BufPtr, ref Offset);

                            Events.PushEvent(new UserDisconnectedEvent(new User(Username, Icon, IP)));
                        }
                        break;

                    case Commfort.Client.InEventType.LocalUserChannelJoin:
                        {
                            var Channel = ManagedIO.ReadString(BufPtr, ref Offset);

                            Events.PushEvent(new LocalUserChannelJoinEvent(new Channel(Channel)));
                        }
                        break;

                    case Commfort.Client.InEventType.LocalUserChannelLeave:
                        {
                            var Channel = ManagedIO.ReadString(BufPtr, ref Offset);
                            Events.PushEvent(new LocalUserChannelLeaveEvent(new Channel(Channel)));
                        }
                        break;

                    case Commfort.Client.InEventType.UserChannelJoin:
                        {
                            var Channel = ManagedIO.ReadString(BufPtr, ref Offset);
                            var Username = ManagedIO.ReadString(BufPtr, ref Offset);
                            var IP = ManagedIO.ReadString(BufPtr, ref Offset);
                            var Icon = (UserIcon)ManagedIO.ReadInteger(BufPtr, ref Offset);

                            Events.PushEvent(new UserChannelJoinEvent(new Channel(Channel), new User(Username, Icon, IP)));
                        }
                        break;

                    case Commfort.Client.InEventType.UserChannelLeave:
                        {
                            var Channel = ManagedIO.ReadString(BufPtr, ref Offset);
                            var Username = ManagedIO.ReadString(BufPtr, ref Offset);
                            var IP = ManagedIO.ReadString(BufPtr, ref Offset);
                            var Icon = (UserIcon)ManagedIO.ReadInteger(BufPtr, ref Offset);
                            Events.PushEvent(new UserChannelLeaveEvent(new Channel(Channel), new User(Username, Icon, IP)));
                        }
                        break;

                    case Commfort.Client.InEventType.ChannelTopicChange:
                        {
                            var Username = ManagedIO.ReadString(BufPtr, ref Offset);
                            var IP = ManagedIO.ReadString(BufPtr, ref Offset);
                            var Icon = (UserIcon)ManagedIO.ReadInteger(BufPtr, ref Offset);
                            var Channel = ManagedIO.ReadString(BufPtr, ref Offset);
                            var NewTopic = ManagedIO.ReadString(BufPtr, ref Offset);

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
