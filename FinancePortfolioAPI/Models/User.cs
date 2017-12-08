using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinancePortfolioAPI.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        //public byte[] EncryptedPassword { get; set; }
        public bool Active { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DOB { get; set; }
        public string EmailAddress { get; set; }
        public string Address { get; set; }
        public int QuestionId1 { get; set; }
        public string Question1 { get; set; }
        public string Answer1 { get; set; }        
        public int QuestionId2 { get; set; }
        public string Question2 { get; set; }
        public string Answer2 { get; set; }
        public List<UserTickerSymbols> UserTickersymbols { get; set; }
    }

    //public class UserTickerDetails
    //{
    //    public int UserId { get; set; }
    //    public List<UserTickerSymbols> UserTickersymbols { get; set; }
    //}
}
