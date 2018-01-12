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
        ISshConnector sshConnection;
        public SshConnectPage()
        {
            InitializeComponent();
            this.sshConnection = SshControllingPage.sshConnection;
        }

        async void handleConnect(object sender, EventArgs e) {
            Task<string> task = sshConnection.Connect(hostEntry.Text, portEntry.Text, usernameEntry.Text, pwdEntry.Text);
            task.Wait();
            string msg = task.Result;
            await DisplayAlert("ssh Connection", msg, "Ok");
            //set parent object to value
            SshControllingPage.sshConnection = sshConnection;
            await Navigation.PopAsync();
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
