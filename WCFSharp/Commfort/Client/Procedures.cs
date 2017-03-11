using WCFSharp.Types;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;

namespace WCFSharp
{
    public static partial class Commfort
    {
        public static partial class Client
        {
            internal enum InEventType
            {
                Message = 5,
                ConnectionStatusChange = 3,
                UserConnected = 1,
                UserDisconnected = 2,
                LocalUserChannelJoin = 30,
                LocalUserChannelLeave = 31,
                UserChannelJoin = 32,
                UserChannelLeave = 33,
                ChannelTopicChange = 6
            }

            internal enum ProcedureType
            {
                PrintTextInEvents = 100,
                SendPersonalMessage = 70,
                JoinChannel = 67,
                LeaveChannel = 66,
                LeavePrivateChannel = 65,
                SendMessage = 50,
                SendPrivateMessage = 63,
                SendPicture = 51,
                SendPrivatePicture = 64,
                ChangeStatus = 53,
                ChangeChannelTopic = 61,
                ChangeChannelGreeting = 62,
                ClearChannel = 80,
                ChangeUserInfo = 81,
                SendDataToServer = 2020
            }

            internal static void PrintMessageToEvents(string Message, EventMessageStyle Style = EventMessageStyle.Default, bool WithTime = true, bool WriteToHistory = true)
            {
                if (IsServer)
                    return;

                var outbuf = new OutBuffer
                (
                    (int)Style,
                    WithTime ? 1 : 0,
                    WriteToHistory ? 1 : 0,
                    Message
                );
                Process(ProcedureType.PrintTextInEvents, outbuf);
            }

            internal static void JoinChannel(string Channel, bool ShowInChannelList = true, bool InviteOnly = true)
            {
                if (IsServer)
                    return;

                int type = 0;
                if (!ShowInChannelList && !InviteOnly)
                    type = 0;
                else if (ShowInChannelList && !InviteOnly)
                    type = 1;
                else if (!ShowInChannelList && InviteOnly)
                    type = 2;
                else if (ShowInChannelList && InviteOnly)
                    type = 3;
                
                var outbuf = new OutBuffer
                (
                    type,
                    Channel
                );
                Process(ProcedureType.JoinChannel, outbuf);
            }

            internal static void LeaveChannel(string Channel)
            {
                if (IsServer)
                    return;

                var outbuf = new OutBuffer
                (
                    Channel
                );
                Process(ProcedureType.LeaveChannel, outbuf);
            }

            internal static void LeavePrivateChannel(string Username)
            {
                if (IsServer)
                    return;

                var outbuf = new OutBuffer
                (
                    Username
                );
                Process(ProcedureType.LeavePrivateChannel, outbuf);
            }

            internal static void SendMessage(string Channel, string Message, MessageType Type = MessageType.Default)
            {
                if (IsServer)
                    return;
                if (Type == MessageType.Picture)
                    return;

                var outbuf = new OutBuffer
                (
                    Channel, 
                    (int)Type,
                    Message
                );
                Process(ProcedureType.SendMessage, outbuf);
            }

            internal static void SendMessage(Channel Channel, string Message, MessageType Type = MessageType.Default)
            {
                SendMessage(Channel.Name, Message, Type);
            }

            internal static void SendPrivateMessage(string Username, string Message, MessageType Type = MessageType.Default)
            {
                if (IsServer)
                    return;
                if (Type == MessageType.Picture)
                    return;

                var outbuf = new OutBuffer
                (
                    Username,
                    (int)Type,
                    Message
                );
                Process(ProcedureType.SendPrivateMessage, outbuf);
            }

            internal static void SendMessage(User User, string Message, MessageType Type = MessageType.Default)
            {
                SendMessage(User.Name, Message, Type);
            }

            internal static void SendPersonalMessage(string Username, string Message, bool Important = false)
            {
                if (IsServer)
                    return;

                var outbuf = new OutBuffer
                (
                    Username,
                    Important ? 1 : 0,
                    Message
                );
                Process(ProcedureType.SendPersonalMessage, outbuf);
            }

            internal static void SendPersonalMessage(User User, string Message, bool Important = false)
            {
                SendPersonalMessage(User.Name, Message, Important);
            }

