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
        public SshConnectPage()
        {
            InitializeComponent();
            if (!CommunicationPage.sshConnection.Connected)
                connectButton.IsEnabled = true;
        }

        async void handleConnect(object sender, EventArgs e) {
            if (hostEntry.Text.Equals("") || portEntry.Text.Equals("") || usernameEntry.Text.Equals("") || pwdEntry.Text.Equals(""))
                await DisplayAlert("Alert", "Please enter values to every entry before trying to connect.", "Ok");
            else {
                connectButton.IsEnabled = false;
                Task<string> task = CommunicationPage.sshConnection.Connect(hostEntry.Text, portEntry.Text, usernameEntry.Text, pwdEntry.Text);
                task.Wait();
                string msg = task.Result;
                await DisplayAlert("ssh Connection", msg, "Ok");

                //if successfull: close page, else enable button again
                if (msg.StartsWith("Successfully"))
                    await Navigation.PopAsync();
                else {
                    connectButton.IsEnabled = true;
                }
            }

        }

        void handlePredefine(object sender, EventArgs e) {
            //optional: read from file (also with passwords)
            Button clickedButton = (Button)sender;
            if (clickedButton.Text.Equals("Raspi Home")) {
                hostEntry.Text = "192.168.178.30";
                portEntry.Text = "22";
                usernameEntry.Text = "pi";
                pwdEntry.Text = "";
            } else if (clickedButton.Text.Equals("Raspi Uni")) {
                hostEntry.Text = "141.45.207.59";
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
    }
}
