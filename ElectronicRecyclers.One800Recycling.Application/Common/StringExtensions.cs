using System.Threading;
using System.Text.RegularExpressions;

namespace ElectronicRecyclers.One800Recycling.Application.Common
{
    public static class StringExtensions
    {
        public static string ToTitleCase(this string str)
        {
            return Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(str.ToLower());
        }

        public static string AddSpaceBeforeCapitalLetter(this string str)
        {
             return Regex.Replace(str, @"([a-z])([A-Z])", @"$1 $2", RegexOptions.None);
        }

        public static string ToEmailUserName(this string str)
        {
            return Regex.Replace(str, @"@\w*\.\w*", "");
        }
    }
}