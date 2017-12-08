using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FinancePortfolioAPI.BusinessLayer;
using FinancePortfolioAPI.Models;

namespace FinancePortfolioAPI.Controllers
{
    public class UserController : ApiController
    {
        [HttpPost()]
        [ActionName("ValidateUser")]
        public User ValidateUser([FromBody] User userEntity)
        {
            
            try
            {
                UserBL userBL = new UserBL();
                User user = userBL.ValidateUser(userEntity);
                return user;
            }
            catch(Exception ex)
            {
                Common.WriteLog(ex);
                throw ex;
            }
        }

        [HttpPost()]
        [ActionName("RegisterUser")]
        public int RegisterUser([FromBody] User userEntity)
        {
            try
            {
                UserBL userBL = new UserBL();
                int userID = userBL.RegisterUser(userEntity);
                return userID;
            }
            catch(Exception ex)
            {
                Common.WriteLog(ex);
                throw ex;
            }
        }

        [HttpPost()]
        [ActionName("UpdateTickerSymbols")]
        public void UpdateTickerSymbols([FromBody] List<UserTickerSymbols> userTickerSymbolsList)
        {
            try
            {

                UserBL userBL = new UserBL();
                userBL.UpdateUserTickerSymbols(userTickerSymbolsList);

            }
            catch(Exception ex)
            {
                Common.WriteLog(ex);
                throw ex;
            }
        }

        [HttpGet()]
        [ActionName("GetUserTickersDetails")]
        public List<TickerSymbol> GetUserTickersDetails(int id)
        {
            try
            {
                List<TickerSymbol> userTickers = new List<TickerSymbol>();

                UserBL userBL = new UserBL();
                userTickers = userBL.GetUserTickersDetails(id);
                return userTickers;
            }
            catch (Exception ex)
            {
                Common.WriteLog(ex);
                throw ex;
            }
        }

        [HttpGet]
        [ActionName("GetSecurityQuestions")]

        public IEnumerable<SecurityQuestion> GetSecurityQuestions()
        {
            try
            {
                UserBL userBL = new UserBL();
                List<SecurityQuestion> securityQuestion = userBL.GetSecurityQuestions();
                return securityQuestion;
            }
            catch (Exception ex)
            {
                Common.WriteLog(ex);
                throw ex;
            }



            //return tickerSymbols.ToList().ToList().Select(p => new TickerSymbolForParser {
            //    TickerSymbolName = p.TickerSymbolName
            //}).AsQueryable();
        }

        [HttpGet()]
        [ActionName("GetUserDetailsByUserName")]
        public User GetUserDetailsByUserName([FromUri] string id)
        {
            try
            {
                UserBL userBL = new UserBL();
                User user = userBL.GetUserDetailsByUserName(id);
                return user;
            }
            catch (Exception ex)
            {
                Common.WriteLog(ex);
                throw ex;
            }
        }
    }
}
