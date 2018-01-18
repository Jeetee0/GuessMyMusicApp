using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;

namespace GuessMyMusic.Models
{
    public class HTTPRequester
    {
        HttpClient client;
        string uri;
        string response;

        public string Response { get => response; set => response = value; }

        public HTTPRequester(string ip, string port, string path, List<string> queryParams)
        {
            client = new HttpClient();
            uri = BuildUri(ip, port, path, queryParams);
        }

        public HTTPRequester(string ip, string port, string path)
        {
            client = new HttpClient();
            uri = BuildUri(ip, port, path);
        }

        static string BuildUri(string ip, string port, string path)
        {
            StringBuilder uri = new StringBuilder("");
            uri.Append("http://");
            uri.Append(ip);
            uri.Append(":" + port);
            uri.Append(path);
            return uri.ToString();
        }

        static string BuildUri(string ip, string port, string path, List<string> queryParams)
        {
            StringBuilder uri = new StringBuilder("");
            uri.Append("http://");
            uri.Append(ip);
            uri.Append(":" + port);
            uri.Append(path);

            if (queryParams.Count != 0)
            {
                uri.Append("?");
                string lastParam = queryParams.Last();
                foreach (var queryParam in queryParams)
                {
                    if (queryParam.Equals(lastParam))
                        uri.Append(queryParam);
                    else
                        uri.Append(queryParam + "&");
                }
            }
            return uri.ToString();
        }

        async public Task<string> SendRequest(bool waitForResponse)
        {
            client.Timeout = new TimeSpan(0, 1, 0);
            if (waitForResponse)
            {
                var responseNew = await client.GetStringAsync(uri);
                return responseNew;
            }
            client.GetStringAsync(uri);
            return "successfully send request";
        }

        async public Task<List<string>> SendRequestGetStringList()
        {
            string responseNew = await client.GetStringAsync(uri);

            //wrote my own parser because its only a json with list of strings
            List<string> listOfInterpretes = new List<string>();

            responseNew = removeSpecialCharacters(responseNew, "[]\\\"");
            string[] interpretes = responseNew.Split(',');

            foreach (var item in interpretes)
                listOfInterpretes.Add(item);
            return listOfInterpretes;
        }

        static string removeSpecialCharacters(string str, string removeableChars)
        {
            StringBuilder returnValue = new StringBuilder("");
            foreach (char c in str)
            {
                if (!removeableChars.Contains(c.ToString()))
                {
                    returnValue.Append(c);
                }
            }
            return returnValue.ToString();
        }
    }
}
