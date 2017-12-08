using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinancePortfolioAPI.Models
{
    public class UserTickerSymbols
    {
        public int UserTickerSymbolId { get; set; }
        public int UserId { get; set; }
        public int TickerSymbolId { get; set; }
        public string TickerSymbolName { get; set; }
        public bool Active { get; set; }
    }
}