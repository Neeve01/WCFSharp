using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using WCFSharp.Types;

namespace WCFSharp
{
    public static partial class Commfort
    {
        public static partial class Client
        {
            internal enum GetterType
            {
                GetPluginSide = 2000, //v
                GetProgramVersion = 2001, //v
                GetSuggestedTempPath = 2010, //v
                GetServerAddress = 10, //v
                GetConnectionState = 11, //v
                GetLocalUserName = 12, //v
                GetLocalUserStatus = 13, //v
                GetActiveChannel = 14, //v
                GetCurrentChannels = 15, //v
                GetCurrentPrivateChannels = 16, //v
                GetUsersInChannel = 17, //v
                GetUsersInChat = 18, //v
                GetLocalUserRights = 19,
                GetUserInfo = 20, 
                GetChannelInfo = 21, 
                GetLocalUserInfo = 22 
            }

            internal static string GetServerAddress()
            {
                uint size = 0;
                var dataPtr = Commfort.GetData(GetterType.GetServerAddress, null, out size);

                var position = 0;
                var address = StaticPointerReader.ReadString(dataPtr, ref position);

                Marshal.FreeHGlobal(dataPtr);

                return address;
            }

            internal static PluginSide GetPluginSide()
            {
                uint size = 0;
                var dataPtr = Commfort.GetData(GetterType.GetPluginSide, null, out size);

                var position = 0;
                var side = (PluginSide)StaticPointerReader.ReadInteger(dataPtr, ref position);

                Marshal.FreeHGlobal(dataPtr);

                return side;
            }

            internal static string GetProgramVersion()
            {
                uint size = 0;
                var dataPtr = Commfort.GetData(GetterType.GetProgramVersion, null, out size);

                var position = 0;
                var version = StaticPointerReader.ReadString(dataPtr, ref position);

                Marshal.FreeHGlobal(dataPtr);

                return version;
            }

            internal static string GetSuggestedTempPath()
            {
                uint size = 0;
                var dataPtr = Commfort.GetData(GetterType.GetSuggestedTempPath, null, out size);

                var position = 0;
                var path = StaticPointerReader.ReadString(dataPtr, ref position);

                Marshal.FreeHGlobal(dataPtr);

                return path;
            }

            internal static ConnectionState GetConnectionState()
            {
                uint size = 0;
                var dataPtr = Commfort.GetData(GetterType.GetSuggestedTempPath, null, out size);

                var position = 0;
                var state = (ConnectionState)StaticPointerReader.ReadInteger(dataPtr, ref position);

                Marshal.FreeHGlobal(dataPtr);

                return state;
            }

            internal static string GetLocalUserName()
            {
                uint size = 0;
                var dataPtr = Commfort.GetData(GetterType.GetLocalUserName, null, out size);

                var position = 0;
                var name = StaticPointerReader.ReadString(dataPtr, ref position);

                Marshal.FreeHGlobal(dataPtr);

                return name;
            }

            internal static string GetLocalUserStatus()
            {
                uint size = 0;
                var dataPtr = Commfort.GetData(GetterType.GetLocalUserStatus, null, out size);

                var position = 0;
                var status = StaticPointerReader.ReadString(dataPtr, ref position);

                Marshal.FreeHGlobal(dataPtr);

                return status;
            }

            internal static Channel GetActiveChannel()
            {
                uint size = 0;
                var dataPtr = Commfort.GetData(GetterType.GetActiveChannel, null, out size);

                var position = 0;
                var channel = StaticPointerReader.ReadString(dataPtr, ref position);

                Marshal.FreeHGlobal(dataPtr);

                return new Channel(channel);
            }

            internal static IEnumerable<Channel> GetCurrentChannels()
            {
                uint size = 0;
                var dataPtr = Commfort.GetData(GetterType.GetCurrentChannels, null, out size);

                var position = 0;
                var count = StaticPointerReader.ReadInteger(dataPtr, ref position);

                for (int i = 1; i <= count; i++)
                {
                    var channel = StaticPointerReader.ReadString(dataPtr, ref position);
                    var topic = StaticPointerReader.ReadString(dataPtr, ref position); // Not really necessary
                    yield return new Channel(channel);
                }

                Marshal.FreeHGlobal(dataPtr);
            }

            internal static IEnumerable<User> GetCurrentPrivateChannels()
            {
                uint size = 0;
                var dataPtr = Commfort.GetData(GetterType.GetCurrentPrivateChannels, null, out size);

                var position = 0;
                var count = StaticPointerReader.ReadInteger(dataPtr, ref position);

                for (int i = 1; i <= count; i++)
                {
                    var user = StaticPointerReader.ReadString(dataPtr, ref position);
                    yield return new User(user);
                }

                Marshal.FreeHGlobal(dataPtr);
            }

