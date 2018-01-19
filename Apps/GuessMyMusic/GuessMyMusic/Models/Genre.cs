using System;
namespace GuessMyMusic.Models
{
    public class Genre
    {
        private string name;
        private string bpmRange;

        public string BpmRange { get => bpmRange; set => bpmRange = value; }
        public string Name { get => name; set => name = value; }

        public Genre(string name, string bpmRange)
        {
            this.Name = name;
            this.BpmRange = bpmRange;
        }
    }
}
