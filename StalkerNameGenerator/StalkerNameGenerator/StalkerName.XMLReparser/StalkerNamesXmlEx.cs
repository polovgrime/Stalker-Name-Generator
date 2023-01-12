using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StalkerName.XMLReparser
{
    static class StalkerNamesXmlEx
    {
        private static string ReplaceNumbers(this string source)
        {
            for (var i = 0; i < 10; i++)
            {
                source = source.ReplaceNumber(i);
            }
            return source;
        }

        public static string AdaptName(this string source)
        {
            var newStr = source.RemoveExtra();
            if (string.IsNullOrEmpty(newStr))
            {
                newStr = "common";
            }
            return newStr;
        }

        private static string RemoveExtra(this string source)
        {
            return source.ReplaceNumbers()
                .Replace("lname", string.Empty)
                .Replace("_", string.Empty)
                .Replace("name", string.Empty);
        }

        private static string ReplaceNumber(this string source, int number)
        {
            return source.Replace(number.ToString(), string.Empty);
        }
    }
}
