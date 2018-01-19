using System;
using System.Threading.Tasks;

namespace GuessMyMusic.Models
{
    public interface ISshConnector
    {
        bool Connected { get; set; }

        Task<string> Connect(string host, string port, string username, string pwd);
        Task<string> RunCommandAsync(string delay);
        void Disconnect();
        string ToString();
    }


}
