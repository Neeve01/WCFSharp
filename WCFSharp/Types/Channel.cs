using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WCFSharp.Types
{
    public class Channel
    {
        public string Name { get; }

        public Channel(string Name)
        {
            this.Name = Name;
        }
    }
}
