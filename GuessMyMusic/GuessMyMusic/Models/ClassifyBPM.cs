using System;
using System.Collections.Generic;
using GuessMyMusic.Models;


//todo (easteregg) when having high bpm set genre to: you can tap fast

namespace GuessMyMusic.Models
{
    public static class ClassifyBPM
    {
        public static Tap ClassifyBpmValue(Tap currentTap) {
            List<Genre> genreList = MainPages.GenreOverviewPage.GenreList;
            int bpm = currentTap.Bpm;

            //easter egg
            if (bpm >= 500) {
                currentTap.Genre = "Well clicked, Sir (or Madam).";
                return currentTap;
            }

            string bestGenre = "";
            int difference;
            int smallestDifference = 100;

            foreach (var genre in genreList)
            {
                string[] array = genre.BpmRange.Replace(" ", string.Empty).Split('-');
                if (array.Length > 1) {
                    int lowerBpm = Int32.Parse(array[0]);
                    int upperBpm = Int32.Parse(array[1]);
                    int middle = (lowerBpm + upperBpm) / 2;
                    difference = middle - currentTap.Bpm;

                } else if (array.Length == 1) {
                    difference = Int32.Parse(array[0]) - currentTap.Bpm;
                } else {
                    difference = 100;
                }

                if (Math.Abs(difference) < smallestDifference)
                {
                    smallestDifference = Math.Abs(difference);
                    bestGenre = genre.Name;
                }
            }

            currentTap.Genre = bestGenre;
            return currentTap;
        }
    }
}
