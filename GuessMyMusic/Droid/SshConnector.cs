using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using GuessMyMusic.Models;
using GuessMyMusic.Droid;

using Rebex.Net;
using Rebex;
using Rebex.TerminalEmulation;

[assembly: Xamarin.Forms.Dependency(typeof(SshConnector))]
namespace GuessMyMusic.Droid
{
    public class SshConnector : ISshConnector
    {
        Ssh ssh;
        bool connected;
        string _host = "192.168.178.30";
        string _port = "22";
        string _username = "pi";
        string _pwd = "rofl";

        string cdToProject = "cd Projects/FrameModifier9001/FrameViewer";
        //string makeDir = "mkdir testFolder";
        string executePython = "python print_frames.py";
        string param;

        public bool Connected { get => connected; set => connected = value; }

        public Task<string> Connect(string host, string port, string username, string pwd)
        {
            //free trial key for 30 days
            Licensing.Key = "==AbTRohGmy5pJ7EGwC/gd6w8DlB3dEzyAdKfMRmPAPZcM==";

            ssh = new Ssh();
            _host = host;
            _port = port;
            _username = username;
            _pwd = pwd;

            return Task.Run<string>(() =>
            { 
                string result;
                try
                {
                    ssh.ConnectAsync(host, port).Wait();
                    ssh.LoginAsync(username, pwd).Wait();
                    result = "Successfully connected to" + System.Environment.NewLine + host;
                    Connected = true;
                }
                catch (Exception ex)
                {
                    result = "* Error:" + System.Environment.NewLine + ex;
                }
                return result;
            });

        }

        public Task<string> RunCommandAsync(string command)
        {
            return Task.Run<string>(() =>
            {
                string result;
                try
                {
                    Scripting scripting = ssh.StartScripting(command);
                    //wait 2 secs
                    scripting.Timeout = 2000;
                    result = scripting.ReadUntil(ScriptEvent.Closed);
                    if (result == null || result.Length == 0)
                        result = "No response";
                }
                catch (Exception ex)
                {
                    result = "* Error:" + System.Environment.NewLine + ex;
                }
                return result;
            });

        }

        public void Disconnect() {
            ssh.Disconnect();
        }

        public override string ToString() {
            return _host;
        }



    }
}
