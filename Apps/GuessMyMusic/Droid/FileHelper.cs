using System;
using System.IO;
using GuessMyMusic;
using GuessMyMusic.Droid;
using Java.IO;

[assembly: Xamarin.Forms.Dependency(typeof(FileHelper))]
namespace GuessMyMusic.Droid
{
    public class FileHelper : Models.IFileHelper
    {
        public FileHelper()
        {
        }

        public string GetLocalFilePath(string filename)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string bla = Path.Combine(path, filename);
            CopyFileToPersonalFolder(bla);

            return bla;
        }

        public string GetLocalRootFolder()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.Personal);
        }

        public void CopyFileToPersonalFolder(string fileName)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string bla = Path.Combine(path, fileName);

            using (var br = new BinaryReader(Android.App.Application.Context.Assets.Open(fileName)))
            {
                using (var bw = new BinaryWriter(new FileStream(bla, FileMode.Create)))
                {
                    byte[] buffer = new byte[2048];
                    int length = 0;
                    while ((length = br.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        bw.Write(buffer, 0, length);
                    }
                }
            }
        }
    }
}
