using System;
using System.Net;
using System.Net.Http;
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
        string prefixPath = "/controlRasPi/disco";

        public RaspiAPICall()
        {
            InitializeComponent();
            delayEntry.Placeholder = TappingPage.avg.ToString();
        }

        async void SendRequest(string path)
        {
            string host = ipEntry.Text;
            if (host.Equals("") || HostContainsLetters(host))
                await DisplayAlert("Alert", "Please enter a valid host before sending a request.", "Ok");
            else
            {
                EnableDisableButtons(false);

                string cycles = cyclesEntry.Text;
                string delay = delayEntry.Text;
                if (!cycles.Equals(""))
                    paramList.Add("cycles=" + cycles);
                if (!delay.Equals(""))
                    paramList.Add("delay=" + delay);

                HTTPRequester request = new HTTPRequester(host, "8080", prefixPath + path, paramList);
                string response;

                try {
                    response = await request.SendRequest(true);
                    await DisplayAlert("HTTP Response", response, "Ok");
                } catch (TaskCanceledException tce){
                    await DisplayAlert("Alert", "No response from server.", "Ok");
                } catch (Exception e) {
                    await DisplayAlert("Alert", "Could not connect to server.", "Ok");
                }

                EnableDisableButtons(true);
            }
            paramList.Clear();
        }

        public void handleWalkingLight(object sender, EventArgs e)
        {
            Button clickedButton = (Button)sender;
            paramList.Add("start=" + clickedButton.Text);
            SendRequest("/lauflicht");
        }

        public void handlePredefinePatterns(object sender, EventArgs e)
        {
            Button clickedButton = (Button)sender;
            string text = clickedButton.Text;
            // add the right parameter "filename" to start the chosen script
            if (text.Equals("Big tunnel in"))
                paramList.Add("filename=BigTunnelIn.txt");
            else if (text.Equals("Building lines"))
                paramList.Add("filename=BuildingLinesStartingBottomRight.txt");
            else if (text.Equals("4 tunnel"))
                paramList.Add("filename=TunnelSchnellHinUndHer.txt");
            else {
                //Matrix
                paramList.Add("filename=Matrix.txt");
            }
            // add more parameters
            paramList.Add("mirror=True");
            if (!msgEntry.Text.Equals(""))
                paramList.Add("msg=" + msgEntry.Text);

            SendRequest("/standard/wait");
        }

        void EnableDisableButtons(bool enable) {
            pattern1Button.IsEnabled = enable;
            pattern2Button.IsEnabled = enable;
            pattern3Button.IsEnabled = enable;
            pattern4Button.IsEnabled = enable;
            leftButton.IsEnabled = enable;
            rightButton.IsEnabled = enable;
            topButton.IsEnabled = enable;
            bottomButton.IsEnabled = enable;
        }

        bool HostContainsLetters(string host) {
            foreach (var letter in host)
            {
                if (Char.IsLetter(letter))
                    return true;
            }
            return false;
        }
    }
}
