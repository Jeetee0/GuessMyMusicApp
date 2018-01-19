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
            this.sshConnection = CommunicationPage.sshConnection;

            if (!sshConnection.Connected) {
                DisplayAlert("Alert", "Please connect to a device before sending a message.", "Ok");
                Navigation.PopAsync();
            }
        }

        async void handleExecute(object sender, EventArgs e) {
            string command = buildCommand();
            if (!command.Equals("")) {
                executeButton.IsEnabled = false;
                Task<string> task = sshConnection.RunCommandAsync(command);
                //task.Wait();
                //string msg = task.Result;
                await DisplayAlert("Successfully executed command", "not waiting for response message", "Ok");
                await Navigation.PopAsync();
                executeButton.IsEnabled = true;
            }
        }

        void handlePredefine(object sender, EventArgs e) {

            Button clickedButton = (Button)sender;
            if (clickedButton.Text == "empty") {
                commandEntry.Text = "";
                commandEntry2.Text = "";
                commandEntry3.Text = "";
            }
        } 

        private string buildCommand() {
            string command = "";
            if (!commandEntry.Text.Equals(""))
                command += commandEntry.Text;
            
            if (!commandEntry2.Text.Equals(""))
            {
                if (command.Equals(""))
                    command += commandEntry2.Text;
                else
                    command += " && " + commandEntry2.Text;
            }

            if (!commandEntry3.Text.Equals("")) {
                if (command.Equals(""))
                    command += commandEntry3.Text;
                else
                    command += " && " + commandEntry3.Text; 
            }

            return command;
        }
    }
}
