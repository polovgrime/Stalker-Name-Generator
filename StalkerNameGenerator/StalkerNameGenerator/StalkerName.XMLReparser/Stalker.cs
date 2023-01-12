using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StalkerName.XMLReparser
{
    internal class Stalker
    {
        public readonly NameData Name;

        public readonly NameData LastName;

        public Stalker(NameData name, NameData lastName)
        {
            Name = name;
            LastName = lastName;
        }

        public override string ToString()
        {
            return Name.Name + " " + LastName.Name;
        }
    }
}