            internal static IEnumerable<User> GetCurrentPrivateChannelsStatic()
            {
                uint size = 0;
                var dataPtr = Commfort.GetData(GetterType.GetCurrentPrivateChannels, null, out size);

                var position = 0;
                var count = StaticPointerReader.ReadInteger(dataPtr, ref position);

                for (int i = 1; i <= count; i++)
                {
                    var user = StaticPointerReader.ReadString(dataPtr, ref position);
                    yield return new User(user);
                }

                Marshal.FreeHGlobal(dataPtr);
            }

            internal static UserInfo GetUserInfo(string User)
            {
                var buffer = new OutBuffer(new object[]
                {
                    User
                });

                uint size = 0;
                var dataPtr = Commfort.GetData(GetterType.GetUserInfo, buffer, out size);

                UserInfo userInfo;
                if (size != 0)
                {
                    var position = 0;
                    var ip = StaticPointerReader.ReadString(dataPtr, ref position);
                    var icon = (UserIcon)StaticPointerReader.ReadInteger(dataPtr, ref position);
                    var status = StaticPointerReader.ReadString(dataPtr, ref position);
                    var isactive = (StaticPointerReader.ReadInteger(dataPtr, ref position) == 1) ? true : false;
                    var afktime = StaticPointerReader.ReadInteger(dataPtr, ref position);
                    var activeprocess = StaticPointerReader.ReadString(dataPtr, ref position);

                    userInfo = new UserInfo(icon, ip, true, isactive, afktime, activeprocess, status);
                }
                else
                {
                    userInfo = new UserInfo(UserIcon.Unknown, null, false, false, 0, null, null);
                }
                Marshal.FreeHGlobal(dataPtr);

                return userInfo;
            }

            internal static UserInfo GetUserInfo(User User)
            {
                return GetUserInfo(User.Name);
            }

            internal static IEnumerable<User> GetUsersInChannel(string Channel)
            {
                var buffer = new OutBuffer(new object[]
                {
                    Channel
                });
                uint size = 0;
                var dataPtr = Commfort.GetData(GetterType.GetUsersInChannel, buffer, out size);

                var position = 0;
                var count = StaticPointerReader.ReadInteger(dataPtr, ref position);
                for (int i = 1; i <= count; i++)
                {
                    var Username = StaticPointerReader.ReadString(dataPtr, ref position);
                    var Address = StaticPointerReader.ReadString(dataPtr, ref position); // Not necessary
                    var Icon = (UserIcon)StaticPointerReader.ReadInteger(dataPtr, ref position); // Not necessary

                    yield return new User(Username);
                }

                Marshal.FreeHGlobal(dataPtr);
            }

            internal static IEnumerable<User> GetUsersInChannel(Channel Channel)
            {
                return GetUsersInChannel(Channel.Name);
            }

            internal static IEnumerable<StaticUserInfo> GetUsersInChannelStatic(string Channel)
            {
                var buffer = new OutBuffer(new object[]
                {
                    Channel
                });
                uint size = 0;
                var dataPtr = Commfort.GetData(GetterType.GetUsersInChannel, buffer, out size);

                var position = 0;
                var count = StaticPointerReader.ReadInteger(dataPtr, ref position);
                for (int i = 1; i <= count; i++)
                {
                    var Username = StaticPointerReader.ReadString(dataPtr, ref position);
                    var Address = StaticPointerReader.ReadString(dataPtr, ref position);
                    var Icon = (UserIcon)StaticPointerReader.ReadInteger(dataPtr, ref position);

                    yield return new StaticUserInfo(Username, Address, Icon);
                }

                Marshal.FreeHGlobal(dataPtr);
            }

            internal static IEnumerable<StaticUserInfo> GetUsersInChannelStatic(Channel Channel)
            {
                return GetUsersInChannelStatic(Channel.Name);
            }

            internal static IEnumerable<User> GetUsersInChat()
            {
                uint size = 0;
                var dataPtr = Commfort.GetData(GetterType.GetUsersInChat, null, out size);

                var position = 0;
                var count = StaticPointerReader.ReadInteger(dataPtr, ref position);
                for (int i = 1; i <= count; i++)
                {
                    var Username = StaticPointerReader.ReadString(dataPtr, ref position);
                    var Address = StaticPointerReader.ReadString(dataPtr, ref position); // Not necessary
                    var Icon = (UserIcon)StaticPointerReader.ReadInteger(dataPtr, ref position); // Not necessary

                    yield return new User(Username);
                }

                Marshal.FreeHGlobal(dataPtr);
            }

