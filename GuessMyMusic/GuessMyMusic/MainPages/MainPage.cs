﻿using System;
using Xamarin.Forms;

namespace GuessMyMusic.MainPages
{
    public class MainPage : TabbedPage
    {
        public MainPage()
        {
            Page tapPage, historyPage, genreListPage, sshControllingPage = null;
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
                        Title = "List of Genres"
                    };
                    sshControllingPage = new NavigationPage(new SshControllingPage())
                    {
                        Title = "ssh connection"
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
                        Title = "List of Genres"
                    };
                    sshControllingPage = new SshControllingPage()
                    {
                        Title = "ssh connection"
                    };
                    break;
            }

            Children.Add(tapPage);
            Children.Add(historyPage);
            Children.Add(genreListPage);
            Children.Add(sshControllingPage);

            Title = Children[0].Title;
        }

        protected override void OnCurrentPageChanged()
        {
            base.OnCurrentPageChanged();
            Title = CurrentPage?.Title ?? string.Empty;
        }
    }
}
