using System;
using System.Net.Http;

namespace GuessMyMusic.Models
{
    public static class RestRaspi
    {
        static HttpClient client = new HttpClient();
        static StringContent content;


        public static void doTheThing(string cycles, string delay, string filename) {
            string uri = "localhost:8080/startDisco";

            string parameters = "";
            if (cycles != null && delay != null && filename != null)
                parameters += "?";

            if (cycles != null)
                parameters += "cycles=" + cycles + "&";
            if (delay != null)
                parameters += "delay=" + delay + "&";
            if (filename != null)
                parameters += "filename=" + filename;

            uri += parameters;

            client.PostAsync(uri, content);
        }
    }
}
