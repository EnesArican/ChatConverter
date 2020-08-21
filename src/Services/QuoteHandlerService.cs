using System;
using System.IO;
using System.Linq;
using System.Data;
using ReportGenerator.Interfaces;
using System.Collections.Generic;
using SC = ReportGenerator.Constants.SystemConstant;
using System.Text.RegularExpressions;

namespace ReportGenerator.Services
{
    public class QuoteHandlerService : IQuoteHandlerService
    {

        public int QuoteTotal { get; set; }
        public int MatchFoundTotal { get; set; }

        public List<int> GetQuoteIndexes(List<string> lines)  
        {
            return lines.Select((value, index) => new { value, index })
                    .Where(l => l.value.Contains(SC.QuoteIdentifier))
                    .Select(l => l.index).ToList();
        }

        public List<string> GetLines(string filePath) => File.ReadAllLines(filePath).ToList();

        public List<(int, int)> GetQuoteLineDetails(List<string> lines, List<int> quoteIndexes)
        {
            var quoteSection = String.Empty;

            var quoteDetails = new List<(int quoteIndex, int commaCount)>();

            foreach (var index in quoteIndexes)
            {
                QuoteTotal++;
                quoteSection = GetQuoteSection(lines[index]);
                FindQuoteInPreviousLines(quoteSection.Trim(), lines, index, quoteDetails);
            }

            Console.WriteLine($"Total number of quotes {QuoteTotal}");

            Console.WriteLine($"Total number of matches  {MatchFoundTotal}");

            return quoteDetails;
        }


        private string GetQuoteSection(string line) 
        {
            // remove quote identifier
            var quote = line.Substring(0, line.Length - 5);

            var strt = quote.LastIndexOf(",")+1;
            return quote.Substring(strt);
        }

        private void FindQuoteInPreviousLines(string quoteSection, List<string> lines, int index, List<(int, int)> quoteDetails) 
        {
            // get previous 30 lines from 
            var linesRange = GetFixedLines(lines.GetRange(index - 90, 90));

            int quoteFound = 0;
            List<string> matchedLines = new List<string>();
            foreach (var line in linesRange)
            {
                if (line.Contains(quoteSection)) 
                {
                    quoteFound++;
                    matchedLines.Add(line);
                }
            }

            if (quoteFound == 1 && !matchedLines.First().Contains("[-,-]")) 
            { 
                MatchFoundTotal++;
                var originalLine = matchedLines.First();
                var commaCount = GetCommaCount(originalLine, lines, index);
                quoteDetails.Add((index, commaCount));
            }
                
            //if (quoteFound == 0) 
            //    Console.WriteLine($"Warning: No match found for quote section: {quoteSection}");

            //if (quoteFound >= 1)
            //    Console.WriteLine($"Warning: mulitple matches found for quote section: {quoteSection}");
        }



        // fix up lines where if they don't startwith a date time pattern then attach to previous line
        private List<string> GetFixedLines(List<string> lines) 
        {

            var lineIndexes = lines.Select((value, index) => new { value, index })
                                   .Select(l => l.index).ToList();


            var fixedLines = new List<string>();
            string line = string.Empty;
            foreach (var index in lineIndexes)
            {
                line = lines[index];
                if (!SC.MsjBeginRegex.IsMatch(line) && index != 0) 
                {
                    var i = fixedLines.Count - 1;
                    fixedLines[i] = $"{fixedLines[i]} {line}"; 
                    
                    continue;
                }

                fixedLines.Add(line);
            }
            return fixedLines;
        }



        private int GetCommaCount(string originalLine, List<string> lines, int index) 
        {
            var plainOriginalLine = SC.quoteDateRemovalRegex.Match(originalLine).Groups[0].Value;

            // get number of commas in plain og line
            var commaCount = plainOriginalLine.Count(l => l == ',');
            
            // then make method to convert quote line to wanted format
            // se nunber of commas to get exact quote

            var quoteLine = lines[index];

            return commaCount;
        
        }


        

    }
}
