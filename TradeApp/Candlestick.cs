using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TradeApp
{
    //model for trade candlestick data
    class Candlestick
    {
        [JsonProperty(Order = 1)]
        public long OpenTime { get; set; }
        [JsonProperty(Order = 2)]
        public double Open { get; set; } // Or string, if you prefer
        [JsonProperty(Order = 3)]
        public double High { get; set; } // Or string, if you prefer
        [JsonProperty(Order = 4)]
        public double Low { get; set; } // Or string, if you prefer
        [JsonProperty(Order = 5)]
        public double Close { get; set; } // Or string, if you prefer
        [JsonProperty(Order = 6)]
        public double Volume { get; set; } // Or string, if you prefer
        [JsonProperty(Order = 7)]
        public long CloseTime { get; set; }
        [JsonProperty(Order = 8)]
        public double QuoteAssetVolume { get; set; } // Or string, if you prefer
        [JsonProperty(Order = 9)]
        public long NumberOfTrades { get; set; } // Should this be an long or a decimal?
        [JsonProperty(Order = 10)]
        public double TakerBuyBaseAssetVolume { get; set; }
        [JsonProperty(Order = 11)]
        public double TakerBuyQuoteAssetVolume { get; set; }
        // public string Ignore { get; set; }
    }
}
