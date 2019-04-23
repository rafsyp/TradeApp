using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;

namespace TradeApp
{
    class TradeIdeasNetApi
    {
       private string _url = "http://localhost:60747/";
       private string _userName; 
       private string _password;

        public TradeIdeasNetApi(string userName, string password)
        {
            _userName = userName;
            _password = password;
        }

        //Generates token for auth
        public string GetToken()
        {
            var pairs = new List<KeyValuePair<string, string>>
                    {
                        new KeyValuePair<string, string>( "grant_type", "password" ),
                        new KeyValuePair<string, string>( "username", _userName ),
                        new KeyValuePair<string, string> ( "Password", _password )
                    };
            var content = new FormUrlEncodedContent(pairs);
            ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
            using (var client = new HttpClient())
            {
                var response = client.PostAsync(_url + "Token", content).Result;
                return response.Content.ReadAsStringAsync().Result;
            }
        }

        //Calls Api to save a trade idea using above token for auth
        public string CallApiAsync(Trade trade)
        {
            string urlr = _url+"Api/Trade";
            string token = GetToken();
            ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
            using (var client = new HttpClient())
            {
                if (!string.IsNullOrWhiteSpace(token))
                {
                    var t = JsonConvert.DeserializeObject<Token>(token);

                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + t.access_token);
                }
                var jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(trade);
                var content = new StringContent(jsonString, Encoding.UTF8, "application/json");
                var response = client.PostAsync(urlr, content).Result; 
                return response.Content.ReadAsStringAsync().Result;
            }
        }
    }
}
