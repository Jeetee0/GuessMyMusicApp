using System;
using System.Collections.Generic;

using Xamarin.Forms;

using GuessMyMusic.PopUpPages;

namespace GuessMyMusic.MainPages
{
    public partial class SshControllingPage : ContentPage
    {
        public static Models.ISshConnector sshConnection;


        public SshControllingPage()
        {
            InitializeComponent();
            sshConnection = DependencyService.Get<Models.ISshConnector>();
        }

        async void handleConnect(object sender, EventArgs e)
        {
            Page connectPage = new SshConnectPage { Title = "Connect via ssh" };
            await Navigation.PushAsync(connectPage);
        }

        async void handleSendCommand(object sender, EventArgs e)
        {
            if (sshConnection.Connected) {
                Page commandPage = new SshCommandPage() { Title = "Send command via ssh" };
                await Navigation.PushAsync(commandPage);
            } 
            else
                await DisplayAlert("Alert", "Please connect to a device before executing a command.", "Ok");
        }

        void handleDisconnect(object sender, EventArgs e)
        {
            sshConnection.Disconnect();
            DisplayAlert("Alert", "Disconnected from" + Environment.NewLine + sshConnection.ToString(), "Ok");
            Navigation.PopAsync();
        }
    }
}
