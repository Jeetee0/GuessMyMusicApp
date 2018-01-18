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
        string prefixPath = "/controlRasPi/disco";
        string raspiUniIp;

        public RaspiAPICall()
        {
            InitializeComponent();
            getRaspiIp();
            delayEntry.Text = TappingPage.avg.ToString();
        }

        async void SendRequest(string path)
        {
            if (ipEntry.Text.Equals(""))
                await DisplayAlert("Alert", "Please enter a valid host before sending a request.", "Ok");
            else
            {
                EnableDisableButtons(false);

                string host = ipEntry.Text;
                string cycles = cyclesEntry.Text;
                string delay = delayEntry.Text;
                if (!cycles.Equals(""))
                    paramList.Add("cycles=" + cycles);
                if (!delay.Equals(""))
                    paramList.Add("delay=" + delay);

                HTTPRequester request = new HTTPRequester(ipEntry.Text, "8080", prefixPath + path, paramList);
                string response;

                try
                {
                    response = await request.SendRequest(true);
                    await DisplayAlert("HTTP Response", response, "Ok");
                }
                catch (TaskCanceledException tce)
                {
                    await DisplayAlert("Alert", "No response from server", "Ok");
                }

                EnableDisableButtons(true);
            }
            paramList.Clear();
        }


        public void handlePredefine(object sender, EventArgs e)
        {
            Button clickedButton = (Button)sender;
            if (clickedButton.Text.Equals("raspi home"))
            {
                ipEntry.Text = "192.168.178.30";
            }
            else if (clickedButton.Text.Equals("raspi uni"))
            {
                ipEntry.Text = raspiUniIp;
            }
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
            paramList.Add("mirror=True");
            SendRequest("/standard/wait");
        }

        async void getRaspiIp() {
            //if oskars raspi is starting it sends a request to youris server, stating its ip address
            //i can then get the ip of the raspi from youris server

            HTTPRequester client = new HTTPRequester("141.45.92.235", "8080", "/guessMyMusic/ip");
            raspiUniIp = await client.SendRequest(true);
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
    }
}
