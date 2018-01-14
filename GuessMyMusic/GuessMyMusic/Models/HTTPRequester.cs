using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace GuessMyMusic.Models
{
    public static class HTTPRequester
    {        
        static HttpClient client = new HttpClient();

        //i am calling rhythm api
        //created account at https://developer.gracenote.com/rhythm-api and got my id and tag from there

        //also found out that this api has not really electronic genre in their database
        //switching focus to http request to rapsberry
        static string clientID = "1585897499";
        static string clientTag = "13BBC26B9EEA8FB7B770CA12261A22C4";
        static string userId;
        static string personalizedUrl = "https://c" + clientID + ".web.cddbp.net/webapi/json/1.0/";


        async public static Task<JContainer> RegisterRequest() {
            string uri = personalizedUrl + "register?client=" + clientID + "-" + clientTag;
            //build uri string with genre
            var response = await client.GetAsync(uri).ConfigureAwait(false);
            JContainer data = null;

            if (response != null) {
                //request is giving me timeout
                string json = response.Content.ReadAsStringAsync().Result;
                data = (JContainer)JsonConvert.DeserializeObject(json);
            }
            var jObj = (JObject)data;
            var dict = JsonConvert.DeserializeObject<Dictionary<string, object>>(jObj.ToString());
            return data;
        }
    }
}
