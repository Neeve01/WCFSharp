using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
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

            // Prints current server address
            if (e.Message.StartsWith("!serveraddress"))
            {
                await Commfort.Client.SendMessageAsync(e.Channel, $"Server address: {await Commfort.Client.GetServerAddressAsync()}");
            }

            // Saves your current avatar to commfort directory
            if (e.Message.StartsWith("!myavatar"))
            {
                var data = await Client.GetLocalUserInfoAsync();
                if (data.Avatar != null)
                {
                    using (var avatar = new Bitmap(data.Avatar))
                        try
                        {
                            var filename = "avatar" + (data.AvatarType == Client.LocalUserAvatarType.Gif ? ".gif" : ".jpg");
                            avatar.Save(filename);
                        }
                        catch (Exception E)
                        {
                            await Client.SendMessageAsync($"Couldn't save avatar picture :(", E.Message);
                        }
                }
            }

            // Prints your known user info
            if (e.Message.StartsWith("!userinfo"))
            {
                var sb = new StringBuilder();
                var userinfo = await e.User.GetInfoAsync();
                if (!userinfo.IsOnline)
                    return;

                sb.AppendLine($"UserInfo: {e.User.Name}");
                sb.AppendLine($"Is active: {userinfo.IsActive}");
                sb.AppendLine($"Active process: {userinfo.ActiveProcess}");
                sb.AppendLine($"Status: {userinfo.Status?? "Not set"}");
                sb.AppendLine($"UserIcon: {userinfo.Icon}");
                await Commfort.Client.SendMessageAsync(e.Channel, sb.ToString());
            }
        }
    }
}
 