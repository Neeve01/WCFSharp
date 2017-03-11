using System;
using System.Collections.Generic;
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
        }
    }
}
