using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using WCFSharp.Types;

namespace WCFSharp
{
    public static partial class Commfort
    {
        public static partial class Client
        {

            public static async Task<string> GetServerAddressAsync()
            {
                return await Task.Factory.StartNew(() =>
                {
                    return GetServerAddress();
                }, Commfort.TokenSource.Token, TaskCreationOptions.None, Commfort.Scheduler);
            }

            public static async Task<PluginSide> GetPluginSideAsync()
            {
                return await Task.Factory.StartNew(() =>
                {
                    return GetPluginSide();
                }, Commfort.TokenSource.Token, TaskCreationOptions.None, Commfort.Scheduler);
            }

            public static async Task<string> GetProgramVersionAsync()
            {
                return await Task.Factory.StartNew(() =>
                {
                    return GetProgramVersion();
                }, Commfort.TokenSource.Token, TaskCreationOptions.None, Commfort.Scheduler);
            }

            public static async Task<string> GetSuggestedTempPathAsync()
            {
                return await Task.Factory.StartNew(() =>
                {
                    return GetSuggestedTempPath();
                }, Commfort.TokenSource.Token, TaskCreationOptions.None, Commfort.Scheduler);
            }

            public static async Task<ConnectionState> GetConnectionStateAsync()
            {
                return await Task.Factory.StartNew(() =>
                {
                    return GetConnectionState();
                }, Commfort.TokenSource.Token, TaskCreationOptions.None, Commfort.Scheduler);
            }

            public static async Task<string> GetLocalUserNameAsync()
            {
                return await Task.Factory.StartNew(() =>
                {
                    return GetLocalUserName();
                }, Commfort.TokenSource.Token, TaskCreationOptions.None, Commfort.Scheduler);
            }

            public static async Task<string> GetLocalUserStatusAsync()
            {
                return await Task.Factory.StartNew(() =>
                {
                    return GetLocalUserStatus();
                }, Commfort.TokenSource.Token, TaskCreationOptions.None, Commfort.Scheduler);
            }

            public static async Task<Channel> GetActiveChannelAsync()
            {
                return await Task.Factory.StartNew(() =>
                {
                    return GetActiveChannel();
                }, Commfort.TokenSource.Token, TaskCreationOptions.None, Commfort.Scheduler);
            }

            public static async Task<IEnumerable<Channel>> GetCurrentChannelsAsync()
            {
                return await Task.Factory.StartNew(() =>
                {
                    return GetCurrentChannels();
                }, Commfort.TokenSource.Token, TaskCreationOptions.None, Commfort.Scheduler);
            }

            public static async Task<IEnumerable<User>> GetCurrentPrivateChannelsAsync()
            {
                return await Task.Factory.StartNew(() =>
                {
                    return GetCurrentPrivateChannels();
                }, Commfort.TokenSource.Token, TaskCreationOptions.None, Commfort.Scheduler);
            }

            public static async Task<IEnumerable<User>> GetCurrentPrivateChannelsStaticAsync()
            {
                return await Task.Factory.StartNew(() =>
                {
                    return GetCurrentPrivateChannelsStatic();
                }, Commfort.TokenSource.Token, TaskCreationOptions.None, Commfort.Scheduler);
            }

            public static async Task<UserInfo> GetUserInfoAsync(string User)
            {
                return await Task.Factory.StartNew(() =>
                {
                    return GetUserInfo(User);
                }, Commfort.TokenSource.Token, TaskCreationOptions.None, Commfort.Scheduler);
            }

            public static async Task<UserInfo> GetUserInfoAsync(User User)
            {
                return await Task.Factory.StartNew(() =>
                {
                    return GetUserInfo(User);
                }, Commfort.TokenSource.Token, TaskCreationOptions.None, Commfort.Scheduler);
            }

            public static async Task<IEnumerable<User>> GetUsersInChannelAsync(string Channel)
            {
                return await Task.Factory.StartNew(() =>
                {
                    return GetUsersInChannel(Channel);
                }, Commfort.TokenSource.Token, TaskCreationOptions.None, Commfort.Scheduler);
            }

            public static async Task<IEnumerable<User>> GetUsersInChannelAsync(Channel Channel)
            {
                return await Task.Factory.StartNew(() =>
                {
                    return GetUsersInChannel(Channel);
                }, Commfort.TokenSource.Token, TaskCreationOptions.None, Commfort.Scheduler);
            }

            public static async Task<IEnumerable<StaticUserInfo>> GetUsersInChannelStaticAsync(string Channel)
            {
                return await Task.Factory.StartNew(() =>
                {
                    return GetUsersInChannelStatic(Channel);
                }, Commfort.TokenSource.Token, TaskCreationOptions.None, Commfort.Scheduler);
            }

            public static async Task<IEnumerable<StaticUserInfo>> GetUsersInChannelStaticAsync(Channel Channel)
            {
                return await Task.Factory.StartNew(() =>
                {
                    return GetUsersInChannelStatic(Channel);
                }, Commfort.TokenSource.Token, TaskCreationOptions.None, Commfort.Scheduler);
            }

            public static async Task<IEnumerable<User>> GetUsersInChatAsync()
            {
                return await Task.Factory.StartNew(() =>
                {
                    return GetUsersInChat();
                }, Commfort.TokenSource.Token, TaskCreationOptions.None, Commfort.Scheduler);
            }

            public static async Task<IEnumerable<StaticUserInfo>> GetUsersInChatStaticAsync()
            {
                return await Task.Factory.StartNew(() =>
                {
                    return GetUsersInChatStatic();
                }, Commfort.TokenSource.Token, TaskCreationOptions.None, Commfort.Scheduler);
            }

            public static async Task<LocalUserInfo> GetLocalUserInfoAsync()
            {
                return await Task.Factory.StartNew(() =>
                {
                    return GetLocalUserInfo();
                }, Commfort.TokenSource.Token, TaskCreationOptions.None, Commfort.Scheduler);
            }

            public static async Task<ChannelInfo> GetChannelInfoAsync(string Channel)
            {
                return await Task.Factory.StartNew(() =>
                {
                    return GetChannelInfo(Channel);
                }, Commfort.TokenSource.Token, TaskCreationOptions.None, Commfort.Scheduler);
            }

            public static async Task<ChannelInfo> GetChannelInfoAsync(Channel Channel)
            {
                return await Task.Factory.StartNew(() =>
                {
                    return GetChannelInfo(Channel);
                }, Commfort.TokenSource.Token, TaskCreationOptions.None, Commfort.Scheduler);
            }

            public static async Task<bool> GetLocalUserRightAsync(LocalUserRight Right)
            {
                return await Task.Factory.StartNew(() =>
                {
                    return GetLocalUserRight(Right);
                }, Commfort.TokenSource.Token, TaskCreationOptions.None, Commfort.Scheduler);
            }

            public static async Task<bool> IsModeratorOfAsync(Channel Channel)
            {
                return await Task.Factory.StartNew(() =>
                {
                    return IsModeratorOf(LocalUserRight.ChannelModerator, Channel.Name);
                }, Commfort.TokenSource.Token, TaskCreationOptions.None, Commfort.Scheduler);
            }

            public static async Task<bool> IsModeratorOfAsync(string Channel)
            {
                return await Task.Factory.StartNew(() =>
                {
                    return IsModeratorOf(LocalUserRight.ChannelModerator, Channel);
                }, Commfort.TokenSource.Token, TaskCreationOptions.None, Commfort.Scheduler);
            }
        }
    }
}
