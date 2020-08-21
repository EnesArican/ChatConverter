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
        public List<string> EditQuoteLines(List<string> lines, List<(int quoteIndex, int commaCount)> quoteDetails)
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

            return newLines;
        }

        private void MakeQuoteLine(int index, int commaCount, List<string> lines, List<string> newLines)
        {
            var line = lines[index];

            var dash = "¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯";

            var datetimeString = SC.MsjBeginRegex.Match(line).Groups[0].Value;

            // Getting Quote
            var qtStrtPos = line.GetNthIndexFromEnd(',', commaCount + 2) + 2;
            var qtEndPos = line.LastIndexOf('[');
            var quote = line.Substring(qtStrtPos, (qtEndPos - qtStrtPos));


            // Trim Quote
            if (quote.Length >= 25)
            {
                var trimmedQuote = quote.Substring(0, 25);
                var quoteEndPos = trimmedQuote.LastIndexOf(' '); //== -1 ? 23 : trimmedQuote.LastIndexOf(' ');

                if (quoteEndPos != -1) {
                
                    quote = $"{trimmedQuote.Substring(0, quoteEndPos)} ...";
                }
                else
                {
                    Console.WriteLine(trimmedQuote + "     " + index);
                };


            }

            // Getting Message
            var msgPrt = SC.quoteDateRemovalRegex.Match(line).Groups[0].Value;

            var msgEndPos = msgPrt.GetNthIndexFromEnd(',', commaCount + 2) - 1;
            var msg = msgPrt.Substring(1, msgEndPos);

            var newQuoteLine = $"{datetimeString}{quote}";

            newLines.Add(newQuoteLine);
            newLines.Add(dash);
            newLines.Add(msg);
        }
    }
}
