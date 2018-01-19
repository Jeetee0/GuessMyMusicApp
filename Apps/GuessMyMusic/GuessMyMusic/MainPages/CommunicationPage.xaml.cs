using System;
using System.Collections.Generic;

using Xamarin.Forms;

using GuessMyMusic.PopUpPages;
using GuessMyMusic.Models;

namespace GuessMyMusic.MainPages
{
    public partial class CommunicationPage : ContentPage
    {
        public static ISshConnector sshConnection;

        public CommunicationPage()
        {
            InitializeComponent();
            sshConnection = DependencyService.Get<Models.ISshConnector>();
        }

        async void handleConnect(object sender, EventArgs e)
        {
            if (sshConnection.Connected) {
                await DisplayAlert("Alert", "Please disconnect from your device before connecting to a new one.", "Ok");
            }
            else {
                Page connectPage = new SshConnectPage { Title = "Connect via ssh" };
                await Navigation.PushAsync(connectPage);
            } 

        }

        async void handleSendCommand(object sender, EventArgs e)
        {
            if (sshConnection.Connected)
            {
                Page commandPage = new SshCommandPage() { Title = "Send command via ssh" };
                await Navigation.PushAsync(commandPage);
            }
            else
                await DisplayAlert("Alert", "Please connect to a device before executing a command.", "Ok");
        }

        async void handleDisconnect(object sender, EventArgs e)
        {
            if (sshConnection.Connected)
            {
                sshConnection.Disconnect();
                sshConnection = DependencyService.Get<Models.ISshConnector>();
                await DisplayAlert("Alert", "Disconnected from" + Environment.NewLine + sshConnection.ToString(), "Ok");
            }
            else {
                await DisplayAlert("Alert", "Not connected to a device.", "Ok");
            } 

        }

        async void handleAPICall(object sender, EventArgs e)
        {
            Page apiCallPage = new RaspiAPICall() { Title = "Send request via HTTP" };
            await Navigation.PushAsync(apiCallPage);
            //await HTTPRequester.RegisterRequest();
        }
    }
}
