using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StalkerName.XMLReparser
{

    internal class NameData
    {
        public readonly string Name;

        public readonly string Faction;

        public readonly string Type;

        public NameData(string name, string faction, string type)
        {
            Name = name;
            Faction = faction;
            Type = type;
        }

        public override string ToString()
        {
            return $"Name: {Name} Faction: {Faction}";
        }
    }
}
