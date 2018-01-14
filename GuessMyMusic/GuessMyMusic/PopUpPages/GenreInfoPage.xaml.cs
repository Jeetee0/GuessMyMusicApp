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

        public GenreInfoPage(Genre genre, string wallOfText)
        {
            InitializeComponent();
            this.genre = genre;
            this.genreInfo = wallOfText;
            SetLabels();


            //get interprets from raspi
            //http://192.168.178.30:8080/controlOskarsRaspberryPi/requestInterprets/Progressive%20Trance
        }

        async private void SetLabels() {
            bpmLabel.Text = "BPM range: " + genre.BpmRange;
            genreInfoLabel.Text = genreInfo;
            bool gotResponse = await GetInterpretes();
            if (gotResponse) {
                foreach (var item in interpreteList)
                {
                    interpreteSL.Children.Add(new Label { Text= "- " + item });
                }   
            }
        }
        async private Task<bool> GetInterpretes() {
            string ip = "192.168.178.30";               //raspi home
            //string ip = "141.45.207.59";              //raspi uni
            string path = "/controlOskarsRaspberryPi/requestInterprets/" + genre.Name;

            try {
                HTTPRequester request = new HTTPRequester(ip, "8080", path, null);
                this.interpreteList = await request.SendRequestGetStringList();
                return true;
            } catch (Exception e) {
                return false;
            }
        }
    }
}
