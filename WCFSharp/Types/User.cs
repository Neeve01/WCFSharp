using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WCFSharp.Types
{
    public class User
    {
        public string Name { get; }
        public UserIcon Icon { get; set; }
        public string Address { get; set; }

        public bool IsOnline { get; set; }

        public User() { }

        public User(string Name, UserIcon Icon, string Address)
        {
            this.Name = Name;
            this.Icon = Icon;
            this.Address = Address;
        }
    }
}
