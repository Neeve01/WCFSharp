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
            public static async Task<string> GetServerAddressAsync()
            {
                return await Task.Factory.StartNew<string>(() =>
                {
                    return GetServerAddress();
                }, Commfort.TokenSource.Token, TaskCreationOptions.None, Commfort.OutEventsScheduler);
            }
        }
    }
}
