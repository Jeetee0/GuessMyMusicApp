using System;
using System.Threading.Tasks;
using Xamarin.Forms;

using GuessMyMusic.Models;
using GuessMyMusic.MainPages;

namespace GuessMyMusic.PopUpPages
{
    public partial class SshCommandPage : ContentPage
    {
        ISshConnector sshConnection;

        public SshCommandPage()
        {
            InitializeComponent();
            this.sshConnection = SshControllingPage.sshConnection;

            if (!sshConnection.Connected) {
                DisplayAlert("Alert", "Please connect to a device before sending a message.", "Ok");
                Navigation.PopAsync();
            }
        }

        async void handleExecute(object sender, EventArgs e) {
            string command = commandEntry.Text;
            if (!command.Equals("")) {
                Task<string> task = sshConnection.RunCommandAsync(command);
                task.Wait();
                string msg = task.Result;
                await DisplayAlert("Successfully executed command", msg, "Ok");
            }
        }

        void handlePredefine(object sender, EventArgs e) {

            Button clickedButton = (Button)sender;
            if (clickedButton.Text.Equals("start disco")) {
                commandEntry.Text = "python print_frames.py --delay " + TappingPage.avg;
            }
        } 
    }
}
