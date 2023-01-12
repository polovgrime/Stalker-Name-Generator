using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StalkerName.XMLReparser
{
    internal class Stalker
    {
        public readonly string Name;
        public readonly string LastName;

        public Stalker(string name, string lastname)
        {
            Name = name;
            LastName = lastname;
        }

        public override string ToString()
        {
            return $"{Name} {LastName}";
        }
    }
}
