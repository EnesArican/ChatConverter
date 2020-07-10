using System.Collections.Generic;

namespace ReportGenerator.Interfaces
{
    public interface IQuoteRewriterService
    {
        void EditQuoteLines(List<string> lines, List<(int quoteIndex, int commaCount)> quoteDetails);
    }
}
