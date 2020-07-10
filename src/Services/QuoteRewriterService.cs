using ReportGenerator.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

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

            Console.WriteLine(i);
        }

        private void MakeQuoteLine(int index, int commaCount, List<string> lines, List<string> newLines)
        {
            var line = lines[index];
        }
    }
}
