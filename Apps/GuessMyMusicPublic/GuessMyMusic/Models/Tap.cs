using System;
using Xamarin.Forms;

namespace GuessMyMusic.Models
{
    public class Tap
    {
        private int bpm;
        private DateTime date;
        private String genre;
        private bool setByUser;

        public int Bpm { get => bpm; set => bpm = value; }
        public DateTime Date { get => date; set => date = value; }
        public string Genre { get => genre; set => genre = value; }
        public bool SetByUser { get => setByUser; set => setByUser = value; }

        private Color red = Color.FromHex("#A00000");
        private Color white = Color.FromHex("#000000");

        //bgcolor is defined through bool setByUser
        //if my algorithm has set the genre it gets displayed red
        //if the user has set the genre himself it gets displayed black
        public Color BgColor {
            get
            {
                if (SetByUser)
                    return white;
                return red; 
            }
        }


        public Tap(int bpm, DateTime date, string genre, bool setByUser)
        {
            this.bpm = bpm;
            this.date = date;
            this.genre = genre;
            this.setByUser = setByUser;
        }



    }
}
