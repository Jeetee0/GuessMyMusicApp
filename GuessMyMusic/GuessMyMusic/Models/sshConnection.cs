using System;
using Tamir.SharpSsh;


namespace GuessMyMusic.Models
{
    public class sshConnection
    {
        static string host = "192.168.178.30";
        static string user = "pi";
        static string pwd = "rofl";

        public sshConnection()
        {
        }

        public static void startDisco(int avg) {

            string cdToProject = "cd Projects/FrameModifier9001/FrameViewer";
            string makeDir = "mkdir testFolder";
            string executePython = "python print_frames.py";
            string param = " --delay " + avg;

            SshStream stream = new SshStream(host, user, pwd);
            stream.Write(cdToProject + " && " + executePython + param);
        }

    }
}
