using System;
using System.Net;
using System.Threading.Tasks;
using System.Collections.Generic;

using Xamarin.Forms;

using GuessMyMusic.Models;

using Newtonsoft.Json;

namespace GuessMyMusic.PopUpPages
{
    public partial class GenreInfoPage : ContentPage
    {
        Genre genre;
        string genreInfo;
        List<string> interpreteList;
        //static string ip = "localhost";
        static string ip = "192.168.178.30";        //raspi home
        //static string ip = "141.45.207.59";       //raspi uni

        public GenreInfoPage(Genre genre)
        {
            InitializeComponent();
            this.genre = genre;
            SetLabels();
        }

        async private void SetLabels()
        {
            bpmLabel.Text = "BPM range: " + genre.BpmRange;
            genreInfoLabel.Text = genreInfo;

            //api call to get interprets for selected genre
            bool gotResponse = await GetInterprets();
            if (gotResponse)
            {
                foreach (var item in interpreteList)
                {
                    interpreteSL.Children.Add(new Label { Text = "- " + item });
                }
            }

            //api call to get infotext for selected genre
            gotResponse = await getGenreInfo();
            if (gotResponse)
                genreInfoLabel.Text = genreInfo;
            else
                genreInfoLabel.Text = "Got no response from Server.";
        }

        async private Task<bool> GetInterprets() {
            string path = "/controlRasPi/genres/interprets/" + genre.Name;
            try {
                HTTPRequester request = new HTTPRequester(ip, "8080", path, null);
                this.interpreteList = await request.SendRequestGetStringList();
                return true;
            } catch (Exception e) {
                return false;
            }
        }

        async private Task<bool> getGenreInfo() {
            string path = "/controlRasPi/genres/genreInfo/" + genre.Name;
            try
            {
                HTTPRequester request = new HTTPRequester(ip, "8080", path, null);
                genreInfo = await request.SendRequest(true);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
