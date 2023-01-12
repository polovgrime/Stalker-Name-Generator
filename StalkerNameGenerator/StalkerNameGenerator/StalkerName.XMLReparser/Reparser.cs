using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml;

namespace StalkerName.XMLReparser
{
    internal class Reparser
    {
        private const string BANDITS = "bandit";
        private const string STALKERS = "stalker";
        private const string COMMON = "common";
        private const string NAME_TYPE = "name";
        private const string LASTNAME_TYPE = "lastname";
        private readonly string savePath;
        private readonly string firstNamesFile;
        private readonly string lastNamesFile;
        private Encoding cyrillicEncoding;

        public Reparser(string savePath, string firstNamesFile, string lastNamesFile)
        {
            this.savePath = savePath;
            this.firstNamesFile = firstNamesFile;
            this.lastNamesFile = lastNamesFile;
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            cyrillicEncoding = Encoding.GetEncoding("windows-1251");
        }

        public void Start()
        {
            try
            {
                ParseAndWriteBandits();
                ParseAndWriteStalkers();
            } 
            catch (Exception ex)
            {
                throw new Exception("Exception while parsing", ex);
            }
        }

        private void ParseAndWriteBandits()
        {
            var names = LoadNamesFiltered(firstNamesFile, NAME_TYPE, BANDITS);
            var lastNames = LoadNamesFiltered(lastNamesFile, LASTNAME_TYPE, BANDITS);
            var all = names.Concat(lastNames).ToList();
            SaveToXml(all, "bandits");
        }

        private void ParseAndWriteStalkers()
        {
            var names = LoadNamesFiltered(firstNamesFile, NAME_TYPE, STALKERS);
            var lastNames = LoadNamesFiltered(lastNamesFile, LASTNAME_TYPE, STALKERS);
            var all = names.Concat(lastNames).ToList();
            SaveToXml(all, "stalkers");
        }


        private List<NameData> LoadNamesFiltered(string file, string type, string filter)
        {
            return LoadNames(file, type)
                .Where(e => e.Faction == filter || e.Faction == COMMON)
                .ToList();
        }

        private List<NameData> LoadNames(string file, string type)
        {
            var doc = new XmlDocument();

            var contents = File.ReadAllText(file, cyrillicEncoding);

            doc.LoadXml(contents);
            var nodes = doc.LastChild!.ChildNodes;
            return nodes
                .Cast<XmlNode>()
                .Select(e => new NameData(e.InnerText, e.Attributes!["id"]!.InnerText.AdaptName(), type))
                .ToList();
        }

        private void SaveToXml(List<NameData> data, string name)
        {
            var newXmlDoc = new XDocument();
            var elements = data.Select(e => {
                var element = new XElement("name");
                element.Add(new XAttribute("faction", e.Faction));
                element.Add(new XAttribute("type", e.Type));
                element.Add(e.Name);
                return element;
            });
            var root = new XElement("names");

            root.Add(elements);
            newXmlDoc.Add(root);
            newXmlDoc.Save(savePath + name + ".xml");
        }

    }
}