            internal static IEnumerable<StaticUserInfo> GetUsersInChatStatic()
            {
                uint size = 0;
                var dataPtr = Commfort.GetData(GetterType.GetUsersInChat, null, out size);

                var position = 0;
                var count = StaticPointerReader.ReadInteger(dataPtr, ref position);
                for (int i = 1; i <= count; i++)
                {
                    var Username = StaticPointerReader.ReadString(dataPtr, ref position);
                    var Address = StaticPointerReader.ReadString(dataPtr, ref position);
                    var Icon = (UserIcon)StaticPointerReader.ReadInteger(dataPtr, ref position);

                    yield return new StaticUserInfo(Username, Address, Icon);
                }

                Marshal.FreeHGlobal(dataPtr);
            }

            internal static LocalUserInfo GetLocalUserInfo()
            {
                uint size = 0;
                var dataPtr = Commfort.GetData(GetterType.GetUserInfo, null, out size);

                LocalUserInfo userInfo;

                var position = 0;

                var icon = (UserIcon)StaticPointerReader.ReadInteger(dataPtr, ref position);
                var fullname = StaticPointerReader.ReadString(dataPtr, ref position);
                var birthdate = StaticPointerReader.ReadString(dataPtr, ref position);
                var contacts = StaticPointerReader.ReadString(dataPtr, ref position);
                var work = StaticPointerReader.ReadString(dataPtr, ref position);
                var notes = StaticPointerReader.ReadString(dataPtr, ref position);
                var avatartype = (LocalUserAvatarType)StaticPointerReader.ReadInteger(dataPtr, ref position);
                var avatardata = StaticPointerReader.ReadData(dataPtr, ref position);

                Image avatar = null;
                if (avatardata.Length != 0)
                {
                    try
                    {
                        using (var stream = new MemoryStream(avatardata))
                            avatar = Image.FromStream(stream);
                    } catch
                    {
                        // handle exception
                    }
                }

                userInfo = new LocalUserInfo(icon, fullname, birthdate, contacts, work, notes, avatartype, avatar);

                Marshal.FreeHGlobal(dataPtr);

                return userInfo;
            }

            internal static ChannelInfo GetChannelInfo(string Channel)
            {
                var buffer = new OutBuffer(new object[]
                {
                    Channel
                });
                uint size = 0;
                var dataPtr = Commfort.GetData(GetterType.GetUserInfo, buffer, out size);

                ChannelInfo chanInfo;

                if (size == 0)
                {
                    Marshal.FreeHGlobal(dataPtr);
                    return new ChannelInfo();
                }
                else
                {
                    var position = 0;

                    var topic = StaticPointerReader.ReadString(dataPtr, ref position);
                    var lastchanger = StaticPointerReader.ReadString(dataPtr, ref position);
                    var lastchangetime = StaticPointerReader.ReadDouble(dataPtr, ref position);
                    var greeting = StaticPointerReader.ReadString(dataPtr, ref position);
                    var picturemode = (ChannelPictureMode)StaticPointerReader.ReadInteger(dataPtr, ref position);
                    var visible = StaticPointerReader.ReadInteger(dataPtr, ref position) == 1 ? true : false;
                    var @private = StaticPointerReader.ReadInteger(dataPtr, ref position) == 1 ? true : false;
                    var topiclocked = StaticPointerReader.ReadInteger(dataPtr, ref position) == 1 ? true : false;
                    chanInfo = new ChannelInfo(topic, new User(lastchanger), DateTime.FromOADate(lastchangetime), greeting, picturemode, visible, @private, topiclocked);

                    Marshal.FreeHGlobal(dataPtr);

                    return chanInfo;
                }
            }

            internal static ChannelInfo GetChannelInfo(Channel Channel)
            {
                return GetChannelInfo(Channel.Name);
            }

            internal static bool GetLocalUserRight(LocalUserRight Right, string Channel = null)
            {
                var buffer = new OutBuffer(new object[]
                {
                    (int)Right,
                    Channel
                });

                uint size = 0;
                var dataPtr = Commfort.GetData(GetterType.GetUserInfo, buffer, out size);
                var position = 0;
                var hasright = StaticPointerReader.ReadInteger(dataPtr, ref position) == 1 ? true : false;

                Marshal.FreeHGlobal(dataPtr);

                return hasright;
            }

            internal static bool IsModeratorOf(LocalUserRight Right, string Channel)
            {
                return GetLocalUserRight(LocalUserRight.ChannelModerator, Channel);
            }
        }
    }
}
