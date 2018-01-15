using System;
using System.Threading.Tasks;
using System.Collections.Generic;

using Xamarin.Forms;

using GuessMyMusic.MainPages;
using GuessMyMusic.Models;

namespace GuessMyMusic.PopUpPages
{
    public partial class RaspiAPICall : ContentPage
    {
        List<string> paramList = new List<string>();

        public RaspiAPICall()
        {
            InitializeComponent();
        }

        async public void handleSendRequest(object sender, EventArgs e) {
            if (ipEntry.Text.Equals("") || portEntry.Text.Equals("") || pathEntry.Text.Equals(""))
                await DisplayAlert("Alert", "Please enter values to ip, port and path entries", "Ok");
            else {
                sendButton.IsEnabled = false;

                //get params
                paramList.Clear();
                if (!paramEntry1.Text.Equals(""))
                    paramList.Add(paramEntry1.Text);
                if (!paramEntry2.Text.Equals(""))
                    paramList.Add(paramEntry2.Text);
                if (!paramEntry3.Text.Equals(""))
                    paramList.Add(paramEntry3.Text);
                if (!paramEntry4.Text.Equals(""))
                    paramList.Add(paramEntry4.Text);

                HTTPRequester request = new HTTPRequester(ipEntry.Text, portEntry.Text, pathEntry.Text, paramList);
                string response;

                try {
                    //if delay is set more then 500 the demo takes to long. so we wont wait for the response
                    if (TappingPage.avg >= 500 && pathEntry.Text.Equals("/controlRasPi/disco/wait"))
                        response = await request.SendRequest(false);
                    else
                        response = await request.SendRequest(true);
                    await DisplayAlert("HTTP Response", response, "Ok");
                } catch (TaskCanceledException tce) {
                    await DisplayAlert("Alert", "No response from server", "Ok");
                }

                sendButton.IsEnabled = true;
            }
        }

        public void handlePredefine(object sender, EventArgs e) {
            Button clickedButton = (Button)sender;
            if (clickedButton.Text.Equals("disco home")) {
                ipEntry.Text = "192.168.178.30";
            } else if (clickedButton.Text.Equals("disco uni")) {
                ipEntry.Text = "141.45.207.59";
            }
            portEntry.Text = "8080";
            pathEntry.Text = "/controlRasPi/disco/wait";
            paramEntry2.Text = "delay=" + TappingPage.avg;


        }
    }
}
