using System.Xml;
using System.Linq;
using System.Net.Http.Headers;

namespace StalkerName.Builder
{
    public class Program
    {
        private const string STALKERS_XML = @".\stalkers.xml";
        private const string BANDITS_XML = @".\bandits.xml";
        private const string STALKER_FACTION = "stalker";
        private const string BANDIT_FACTION = "bandit";
        private const string NAME_TYPE = "name";
        private const string LASTNAME_TYPE = "lastname";
        private const string SAVE_DIRECTORY = "output";
        private const string SAVE_NAME = @".\{0}\{1}.txt";
        public static void Main(string[] args)
        {
           
            PrintBanditNames(100);
            PrintStalkerNames(100);
        }

        private static void PrintStalkerNames(int count)
        {
            PrintNames(STALKERS_XML, STALKER_FACTION, count);
        }


        private static void PrintBanditNames(int count)
        {
            PrintNames(BANDITS_XML, BANDIT_FACTION, count);
        }


        private static void PrintNames(string path, string stalkerType, int count)
        {
            var doc = new XmlDocument();

            var contents = File.ReadAllText(path);
            doc.LoadXml(contents);

            var names = doc.GetValues(NAME_TYPE, stalkerType);
            var lastNames = doc.GetValues(LASTNAME_TYPE, stalkerType);

            var outNames = new List<string>();
            var random = new Random();

            while (count > 0)
            {
                outNames.Add($"{names[random.Next(names.Count)]} {lastNames[random.Next(lastNames.Count)]}");
                count--;
            }

            Console.WriteLine(string.Join(",\n", outNames));

            if (Directory.Exists(SAVE_DIRECTORY) == false)
            {
                Directory.CreateDirectory(SAVE_DIRECTORY);
            }

            var filename = string.Format(SAVE_NAME, SAVE_DIRECTORY, stalkerType);
            
            if (File.Exists(filename) == false)
            {
                File.Create(filename).Dispose();
            }

            File.AppendAllText(filename, "\n" + string.Join(",\n", outNames));
        }

    }

    static class NameListEx
    {
        public static string GetRandomName(this List<string> source, Random rand)
        {
            return source[rand.Next(source.Count)];
        }

    }

    static class XmlEx
    {
        private const string COMMON_FACTION = "common";

        public static List<string> GetValues(this XmlDocument doc, string type, string faction)
        {
            var nodes = doc.LastChild!.ChildNodes;
            
            return nodes
                .Cast<XmlNode>()
                .Where(e => e.Attributes!["type"]!.Value == type)
                .Where(e =>
                {
                    var nodeFaction = e.Attributes!["faction"]!.Value;
                    return nodeFaction == COMMON_FACTION || nodeFaction == faction;
                })
                .Select(e => e.InnerText)
                .ToList();

        }
    }
}