using System;

namespace GuessMyMusic.Models
{
    public interface IFileHelper
    {
        string GetLocalFilePath(string filename);
        string GetLocalRootFolder();
        void CopyFileToPersonalFolder(string fileName);
    }
}
