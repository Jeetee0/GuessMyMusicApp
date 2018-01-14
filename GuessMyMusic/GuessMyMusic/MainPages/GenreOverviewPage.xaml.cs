using System;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using GuessMyMusic.Models;
using PCLStorage;

using Xamarin.Forms;

using GuessMyMusic.PopUpPages;

namespace GuessMyMusic.MainPages
{
    public partial class GenreOverviewPage : ContentPage
    {
        private static List<Genre> genreList = new List<Genre>();
        public static Dictionary<string, string> genreInfos = new Dictionary<string, string>();
        public static List<Genre> GenreList { get => genreList; set => genreList = value; }
        static string genresFileName = "genres.csv";
        static string genreInfoFilename = "genreInfos.csv";
        static string infoMessage = "Note that normally only electronic music genres can be classified into specific bpm ranges.\nBecause of that all other genres are not listed in here.";


        public GenreOverviewPage()
        {
            InitializeComponent();
            ReadGenres();

        }

        async void Handle_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
            {
                return; //ItemSelected is called on deselection, which results in SelectedItem being set to null
            }

            //get selected item and open page to give infos about page
            Genre selectedGenre = (Genre)e.SelectedItem;
            string genreInfo = genreInfos[selectedGenre.Name];
            Page genreInfoPage = new GenreInfoPage(selectedGenre, genreInfo) { Title = selectedGenre.Name };
            await Navigation.PushAsync(genreInfoPage);

            //disable selected item
            ((ListView)sender).SelectedItem = null;
        }

        async public void ReadGenres()
        {
            //i cant call method from ReaderWriter class. 
            //it did not wait until async reading is finished. so i have to keep the methods in this class
            GenreList = await ReadInGenresAndReturnGenreList();
            genreInfos = await ReadInGenreInfos();
            genresListView.ItemsSource = GenreList;
        }

        async public static Task<List<Genre>> ReadInGenresAndReturnGenreList()
        {
            List<Genre> genreList = new List<Genre>();
            //IFolder myCoolFolder = await rootFolder.CreateFolderAsync("MyCoolFolder", CreationCollisionOption.OpenIfExists);
            string text = await ReadFileContent(genresFileName);
            string[] lines = text.Split('\n');
            for (int i = 0; i < lines.Length - 1; i++)
            {
                //skip empty lines
                if (lines[i] == "" || lines[i].StartsWith("\t") || lines[i].StartsWith("\r"))
                    continue;

                string[] parts = lines[i].Split(';');
                genreList.Add(new Genre(parts[0], parts[1]));
            }
            return genreList;
        }

        async private static Task<Dictionary<string, string>> ReadInGenreInfos() {
            
            Dictionary<string, string> genreInfos = new Dictionary<string, string>();
            string text = await ReadFileContent(genreInfoFilename);
            string[] lines = text.Split('\n');
            for (int i = 0; i < lines.Length - 1; i++)
            {
                //skip empty lines
                if (lines[i] == "" || lines[i].StartsWith("\t") || lines[i].StartsWith("\r"))
                    continue;

                string[] parts = lines[i].Split(';');
                genreInfos.Add(parts[0], parts[1]);
            }
            return genreInfos;
        }

        public static async Task<string> ReadFileContent(string fileName)
        {
            IFolder rootFolder = FileSystem.Current.LocalStorage;
            ExistenceCheckResult exist = await rootFolder.CheckExistsAsync(fileName);
            DependencyService.Get<Models.IFileHelper>().CopyFileToPersonalFolder(fileName);

            string text = null;
            if (exist == ExistenceCheckResult.NotFound)
            {
                DependencyService.Get<Models.IFileHelper>().CopyFileToPersonalFolder(fileName);
                exist = await rootFolder.CheckExistsAsync(fileName);
            }
            if (exist == ExistenceCheckResult.FileExists)
            {
                IFile file = await rootFolder.GetFileAsync(fileName);
                text = await file.ReadAllTextAsync();
            }

            return text;
        }
    }
}

