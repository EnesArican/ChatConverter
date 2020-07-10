using ReportGenerator.Interfaces;
using Syroot.Windows.IO;
using System;
using System.IO;
using System.Linq;
using SC = ReportGenerator.Constants.SystemConstant;

namespace ReportGenerator.Services
{
    public class DataReaderService : IDataReaderService
    {
       
        
        public string FindChatFile()
        {
            string downloadsPath = KnownFolders.Downloads.DefaultPath;
            Console.WriteLine("Downloads folder path: " + downloadsPath);

            var directory = new DirectoryInfo($"{downloadsPath}\\files");
            var myFile = directory.GetFiles(SC.ChatFileName).OrderByDescending(f => f.LastWriteTime).First();
            Console.WriteLine("file you are looking for: " + myFile);

            return myFile.FullName;
        }

       

        public void SaveAndClose()
        {
            //workbook.SaveAs(@"C:\Temp\test.xlsx", Local: true); ;
            //workbook.Close();
            //ExcelApp.Quit();
        }
    }
}
