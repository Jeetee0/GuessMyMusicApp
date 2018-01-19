using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using GuessMyMusic.Models;
using System.Threading.Tasks;
using PCLStorage;

using Xamarin.Forms;

namespace GuessMyMusic.MainPages
{
    public partial class HistoryPage : ContentPage
    {
        static ObservableCollection<Tap> tapsList = new ObservableCollection<Tap>();
        static IFolder rootFolder = FileSystem.Current.LocalStorage;
        static string tapsFileName = "taps.csv";
        static string infoMessage = "Click on one of your Taps to edit it.";

        public static ObservableCollection<Tap> TapsList { get => tapsList; set => tapsList = value; }

        //todo: save taps when closing app?
        //or save always when new tap is added or modified

        public HistoryPage()
        {
            InitializeComponent();
            ReadTaps();
            infoLabel.Text = infoMessage;
        }

        async void ReadTaps() {
            //await DeleteCurrentTapsFile();
            TapsList = await readInTapsAndReturnListOfTaps();
            this.tapsListView.ItemsSource = TapsList;

        }

        async void Handle_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
            {
                return; //ItemSelected is called on deselection, which results in SelectedItem being set to null
            }

            Tap selectedTap = (Tap) e.SelectedItem;
            int index = TapsList.IndexOf((Tap)tapsListView.SelectedItem);
            string action = await DisplayActionSheet("Which Action should be executed?", "Cancel", null, "Edit", "Delete");

            if (action == "Edit") {
                //open editpage to edit genre
                Page editPage = new PopUpPages.EditYourTapPage(index, selectedTap) { Title = "Edit your tap"};
                await Navigation.PushAsync(editPage);
            } else if (action == "Delete") {
                var response = await DisplayAlert("Delete saved Tap?", "Are you sure to delete your Tap with BPM value: " + selectedTap.Bpm + "?", "Delete", "Cancel");
                if (response) {
                    tapsList.Remove(selectedTap);
                    await SaveListOfTaps();
                }
            }

            //disable selected item
            ((ListView)sender).SelectedItem = null;
        }

        async Task<ObservableCollection<Tap>> readInTapsAndReturnListOfTaps()
        {
            ObservableCollection<Tap> tapList = new ObservableCollection<Tap>();
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

        async static Task<bool> SaveListOfTaps()
        {
            ExistenceCheckResult exist = await rootFolder.CheckExistsAsync(tapsFileName);

            //empty current file so taps does not get doubled
            if (exist == ExistenceCheckResult.FileExists)
                await DeleteCurrentTapsFile();

            //generate complete string and write into file
            string text = "";
            IFile file = await rootFolder.GetFileAsync(tapsFileName);
            foreach (var tap in TapsList)
            {
                text += tap.Bpm + ";" + tap.Date + ";" + tap.Genre + ";" + tap.SetByUser + ";\n";
            }
            await file.WriteAllTextAsync(text);
            return true;

        }

        async Task<string> ReadFileContent(string fileName)
        {
            ExistenceCheckResult exist = await rootFolder.CheckExistsAsync(fileName);
            //DependencyService.Get<Models.IFileHelper>().CopyFileToPersonalFolder(tapsFileName);

            string text = null;
            if (exist == ExistenceCheckResult.NotFound) {
                DependencyService.Get<Models.IFileHelper>().CopyFileToPersonalFolder(tapsFileName);
                exist = await rootFolder.CheckExistsAsync(fileName);
            }
            if (exist == ExistenceCheckResult.FileExists)
            {
                IFile file = await rootFolder.GetFileAsync(fileName);
                text = await file.ReadAllTextAsync();
            }

            return text;
        }

        async static Task<bool> DeleteCurrentTapsFile()
        {
            ExistenceCheckResult exist = await rootFolder.CheckExistsAsync(tapsFileName);
            if (exist == ExistenceCheckResult.FileExists)
            {
                //overwriting file with nothing
                IFile file = await rootFolder.GetFileAsync(tapsFileName);
                await file.WriteAllTextAsync("");
                return true;
            }
            return false;
        }

        public static async void AddTapToList(Tap newTap) {
            newTap = ClassifyBPM.ClassifyBpmValue(newTap);
            TapsList.Add(newTap);
            await SaveListOfTaps();
        }

        public static async void EditTapOfList(int index, Tap newTap) {
            TapsList[index] = newTap;
            await SaveListOfTaps();
        }



    }
}
