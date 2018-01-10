using System;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using GuessMyMusic.Models;
using Xamarin.Forms;
using PCLStorage;

//todo: check if mycoolfolder is needed or wrong path?

namespace GuessMyMusic.Models
{
    public static class ReaderWriter
    {
        //static string genreFileName = DependencyService.Get<Models.IFileHelper>().GetLocalFilePath("genres.csv");
        //static string tapsFileName = DependencyService.Get<Models.IFileHelper>().GetLocalFilePath("taps.csv");
        static string genreFileName = "genres.csv";
        static string tapsFileName = "tapsFileName.csv";

        static IFolder rootFolder = FileSystem.Current.LocalStorage;

        async public static Task<List<Genre>> ReadInGenresAndReturnGenreList()
        {
            List<Genre> genreList = new List<Genre>();
            //IFolder myCoolFolder = await rootFolder.CreateFolderAsync("MyCoolFolder", CreationCollisionOption.OpenIfExists);
            string text = await ReadFileContent(genreFileName);

            string[] lines = text.Split('\n');
            for (int i = 0; i < lines.Length - 1; i++)
            {
                //skip empty lines
                if (lines[i] == "" || lines[i].StartsWith("\t"))
                    continue;
                
                string[] parts = lines[i].Split(';');
                genreList.Add(new Genre(parts[0], parts[1]));
            }
            return genreList;


        }

        public static async Task<bool> SaveListOfTaps(List<Tap> tapList) {
            ExistenceCheckResult exist = await rootFolder.CheckExistsAsync(tapsFileName);

            if (exist == ExistenceCheckResult.FileExists)
            {
                string text = "";
                IFile file = await rootFolder.GetFileAsync(tapsFileName);
                foreach (var tap in tapList)
                {
                    text += tap.Bpm + ";" + tap.Date + ";" + tap.Genre + ";" + tap.SetByUser + "\n";
                }
                await file.WriteAllTextAsync(text);
                return true;
            }
            return false;

        }

        public static async Task<List<Tap>> readInTapsAndReturnListOfTaps() {
            List<Tap> tapList = new List<Tap>();
            //IFolder myCoolFolder = await rootFolder.CreateFolderAsync("MyCoolFolder", CreationCollisionOption.OpenIfExists);
            string text = await ReadFileContent(tapsFileName);

            string[] lines = text.Split('\n');
            for (int i = 0; i < lines.Length - 1; i++)
            {
                //skip empty lines
                if (lines[i] == "" || lines[i].StartsWith("\t"))
                    continue;

                string[] parts = lines[i].Split(';');
                //structure of csv is same as constructor of Tap (parseble to: int, DateTime, string, bool)
                tapList.Add(new Tap(Int32.Parse(parts[0]), DateTime.Parse(parts[1]), parts[2], Boolean.Parse(parts[3])));
            }
            return tapList;
        }

        public static async Task<string> ReadFileContent(string fileName)
        {
            ExistenceCheckResult exist = await rootFolder.CheckExistsAsync(fileName);

            string text = null;
            if (exist == ExistenceCheckResult.FileExists)
            {
                IFile file = await rootFolder.GetFileAsync(fileName);
                text = await file.ReadAllTextAsync();
            }

            return text;
        }
    }
}
