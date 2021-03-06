﻿using MultiMiner.Blockchain.Data;
using MultiMiner.ExchangeApi;
using MultiMiner.ExchangeApi.Data;
using MultiMiner.Utility.Net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace MultiMiner.Blockchain
{
    public class ApiContext : IApiContext
    {
        public IEnumerable<ExchangeInformation> GetExchangeInformation()
        {
            WebClient webClient = new ApiWebClient();
            webClient.Encoding = Encoding.UTF8;

            string response = webClient.DownloadString(new Uri(GetApiUrl()));

            Dictionary<string, TickerEntry> tickerEntries = JsonConvert.DeserializeObject<Dictionary<string, TickerEntry>>(response);

            List<ExchangeInformation> results = new List<ExchangeInformation>();

            foreach (KeyValuePair<string, TickerEntry> keyValuePair in tickerEntries)
            {
                results.Add(new ExchangeInformation()
                {
                    SourceCurrency = "BTC",
                    TargetCurrency = keyValuePair.Key,
                    TargetSymbol = keyValuePair.Value.Symbol,
                    ExchangeRate = keyValuePair.Value.Last
                });
            }

            return results;
        }

        public string GetInfoUrl()
        {
            return "https://blockchain.info";
        }

        public string GetApiUrl()
        {
            return "https://blockchain.info/ticker";
        }

        public string GetApiName()
        {
            return "Blockchain";
        }
    }
}
