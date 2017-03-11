using System;

namespace WCFSharp
{
    public static partial class Commfort
    {
        public static partial class Client
        {
            public class Channel
            {
                public string Name { get; }

                public Channel(string Name)
                {
                    this.Name = Name;
                }
            }

            public struct StaticChannelInfo
            {
                public string Name { get; }
                public string Topic { get; }
            }

            public enum ChannelPictureMode
            {
                Default = 0,
                Restricted = 1,
                Small = 2,
                Medium = 3,
                Large = 4,
                VeryLarge = 5
            }

            public struct ChannelInfo
            {
                public string Topic { get; }
                public User LastTopicChanger { get; }
                public DateTime LastTopicChange { get; }
                public string Greeting { get; }
                public ChannelPictureMode PicturesMode { get; }
                public bool IsHidden { get; }
                public bool IsInviteOnly { get; }
                public bool TopicLocked { get; }

                public ChannelInfo(string Topic, User LastTopicChanger, DateTime LastTopicChange, string Greeting, ChannelPictureMode PicturesMode, bool IsHidden, bool IsInviteOnly, bool TopicLocked)
                {
                    this.Topic = Topic;
                    this.LastTopicChanger = LastTopicChanger;
                    this.LastTopicChange = LastTopicChange;
                    this.Greeting = Greeting;
                    this.PicturesMode = PicturesMode;
                    this.IsHidden = IsHidden;
                    this.IsInviteOnly = IsInviteOnly;
                    this.TopicLocked = TopicLocked;
                }
            }
        }
    }
}
