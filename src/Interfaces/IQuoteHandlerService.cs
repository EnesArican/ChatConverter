using System.Collections.Generic;


namespace ReportGenerator.Interfaces
{
    public interface IQuoteHandlerService
    {
        List<(int, int)> GetQuoteLineDetails(List<string> lines, List<int> quoteIndexes);

        List<int> GetQuoteIndexes(List<string> lines);

        List<string> GetLines(string filePath);
    }
}
