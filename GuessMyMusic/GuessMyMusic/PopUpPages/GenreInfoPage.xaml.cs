using System;
using System.Collections.Generic;

using Xamarin.Forms;

using GuessMyMusic.Models;

namespace GuessMyMusic.PopUpPages
{
    public partial class GenreInfoPage : ContentPage
    {
        public GenreInfoPage(Genre genre, string wallOfText)
        {
            InitializeComponent();
            bpmLabel.Text = "BPM range: " + genre.BpmRange;
            genreInfoLabel.Text = "Extracted from Wikipedia:\n\n" + wallOfText;
        }
    }
}
