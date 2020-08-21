

using System.Text.RegularExpressions;

namespace ReportGenerator.Constants
{
    public static class SystemConstant
    {
        public const string ChatFileName = "Chat From A.txt";

        public const string QuoteIdentifier = "[-,-]";

        public static Regex MsjBeginRegex = new Regex(@"\d.*,\s\d.*\s-\s.*:\s");

        public static Regex quoteDateRemovalRegex = new Regex(@"(?<=\d\d\:\d\d\s\-\s(Aysen|Enes)\:)\s.*");

    }
}
