
using System.Collections.Generic;

namespace ReportGenerator.Interfaces
{
    public interface IFileHandlerService
    {
        string FindChatFile();
        void SaveLines(List<string> lines);
    }
}
