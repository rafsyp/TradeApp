using Binance;
using Binance.WebSocket;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;

namespace TradeApp
{
    class MainApp
    {
        static void Main(string[] args)
        {

            // Choose market and timeframe
            String market = "BTCUSDT";
            String interval = "4h";

            Trade trade = new Trade();
            trade.Ticker = market;
            trade.Comment = "moving average cross";
            trade.Tradetype = "long/short";
            trade.DateAdded = DateTime.Now;


            //Get list of historical data
            var data = new ListService();
            data.GetJson(market,interval);
            while (data.list == null)
            {
                Console.WriteLine("loading historical data");
                Thread.Sleep(1000);
            }
            Console.WriteLine("Scanning....");

            //initialize statistics and timeframe (max 500)
            int timeframe = 50;
            var sublist = data.list.GetRange(499 - timeframe, timeframe);
            double[] array = new double[timeframe];
            for (int i = 0; i < timeframe; i++)
            {
                double value = sublist[i].Close;
                array[i] = value;
            }
            var stats = new Statistics(array);

            //calculate moving averages
            double sma15 = stats.simpleMovingAverage(15);
            double sma50 = stats.simpleMovingAverage(50);

            //upload idea to api
            TradeIdeasNetApi api = new TradeIdeasNetApi("test@gmail.com", "test");

            //scan for trade idea
            while (true)
            {
                if (sma15 > sma50)
                {
                    trade.DateAdded = DateTime.Now;
                    api.CallApiAsync(trade);
                }
                Thread.Sleep(10000);
            }
        
        }
    }
}
