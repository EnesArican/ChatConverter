
namespace ReportGenerator.Interfaces
{
    public interface IFileHandlerService
    {
        string FindChatFile();
        void SaveAndClose();
    }
}
