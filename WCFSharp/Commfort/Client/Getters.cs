using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using WCFSharp.Types;

namespace WCFSharp
{
    public static partial class Commfort
    {
        public static partial class Client
        {
            internal enum GetterType
            {
                ProgramType = 2000,
                Version = 2001,
                SuggestedTempPath = 2010,
                ServerAddress = 10,
                ConnectionState = 11,
                LocalUserName = 12,
                LocalUserStatus = 13,
                ActiveChannel = 14,
                CurrentChannels = 15,
                CurrentPrivates = 16,
                UsersInChannel = 17,
                UsersInChat = 18,
                LocalUserRights = 19,
                GetUserInfo = 20,
                GetChannelInfo = 21,
                GetCurrentUserInfo = 22
            }

            internal static string GetServerAddress()
            {
                uint size = 0;
                var dataPtr = Commfort.GetData(GetterType.ServerAddress, null, out size);

                var position = 0;
                var address = StaticPointerReader.ReadString(dataPtr, ref position);

                Marshal.FreeHGlobal(dataPtr);

                return address;
            }
        }
    }
}
