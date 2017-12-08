using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinancePortfolioAPI.Models
{
    public class SecurityQuestion
    {
        public int QuestionId { get; set; }
        public string Question { get; set; }
        public bool Active { get; set; }
    }
}