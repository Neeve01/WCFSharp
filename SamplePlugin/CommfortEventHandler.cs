using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WCFSharp;
using WCFSharp.Types;
using static WCFSharp.Commfort;

namespace SamplePlugin
{
    public class CommfortEventHandler
    {
        public async Task OnMessage(Client.Events.MessageEvent e)
        {
            var localuser = await Client.GetLocalUserNameAsync(); ;

            if (e.User.Name != localuser)
                return;

            if (e.Message.StartsWith("!serveraddress"))
            {
                await Commfort.Client.SendMessageAsync(e.Channel, $"Server address: {await Commfort.Client.GetServerAddressAsync()}");
            }

            if (e.Message.StartsWith("!userinfo"))
            {
                var sb = new StringBuilder();
                var userinfo = await e.User.GetInfoAsync();
                if (!userinfo.IsOnline)
                    return;

                sb.AppendLine($"UserInfo: {e.User.Name}");
                sb.AppendLine($"Is active: {userinfo.IsActive}");
                sb.AppendLine($"Active process: {userinfo.ActiveProcess}");
                sb.AppendLine($"Status: {userinfo.Status}");
                sb.AppendLine($"UserIcon: {userinfo.Icon}");
                await Commfort.Client.SendMessageAsync(e.Channel, sb.ToString());
            }
        }
    }
}
 