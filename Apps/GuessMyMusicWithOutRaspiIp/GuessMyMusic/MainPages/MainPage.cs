using System;
using Xamarin.Forms;

namespace GuessMyMusic.MainPages
{
    public class MainPage : TabbedPage
    {
        public MainPage()
        {
            Page tapPage, historyPage, genreListPage, communicationPage = null;
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
                    genreListPage = new NavigationPage(new GenreOverviewPage())
                    {
                        Title = "Genres Overview"
                    };
                    communicationPage = new NavigationPage(new CommunicationPage())
                    {
                        Title = "Connect"
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
                    genreListPage = new GenreOverviewPage()
                    {
                        Title = "Genres Overview"
                    };
                    communicationPage = new CommunicationPage()
                    {
                        Title = "Connect"
                    };
                    break;
            }

            Children.Add(tapPage);
            Children.Add(historyPage);
            Children.Add(genreListPage);
            Children.Add(communicationPage);

            Title = Children[0].Title;
        }

        protected override void OnCurrentPageChanged()
        {
            base.OnCurrentPageChanged();
            Title = CurrentPage?.Title ?? string.Empty;
        }
    }
}

