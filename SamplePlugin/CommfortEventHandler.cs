using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WCFSharp;
using WCFSharp.Types;

namespace SamplePlugin
{
    public class CommfortEventHandler
    {
        public async Task OnMessage(WCFSharp.Types.ClientEvents.MessageEvent e)
        {
            if (!e.Message.StartsWith("Repeating: "))
            {
                await Commfort.Client.SendMessageAsync(e.Channel, $"Repeating: {e.Message}");
            }
        }
    }
}
 