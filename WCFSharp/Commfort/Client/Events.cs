using System.Drawing;
using WCFSharp.Types;

namespace WCFSharp
{
    public static partial class Commfort
    {
        public static partial class Client
        {
            public static class Events
            {
                public struct ConnectionChangeEvent
                {
                    public ConnectionState State { get; }

                    public ConnectionChangeEvent(ConnectionState State)
                    {
                        this.State = State;
                    }
                }

                public struct MessageEvent
                {
                    public User User { get; }
                    public Channel Channel { get; }
                    public MessageType Type { get; }
                    public string Message { get; }

                    public MessageEvent(User User, Channel Channel, MessageType Type, string Message)
                    {
                        this.User = User;
                        this.Channel = Channel;
                        this.Type = Type;
                        this.Message = Message;
                    }
                }

                public struct PictureEvent
                {
                    public User User { get; }
                    public Channel Channel { get; }
                    public Image Picture { get; }

                    public PictureEvent(User User, Channel Channel, Image Picture)
                    {
                        this.User = User;
                        this.Channel = Channel;
                        this.Picture = Picture;
                    }
                }

                public struct PrivateMessageEvent
                {
                    public User User { get; }
                    public Channel Channel { get; }
                    public Image Picture { get; }

                    public PrivateMessageEvent(User User, Channel Channel, Image Picture)
                    {
                        this.User = User;
                        this.Channel = Channel;
                        this.Picture = Picture;
                    }
                }

                public struct LocalUserChannelJoinEvent
                {
                    public Channel Channel { get; }

                    public LocalUserChannelJoinEvent(Channel Channel)
                    {
                        this.Channel = Channel;
                    }
                }

                public struct LocalUserChannelLeaveEvent
                {
                    public Channel Channel { get; }

                    public LocalUserChannelLeaveEvent(Channel Channel)
                    {
                        this.Channel = Channel;
                    }
                }

                public struct UserChannelJoinEvent
                {
                    public Channel Channel { get; }
                    public User User { get; }

                    public UserChannelJoinEvent(Channel Channel, User User)
                    {
                        this.Channel = Channel;
                        this.User = User;
                    }
                }

                public struct UserDisconnectedEvent
                {
                    public string User { get; }
                    public UserIcon Icon { get; }
                    public string Address { get; }

                    public UserDisconnectedEvent(string User, UserIcon Icon, string Address)
                    {
                        this.User = User;
                        this.Icon = Icon;
                        this.Address = Address;
                    }
                }

                public struct ChannelTopicChangeEvent
                {
                    public Channel Channel { get; }
                    public string NewTopic { get; }
                    public User User { get; }

                    public ChannelTopicChangeEvent(Channel Channel, string NewTopic, User User)
                    {
                        this.Channel = Channel;
                        this.NewTopic = NewTopic;
                        this.User = User;
                    }
                }

                public struct UserChannelLeaveEvent
                {
                    public Channel Channel { get; }
                    public User User { get; }

                    public UserChannelLeaveEvent(Channel Channel, User User)
                    {
                        this.Channel = Channel;
                        this.User = User;
                    }
                }


                public struct UserConnectedEvent
                {
                    public User User { get; }

                    public UserConnectedEvent(User User)
                    {
                        this.User = User;
                    }
                }
            }
        }
    }
}
