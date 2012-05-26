using System;
using System.Collections.Generic;
using System.Json;
using System.Net;

namespace Currency
{
    public class CurrencyUpdate
    {
        const string Url = "http://openexchangerates.org/latest.json";

        public DateTime Timestamp { get; set; }
        public string Base { get; set; }
        public List<CurrencyInfo> Infos { get; set; }

        public CurrencyUpdate(JsonValue json)
        {
            Timestamp = ((double)json["timestamp"]).UnixTimeStampToDateTime();
            Base = (string)json["base"];
            Infos = new List<CurrencyInfo>();

            var rates = (JsonObject)json["rates"];
            foreach (KeyValuePair<string, JsonValue> rate in rates)
            {
                Infos.Add(new CurrencyInfo() { Code = rate.Key, Rate = (double)rate.Value });
            }

            Infos.Sort((x, y) => x.Name.CompareTo(y.Name));
        }

        public static CurrencyUpdate Latest()
        {
            var webClient = new WebClient();
            var result = webClient.DownloadString(new Uri(Url));

            return new CurrencyUpdate(JsonObject.Parse(result));
        }
    }
}

