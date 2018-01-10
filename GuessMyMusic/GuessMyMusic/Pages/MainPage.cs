using System;
using Xamarin.Forms;

namespace GuessMyMusic.Pages
{
    public class MainPage : TabbedPage
    {
        public MainPage()
        {
            Page tapPage, historyPage, genreListPage = null;
            NavigationPage.SetHasNavigationBar(this, false);

            switch (Device.RuntimePlatform)
            {
                case Device.iOS:
                    tapPage = new NavigationPage(new TappingPage())
                    {
                        Title = "Tap for BPM"
                    };

                    historyPage = new NavigationPage(new HistoryPage())
                    {
                        Title = "History of Taps"
                    };
                    genreListPage = new NavigationPage(new GenreListPage())
                    {
                        Title = "List of Genres"
                    };
                    break;
                default:
                    tapPage = new TappingPage()
                    {
                        Title = "Tap for BPM"
                    };

                    historyPage = new HistoryPage()
                    {
                        Title = "History of Taps"
                    };
                    genreListPage = new GenreListPage()
                    {
                        Title = "List of Genres"
                    };
                    break;
            }

            Children.Add(tapPage);
            Children.Add(historyPage);
            Children.Add(genreListPage);

            Title = Children[0].Title;
        }

        protected override void OnCurrentPageChanged()
        {
            base.OnCurrentPageChanged();
            Title = CurrentPage?.Title ?? string.Empty;
        }
    }
}

