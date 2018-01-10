using System;
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

        public Tap(int bpm, DateTime date, string genre, bool setByUser)
        {
            this.bpm = bpm;
            this.date = date;
            this.genre = genre;
            this.setByUser = setByUser;
        }


    }
}
