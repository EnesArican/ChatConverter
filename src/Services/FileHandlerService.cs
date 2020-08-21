using ReportGenerator.Interfaces;
using Syroot.Windows.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using SC = ReportGenerator.Constants.SystemConstant;

namespace ReportGenerator.Services
{
    public class FileHandlerService : IFileHandlerService
    {
       public readonly string downloadsPath = KnownFolders.Downloads.DefaultPath;

        public string FindChatFile()
        {
            Console.WriteLine("Downloads folder path: " + downloadsPath);

            var directory = new DirectoryInfo($"{downloadsPath}\\files");
            var myFile = directory.GetFiles(SC.ChatFileName).OrderByDescending(f => f.LastWriteTime).First();
            Console.WriteLine("file you are looking for: " + myFile);

            return myFile.FullName;
        }

       

        public void SaveLines(List<string> lines)
        {
            File.WriteAllLines($"{downloadsPath}\\files\\WriteText.txt", lines);
        }
    }
}
