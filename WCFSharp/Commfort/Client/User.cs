using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WCFSharp.Types;

namespace WCFSharp
{
    public static partial class Commfort
    {
        public static partial class Client
        {
            public class User
            {
                public string Name { get; }
                public async Task<UserInfo> GetInfoAsync()
                {
                    return await Task.Factory.StartNew<UserInfo>(() =>
                    {
                        return GetInfo();
                    }, Commfort.TokenSource.Token, TaskCreationOptions.None, Commfort.Scheduler);
                }

                internal UserInfo GetInfo()
                {
                    return GetUserInfo(this.Name);
                }

                public User(string Name)
                {
                    this.Name = Name;
                }
            }

            public struct StaticUserInfo
            {
                public UserIcon Icon { get; }
                public string Address { get; }
                public string Name { get; }

                public StaticUserInfo(string Name, string Address, UserIcon Icon)
                {
                    this.Name = Name;
                    this.Address = Address;
                    this.Icon = Icon;
                }
            }

            public struct UserInfo
            {
                public UserIcon Icon { get; }
                public string Address { get; }
                public bool IsOnline { get; }
                public bool IsActive { get; }
                public int AFKTime { get; }
                public string ActiveProcess { get; }
                public string Status { get; }

                public UserInfo(UserIcon Icon, string Address, bool IsOnline, bool IsActive, int AFKTime, string ActiveProcess, string Status)
                {
                    this.Icon = Icon;
                    this.Address = Address;
                    this.IsOnline = IsOnline;
                    this.IsActive = IsActive;
                    this.AFKTime = AFKTime;
                    this.ActiveProcess = ActiveProcess;
                    this.Status = Status;
                }
            }

            public enum LocalUserAvatarType
            {
                Gif = 0,
                Jpeg = 1
            }

            public enum LocalUserRight
            {
                ManageRights = 0,
                ManageAccounts = 1,
                ManageAccountActivation = 2,
                BoardModerator = 3,
                AllChannelsModerator = 4,
                ChannelModerator = 5,
                BroadcastMessages = 6,
                HiddenIP = 7,
                FloodBypass = 8,
                LogsAccess = 9
            }

            public struct LocalUserInfo
            {
                public UserIcon Icon { get; }
                public string FullName { get; }
                public string BirthDate { get; }
                public string ContactInfo { get; }
                public string WorkInfo { get; }
                public string Notes { get; }
                public LocalUserAvatarType AvatarType { get; }
                public Image Avatar { get; }

                public LocalUserInfo(UserIcon Icon, string FullName, string BirthDate, string ContactInfo, string WorkInfo, string Notes, LocalUserAvatarType AvatarType, Image Avatar)
                {
                    this.Icon = Icon;
                    this.FullName = FullName;
                    this.BirthDate = BirthDate;
                    this.ContactInfo = ContactInfo;
                    this.WorkInfo = WorkInfo;
                    this.Notes = Notes;
                    this.AvatarType = AvatarType;
                    this.Avatar = Avatar;
                }
            }
        }
    }
}
