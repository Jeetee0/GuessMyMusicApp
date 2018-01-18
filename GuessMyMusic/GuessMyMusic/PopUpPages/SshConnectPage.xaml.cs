using System;
using System.Collections.Generic;
using GuessMyMusic.Models;
using System.Threading.Tasks;

using Xamarin.Forms;

using GuessMyMusic.MainPages;

namespace GuessMyMusic.PopUpPages
{
    public partial class SshConnectPage : ContentPage
    {
        string raspiUniIp = "";

        public SshConnectPage()
        {
            InitializeComponent();
            if (!CommunicationPage.sshConnection.Connected)
                connectButton.IsEnabled = true;
            getRaspiIp();
        }

        async void handleConnect(object sender, EventArgs e) {
            string response = "";
            if (hostEntry.Text.Equals("") || HostContainsLetters(hostEntry.Text) || portEntry.Text.Equals("") || usernameEntry.Text.Equals("") || pwdEntry.Text.Equals(""))
                await DisplayAlert("Alert", "Please enter valid values to every entry before trying to connect.", "Ok");
            else {
                connectButton.IsEnabled = false;
                try {
                    Task<string> task = CommunicationPage.sshConnection.Connect(hostEntry.Text, portEntry.Text, usernameEntry.Text, pwdEntry.Text);
                    task.Wait();
                    response = task.Result;
                    await DisplayAlert("ssh Connection", response, "Ok");
                } catch (Exception ex) {
                    await DisplayAlert("ssh connection", "Could not connect to device.", "Ok");
                }


                //if successfull: close page, else enable button again
                if (response.StartsWith("Successfully"))
                    await Navigation.PopAsync();
                else {
                    connectButton.IsEnabled = true;
                }
            }

        }

        void handlePredefine(object sender, EventArgs e) {
            //optional: read from file (also with passwords)
            Button clickedButton = (Button)sender;
            if (clickedButton.Text.Equals("Oskars Raspi")) {
                hostEntry.Text = raspiUniIp;
                portEntry.Text = "22";
                usernameEntry.Text = "pi";
                pwdEntry.Text = "";
            } else {
                //youris server
                hostEntry.Text = "141.45.92.235";
                portEntry.Text = "22";
                usernameEntry.Text = "student";
                pwdEntry.Text = "";
            }
        }

        async void getRaspiIp()
        {
            //if oskars raspi is starting it sends a request to youris server, stating its ip address
            //i can then get the ip of the raspi from youris server

            HTTPRequester client = new HTTPRequester("141.45.92.235", "8080", "/guessMyMusic/ip");
            raspiUniIp = await client.SendRequest(true);
        }

        bool HostContainsLetters(string host)
        {
            foreach (var letter in host)
            {
                if (Char.IsLetter(letter))
                    return true;
            }
            return false;
        }
    }
}
