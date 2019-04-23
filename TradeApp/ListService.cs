using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Text;

namespace TradeApp
{
    class ListService
    {
        public List<Candlestick> list { get; set; }

        //Class which calls binance api and populates above list with candlestick data 

        public async void GetJson(string market, string tick_interval)
        {
            ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
            var client = new HttpClient();
            var url = "https://api.binance.com/api/v1/klines?symbol=" + market + "&interval=" + tick_interval;
            var response = await client.GetAsync(url);
            var mess = await response.Content.ReadAsStringAsync();
            var settings = new JsonSerializerSettings
            {
                Converters = { new ObjectToArrayConverter<Candlestick>() },
            };

            list = JsonConvert.DeserializeObject<List<Candlestick>>(mess, settings);
        }
    }


}
