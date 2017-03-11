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
                return await Task.Factory.StartNew<string>(() =>
                {
                    return GetServerAddress();
                }, Commfort.TokenSource.Token, TaskCreationOptions.None, Commfort.Scheduler);
            }

            public static async Task<PluginSide> GetPluginSideAsync()
            {
                return await Task.Factory.StartNew<PluginSide>(() =>
                {
                    return GetPluginSide();
                }, Commfort.TokenSource.Token, TaskCreationOptions.None, Commfort.Scheduler);
            }

            public static async Task<string> GetProgramVersionAsync()
            {
                return await Task.Factory.StartNew<string>(() =>
                {
                    return GetProgramVersion();
                }, Commfort.TokenSource.Token, TaskCreationOptions.None, Commfort.Scheduler);
            }

            public static async Task<string> GetSuggestedTempPathAsync()
            {
                return await Task.Factory.StartNew<string>(() =>
                {
                    return GetSuggestedTempPath();
                }, Commfort.TokenSource.Token, TaskCreationOptions.None, Commfort.Scheduler);
            }

            public static async Task<ConnectionState> GetConnectionStateAsync()
            {
                return await Task.Factory.StartNew<ConnectionState>(() =>
                {
                    return GetConnectionState();
                }, Commfort.TokenSource.Token, TaskCreationOptions.None, Commfort.Scheduler);
            }

            public static async Task<string> GetLocalUserNameAsync()
            {
                return await Task.Factory.StartNew<string>(() =>
                {
                    return GetLocalUserName();
                }, Commfort.TokenSource.Token, TaskCreationOptions.None, Commfort.Scheduler);
            }

            public static async Task<string> GetLocalUserStatusAsync()
            {
                return await Task.Factory.StartNew<string>(() =>
                {
                    return GetLocalUserStatus();
                }, Commfort.TokenSource.Token, TaskCreationOptions.None, Commfort.Scheduler);
            }

            public static async Task<Channel> GetActiveChannelAsync()
            {
                return await Task.Factory.StartNew<Channel>(() =>
                {
                    return GetActiveChannel();
                }, Commfort.TokenSource.Token, TaskCreationOptions.None, Commfort.Scheduler);
            }

            public static async Task<IEnumerable<Channel>> GetCurrentChannelsAsync()
            {
                return await Task.Factory.StartNew<IEnumerable<Channel>>(() =>
                {
                    return GetCurrentChannels();
                }, Commfort.TokenSource.Token, TaskCreationOptions.None, Commfort.Scheduler);
            }

            public static async Task<IEnumerable<User>> GetCurrentPrivateChannelsAsync()
            {
                return await Task.Factory.StartNew<IEnumerable<User>>(() =>
                {
                    return GetCurrentPrivateChannels();
                }, Commfort.TokenSource.Token, TaskCreationOptions.None, Commfort.Scheduler);
            }

            public static async Task<UserInfo> GetUserInfoAsync(string User)
            {
                return await Task.Factory.StartNew<UserInfo>(() =>
                {
                    return GetUserInfo(User);
                }, Commfort.TokenSource.Token, TaskCreationOptions.None, Commfort.Scheduler);
            }
        }
    }
}
