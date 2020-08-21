using ReportGenerator.Interfaces;
using System.Collections.Generic;

namespace ReportGenerator
{
    public class ConsoleApp
    {
        private readonly IQuoteHandlerService _dataReader;
        private readonly IFileHandlerService _fileHandler;
        private readonly IQuoteRewriterService _dataWriter;


        public ConsoleApp(IQuoteHandlerService dataReader,
                          IFileHandlerService fileHandler,
                          IQuoteRewriterService dataWriter) 
        {
            _dataReader = dataReader;
            _fileHandler = fileHandler;
            _dataWriter = dataWriter;
        }

        public void Run() 
        {
            var filePath = _fileHandler.FindChatFile();
            var lines = _dataReader.GetLines(filePath);

            var quoteIndexes = _dataReader.GetQuoteIndexes(lines);

            List<(int quoteIndex, int commaCount)> quoteDetails  = _dataReader.GetQuoteLineDetails(lines, quoteIndexes);

            _dataWriter.EditQuoteLines(lines, quoteDetails);

        }
    }
}
