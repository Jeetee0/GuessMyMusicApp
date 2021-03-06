﻿using System;
using System.Linq;
using System.Collections.Generic;
using GuessMyMusic.Models;

using Xamarin.Forms;

namespace GuessMyMusic.MainPages
{
    public partial class TappingPage : ContentPage
    {
        List<double> listOfIntervals = new List<double>();
        double elapsed;
        public static int avg;
        int bpm;
        DateTime lastDateTime = new DateTime();
        DateTime currentDateTime;
        bool showElapsed;

        public TappingPage()
        {
            InitializeComponent();
            saveButton.IsEnabled = false;

            var infoLabel_tap = new TapGestureRecognizer();
            infoLabel_tap.Tapped += (s, e) =>
            {
                showElapsed = !showElapsed;
            };
            infoLabel.GestureRecognizers.Add(infoLabel_tap);

        }



        void on_bpmButtonClicked(object sender, EventArgs e)
        {
            //get current time, calculate elapsed time
            currentDateTime = DateTime.Now;
            elapsed = (currentDateTime - lastDateTime).TotalMilliseconds;

            //activate save button after 5 clicks
            if (listOfIntervals.Count() >= 4) {
                saveButton.IsEnabled = true;
            }

            //reset
            if (elapsed >= 2000) {
                lastDateTime = DateTime.Now;
                bpmButton.Text = $"First Tap, \nplease Tap again.";
                listOfIntervals.Clear();
                saveButton.IsEnabled = false;
            }

            else {
                listOfIntervals.Add(elapsed);

                //calculate avg of list
                avg = Convert.ToInt32(listOfIntervals.Average());
                bpm = 60000 / avg;
                if (showElapsed)
                    bpmButton.Text = $"Elapsed Time: {Convert.ToInt32(avg)} ms\nBPM: {bpm}";
                else
                    bpmButton.Text = $"BPM: {bpm}";
                lastDateTime = currentDateTime;
            }
        }

        void on_saveButtonClicked(object sender, EventArgs e) {

            //save Tap to History
            DateTime currentDate = DateTime.Now;
            Tap newTap = new Tap(bpm, currentDate, "", false);
            HistoryPage.AddTapToList(newTap);
            saveButton.IsEnabled = false;
            listOfIntervals.Clear();
        }
    }
}
