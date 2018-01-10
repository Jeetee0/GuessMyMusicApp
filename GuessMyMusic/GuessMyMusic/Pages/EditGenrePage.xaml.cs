using System;
using System.Collections.Generic;
using GuessMyMusic.Models;

using Xamarin.Forms;

namespace GuessMyMusic.Pages
{
    public partial class EditGenrePage : ContentPage
    {
        int index;
        Tap currentTap;

        public Tap CurrentTap { get => currentTap; set => currentTap = value; }

        public EditGenrePage(int index, Tap selectedTap)
        {
            InitializeComponent();
            this.index = index;
            this.CurrentTap = selectedTap;
            infoLabel.Text = selectedTap.Bpm.ToString();
            genreTitle.Placeholder = selectedTap.Genre;
        }

        void OnSave(object sender, EventArgs e) 
        {
            string genre = genreTitle.Text;
            CurrentTap.Genre = genre;
            CurrentTap.SetByUser = true;
            HistoryPage.EditTapOfList(index, CurrentTap);
            Navigation.PopAsync();
        }

        void OnCancel(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }
    }
}
