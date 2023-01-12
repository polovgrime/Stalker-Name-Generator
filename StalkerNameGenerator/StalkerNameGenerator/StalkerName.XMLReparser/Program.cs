using System.Text;

namespace StalkerName.XMLReparser
{
    public class Program
    {
        private const string SAVE_PATH = @".\";
        private const string FIRST_NAMES = @".\stable_generate_fnames.xml";
        private const string LAST_NAMES = @".\stable_generate_snames.xml";

        public static void Main(string[] args)
        {
            if (Directory.Exists(SAVE_PATH) == false)
            {
                Directory.CreateDirectory(SAVE_PATH);
            }

            var reparser = new Reparser(SAVE_PATH, FIRST_NAMES, LAST_NAMES);
            reparser.Start();
        }
    }
}