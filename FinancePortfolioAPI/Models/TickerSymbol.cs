using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinancePortfolioAPI.Models
{
    public class TickerSymbol
    {
        //[JsonProperty(PropertyName = "TickerSymbolId")]
        public int TickerSymbolId { get; set; }

        [JsonProperty(PropertyName = "ticker")]
        public string TickerSymbolName { get; set; }

        [JsonProperty(PropertyName = "longName")]
        public string TickerSymbolDesc { get; set; }

        [JsonProperty(PropertyName = "Active")]
        public bool Active { get; set; }

        [JsonProperty(PropertyName = "regularMarketPrice")]
        public decimal CurrentPrice { get; set; }

        [JsonProperty(PropertyName = "RegularMarketChange")]
        public string IncrementDecrementValue { get; set; }

        [JsonProperty(PropertyName = "RegularMarketChangePercent")]
        public string IncrementDecrementPercentage { get; set; }

        [JsonProperty(PropertyName = "postMarketTime")]
        public string ClosingTime { get; set; }

        [JsonProperty(PropertyName = "Previous Close")]
        public decimal PreviousClosingPrice { get; set; }

        [JsonProperty(PropertyName = "Open")]
        public decimal OpenPrice { get; set; }

        [JsonProperty(PropertyName = "52 Week Range")]
        public string _52WeekRange { get; set; }

        [JsonProperty(PropertyName = "Earnings Date")]
        public string EarningDate { get; set; }

        [JsonProperty(PropertyName = "Bid")]
        public string Bid { get; set; }

        [JsonProperty(PropertyName = "Ask")]
        public string Ask { get; set; }

       // [JsonProperty(PropertyName = "Day's Range")]
        public string DaysRange { get; set; }

        [JsonProperty(PropertyName = "Volume")]
        public string Volume { get; set; }

        [JsonProperty(PropertyName = "Avg. Volume")]
        public string AvgVolume { get; set; }

        [JsonProperty(PropertyName = "Market Cap")]
        public string MarketCap { get; set; }

        [JsonProperty(PropertyName = "1y Target Est")]
        public string FirstYearTargetEst{ get; set; }
    }

    public class TickerSymbolForParser
    {
        [JsonProperty(PropertyName = "ticker")]
        public string TickerSymbolName { get; set; }
    }
}