            internal static void SendPicture(string Channel, Image Picture, CommfortPictureFormat Format = CommfortPictureFormat.Jpeg)
            {
                if (IsServer)
                    return;

                ImageFormat imgformat;
                switch (Format)
                {
                    case CommfortPictureFormat.Bitmap:
                        imgformat = ImageFormat.Bmp;
                        break;

                    case CommfortPictureFormat.Jpeg:
                        imgformat = ImageFormat.Jpeg;
                        break;

                    case CommfortPictureFormat.PNG:
                        imgformat = ImageFormat.Png;
                        break;

                    default:
                        return;
                }

                using (var stream = new MemoryStream())
                {
                    Picture.Save(stream, imgformat);
                    stream.Position = 0;
                    var data = new byte[stream.Length];
                    stream.Read(data, 0, data.Length);

                    var outbuf = new OutBuffer
                    (
                        Channel,
                        (int)Format,
                        data
                    );
                    Process(ProcedureType.SendPicture, outbuf);
                }
            }

            internal static void SendPrivatePicture(string Username, Image Picture, CommfortPictureFormat Format = CommfortPictureFormat.Jpeg)
            {
                if (IsServer)
                    return;

                ImageFormat imgformat;
                switch (Format)
                {
                    case CommfortPictureFormat.Bitmap:
                        imgformat = ImageFormat.Bmp;
                        break;

                    case CommfortPictureFormat.Jpeg:
                        imgformat = ImageFormat.Jpeg;
                        break;

                    case CommfortPictureFormat.PNG:
                        imgformat = ImageFormat.Png;
                        break;

                    default:
                        return;
                }

                using (var stream = new MemoryStream())
                {
                    Picture.Save(stream, imgformat);
                    stream.Position = 0;
                    var data = new byte[stream.Length];
                    stream.Read(data, 0, data.Length);

                    var outbuf = new OutBuffer
                    (
                        Username,
                        (int)Format,
                        data
                    );
                    Process(ProcedureType.SendPrivatePicture, outbuf);
                }
            }

            internal static void ChangeStatus(string NewStatus, bool IgnoreImportantFlag = false, bool TurnOffSounds = false, bool TurnOffNotifications = false)
            {
                if (IsServer)
                    return;

                var outbuf = new OutBuffer
                (
                    NewStatus,
                    IgnoreImportantFlag ? 1 : 0,
                    TurnOffSounds ? 1 : 0,
                    TurnOffNotifications ? 1 : 0
                );
                Process(ProcedureType.ChangeStatus, outbuf);
            }

            internal static void ChangeChannelTopic(string Channel, string NewTopic)
            {
                if (IsServer)
                    return;

                var outbuf = new OutBuffer
                (
                    Channel,
                    NewTopic
                );
                Process(ProcedureType.ChangeChannelTopic, outbuf);
            }

            internal static void ChangeChannelTopic(Channel Channel, string NewTopic)
            {
                ChangeChannelTopic(Channel.Name, NewTopic);
            }

            internal static void ChangeChannelGreeting(string Channel, string NewGreeting)
            {
                if (IsServer)
                    return;

                var outbuf = new OutBuffer
                (
                    Channel,
                    NewGreeting
                );
                Process(ProcedureType.ChangeChannelGreeting, outbuf);
            }

            internal static void ChangeChannelGreeting(Channel Channel, string NewTopic)
            {
                ChangeChannelGreeting(Channel.Name, NewTopic);
            }

            internal static void ClearChannel(string Channel)
            {
                if (IsServer)
                    return;

                var outbuf = new OutBuffer
                (
                    Channel
                );
                Process(ProcedureType.ClearChannel, outbuf);
            }

            internal static void ClearChannel(Channel Channel)
            {
                ClearChannel(Channel.Name);
            }

            internal static void SendDataToServer(string Receiver, byte[] Data)
            {
                if (IsServer)
                    return;

                var outbuf = new OutBuffer
                (
                    Receiver,
                    Data
                );
                Process(ProcedureType.SendDataToServer, outbuf);
            }
        }
    }
}
