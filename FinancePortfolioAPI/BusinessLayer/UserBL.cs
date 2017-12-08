using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FinancePortfolioAPI.Models;
using FinancePortfolioAPI.DataLayer;
using System.Text;
using System.Data.SqlClient;
namespace FinancePortfolioAPI.BusinessLayer
{
    public class UserBL
    {
        public User ValidateUser(User userEntity)
        {
            try
            {
                User user = new User();
                user = UserDL.ValidateUser(userEntity);
                return user;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int RegisterUser(User userEntity)
        {
            try
            {
                int userId =0;
                SqlTransaction objTrans = null;  
                using (SqlConnection objConn = new SqlConnection(Common.FPSConn))
                {
                    try
                    {
                        objConn.Open();  
                        objTrans = objConn.BeginTransaction();
                        //Register user with details
                        userId = UserDL.RegisterUser(userEntity, objTrans);

                        if (userId > 0)
                        {
                            //Update all the ticker symbols of the user
                            foreach (UserTickerSymbols userTickerSymbols in userEntity.UserTickersymbols)
                            {
                                userTickerSymbols.UserId = userId;
                                UserDL.UpdateUserTickerSymbols(userTickerSymbols, objTrans);
                            }
                            objTrans.Commit();
                        }
                        else
                        {
                            objTrans.Rollback();
                        }
                    }
                    catch(Exception ex)
                    {
                        objTrans.Rollback();
                        throw ex;                        
                    }
                    finally
                    {
                        objConn.Close();
                    }  
                }
                return userId;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //private string DecryptPassword(string password)
        //{
        //    string strDecryptPassword = string.Empty;
        //    try
        //    {
        //        //byte[] bytes = System.Text.Encoding.Default.GetBytes(password);
        //        //StringBuilder sb = new StringBuilder();
        //        //foreach (byte b in bytes)
        //        //{
        //        //    sb.AppendFormat("{0:B}", b);
        //        //}
        //        strDecryptPassword = StringToBinary(password);
        //        return strDecryptPassword;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //private string StringToBinary(string data)
        //{
        //    StringBuilder sb = new StringBuilder();

        //    foreach (char c in data.ToCharArray())
        //    {
        //        sb.Append(Convert.ToString(c, 2).PadLeft(8, '0'));
        //    }
        //    return sb.ToString();
        //}


        public void UpdateUserTickerSymbols(List<UserTickerSymbols> userTickerSymbolsList)
        {
            try
            {
                SqlTransaction objTrans = null;
                using (SqlConnection objConn = new SqlConnection(Common.FPSConn))
                {
                    try
                    {
                        objConn.Open();
                        objTrans = objConn.BeginTransaction();
                        
                        //Update all the ticker symbols of the user
                        foreach (UserTickerSymbols userTickerSymbols in userTickerSymbolsList)
                        {
                            UserDL.UpdateUserTickerSymbols(userTickerSymbols, objTrans);
                        }
                        objTrans.Commit();
                       
                    }
                    catch (Exception ex)
                    {
                        objTrans.Rollback();
                        throw ex;
                    }
                    finally
                    {
                        objConn.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<TickerSymbol> GetUserTickersDetails(int userId)
        {
            try
            {
                List<TickerSymbol> userTickers = new List<TickerSymbol>();
                userTickers = UserDL.GetUserTickersDetails(userId);
                return userTickers;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<SecurityQuestion> GetSecurityQuestions()
        {
            try
            {
                List<SecurityQuestion> securityQuestion = UserDL.GetSecurityQuestions();
                return securityQuestion;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public User GetUserDetailsByUserName(string userName)
        {
            try
            {
                User user = new User();
                user = UserDL.GetUserDetailsByUserName(userName);
                return user;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}