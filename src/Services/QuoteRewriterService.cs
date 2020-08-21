using System;
using System.Linq;
using ReportGenerator.Interfaces;
using System.Collections.Generic;
using SC = ReportGenerator.Constants.SystemConstant;
using ChatConverter.Utilities;

namespace ReportGenerator.Services
{
    public class QuoteRewriterService : IQuoteRewriterService
    {
        public void EditQuoteLines(List<string> lines, List<(int quoteIndex, int commaCount)> quoteDetails)
        {
            var newLines = new List<string>();

            List<int> quoteIndexes = quoteDetails.Select(x => x.quoteIndex).ToList();

            int i = 0;

            while (lines.Count > i)
            {
                if (quoteIndexes.Contains(i))
                {
                    var commaCount = quoteDetails.FirstOrDefault(x => x.quoteIndex == i).commaCount;
                    MakeQuoteLine(i, commaCount, lines, newLines);
                }
                else 
                { 
                    newLines.Add(lines[i]);
                }


                i++;
            }

            Console.WriteLine("total number of lines: " + i);
        }

        private void MakeQuoteLine(int index, int commaCount, List<string> lines, List<string> newLines)
        {
            var line = lines[index];

            var dash = "\n¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯\n";

            var datetimeString = SC.MsjBeginRegex.Match(line).Groups[0].Value;

            // Getting Quote
            var qtStrtPos = line.GetNthIndexFromEnd(',', commaCount + 2) + 2;
            var qtEndPos = line.LastIndexOf('[');
            var quote = line.Substring(qtStrtPos, (qtEndPos - qtStrtPos));


            // Trim Quote
            if (quote.Length >= 25)
            {
                var trimmedQuote = quote.Substring(0, 25);
                var quoteEndPos = trimmedQuote.LastIndexOf(' ') == -1 ? 23 : trimmedQuote.LastIndexOf(' ');

                quote = $"{trimmedQuote.Substring(0, quoteEndPos)} ...";
            }

            // Getting Message
            var msgPrt = SC.quoteDateRemovalRegex.Match(line).Groups[0].Value;

            var msgEndPos = msgPrt.GetNthIndexFromEnd(',', commaCount + 2) - 1;
            var msg = msgPrt.Substring(1, msgEndPos);

            var newLine = $"{datetimeString}{quote} {dash}{msg}";

            newLines.Add(newLine);
        }
    }
}
