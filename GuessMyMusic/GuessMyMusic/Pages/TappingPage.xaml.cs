using System;
using System.Linq;
using System.Collections.Generic;
using GuessMyMusic.Models;

using Xamarin.Forms;

namespace GuessMyMusic.Pages
{
    public partial class TappingPage : ContentPage
    {
        List<double> listOfIntervals = new List<double>();
        double elapsed;
        int avg;
        int bpm;
        DateTime lastDateTime = new DateTime();
        DateTime currentDateTime;

        public TappingPage()
        {
            InitializeComponent();
            saveButton.IsEnabled = false;
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
                //bpmButton.Text = $"Elapsed Time: {Convert.ToInt32(elapsed)} ms\nBPM: {bpm}";
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
