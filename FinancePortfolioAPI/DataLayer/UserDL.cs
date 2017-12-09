using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FinancePortfolioAPI.Models;
using System.Data.SqlClient;
using System.Data;
using System.Security.Cryptography;
using System.Text;
namespace FinancePortfolioAPI.DataLayer
{
    public class UserDL
    {
        /// <summary>
        /// Validate User while doing login to system
        /// </summary>
        /// <param name="userEntity"></param>
        /// <returns></returns>
        public static User ValidateUser(User userEntity)
        {
            try
            {   
                User user = new User();
                SqlParameter paramUserName, paramPassword;
                paramUserName = new SqlParameter("@UserName", userEntity.UserName);
                paramPassword = new SqlParameter("@Password", Crypto.Encrypt(userEntity.Password));
                using (SqlDataReader readerUser = SqlHelper.ExecuteReader(Common.FPSConn, CommandType.StoredProcedure, "Sp_GetUserDetails", paramUserName, paramPassword))
                {
                    if (readerUser.HasRows)
                    {
                        while (readerUser.Read())
                        {
                            user.UserId = readerUser.GetInt32(readerUser.GetOrdinal("UserId"));
                            user.UserName = readerUser.GetString(readerUser.GetOrdinal("UserName"));
                            user.EmailAddress = readerUser.GetString(readerUser.GetOrdinal("EmailAddress"));
                        };
                    }

                    readerUser.Close();
                }
                return user;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //private static byte[] EncryptString(string str)
        //{
        //    MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider();
        //    UTF8Encoding encoder = new UTF8Encoding();
        //    byte[] byteString = md5Hasher.ComputeHash(encoder.GetBytes(str));
        //    return byteString;
        //}
       
        /// <summary>
        /// Register User
        /// </summary>
        /// <param name="userEntity"></param>
        /// <returns></returns>

        public static int RegisterUser(User userEntity, SqlTransaction tran)
        {
            try
            {
                SqlParameter paramUserName = new SqlParameter("@UserName", SqlDbType.VarChar);
                paramUserName.Direction = ParameterDirection.Input;
                paramUserName.Value = userEntity.UserName;

                SqlParameter paramPassword = new SqlParameter("@Password", SqlDbType.VarChar);
                paramPassword.Direction = ParameterDirection.Input;
                paramPassword.Value = Crypto.Encrypt(userEntity.Password);

                SqlParameter paramFirstName = new SqlParameter("@FirstName", SqlDbType.VarChar);
                paramFirstName.Direction = ParameterDirection.Input;
                paramFirstName.Value = userEntity.FirstName;

                SqlParameter paramLastName = new SqlParameter("@LastName", SqlDbType.VarChar);
                paramLastName.Direction = ParameterDirection.Input;
                paramLastName.Value = userEntity.LastName;

                SqlParameter paramDBO = new SqlParameter("@DOB", SqlDbType.DateTime);
                paramDBO.Direction = ParameterDirection.Input;
                paramDBO.Value = userEntity.DOB;

                SqlParameter paramEmailAddress = new SqlParameter("@EmailAddress", SqlDbType.VarChar);
                paramEmailAddress.Direction = ParameterDirection.Input;
                paramEmailAddress.Value = userEntity.EmailAddress;

                SqlParameter paramAddress = new SqlParameter("@Address", SqlDbType.VarChar);
                paramAddress.Direction = ParameterDirection.Input;
                paramAddress.Value = userEntity.Address;

                SqlParameter paramQuestionId1 = new SqlParameter("@QuestionId1", SqlDbType.Int);
                paramQuestionId1.Direction = ParameterDirection.Input;
                paramQuestionId1.Value = userEntity.QuestionId1;

                SqlParameter paramAnswer1 = new SqlParameter("@Answer1", SqlDbType.VarChar);
                paramAnswer1.Direction = ParameterDirection.Input;
                paramAnswer1.Value = userEntity.Answer1;

                SqlParameter paramQuestionId2 = new SqlParameter("@QuestionId2", SqlDbType.Int);
                paramQuestionId2.Direction = ParameterDirection.Input;
                paramQuestionId2.Value = userEntity.QuestionId2;

                SqlParameter paramAnswer2 = new SqlParameter("@Answer2", SqlDbType.VarChar);
                paramAnswer2.Direction = ParameterDirection.Input;
                paramAnswer2.Value = userEntity.Answer2;

                SqlParameter paramUserId = new SqlParameter("@NewUserId", SqlDbType.Int);
                paramUserId.Direction = ParameterDirection.Output;

                SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "Sp_RegisterUser", paramUserName, paramPassword, paramFirstName, paramLastName, paramDBO, paramEmailAddress, paramAddress, paramQuestionId1, paramAnswer1, paramQuestionId2, paramAnswer2, paramUserId);
                return Convert.ToInt32(paramUserId.Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Register User
        /// </summary>
        /// <param name="userEntity"></param>
        /// <returns></returns>

        public static void UpdateUserTickerSymbols(UserTickerSymbols userTickerSymbols, SqlTransaction tran)
        {
            try
            {
                SqlParameter paramUserId = new SqlParameter("@UserId", SqlDbType.Int);
                paramUserId.Direction = ParameterDirection.Input;
                paramUserId.Value = userTickerSymbols.UserId;

                SqlParameter paramTickerSymbolId = new SqlParameter("@TickerSymbolId", SqlDbType.Int);
                paramTickerSymbolId.Direction = ParameterDirection.Input;
                paramTickerSymbolId.Value = userTickerSymbols.TickerSymbolId;

                SqlParameter paramTickerSymbolName = new SqlParameter("@TickerSymbolName", SqlDbType.VarChar);
                paramTickerSymbolName.Direction = ParameterDirection.Input;
                paramTickerSymbolName.Value = userTickerSymbols.TickerSymbolName;

                SqlParameter paramActive = new SqlParameter("@Active", SqlDbType.Bit);
                paramActive.Direction = ParameterDirection.Input;
                paramActive.Value = userTickerSymbols.Active;

                SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "Sp_UpdateUserTickerSymbols", paramUserId, paramTickerSymbolId, paramTickerSymbolName, paramActive);
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public static List<TickerSymbol> GetUserTickersDetails(int userId)
        {
            try
            {

                List<TickerSymbol> tempList = new List<TickerSymbol>();

                SqlParameter paramUserId;
                paramUserId = new SqlParameter("@UserId", userId);

                using (SqlDataReader readerUserTicker = SqlHelper.ExecuteReader(Common.FPSConn, CommandType.StoredProcedure, "Sp_GetUsersTickers", paramUserId))
                {
                    if (readerUserTicker.HasRows)
                    {
                        tempList = new List<TickerSymbol>();
                        while (readerUserTicker.Read())
                        {
                            tempList.Add(FillDataRecord(readerUserTicker));
                        }
                    }

                    readerUserTicker.Close();
                }
                return tempList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static TickerSymbol FillDataRecord(IDataRecord dataRecordLogin)
        {
            try
            {
                var tickerSymbols = new TickerSymbol()
                {
                    TickerSymbolId  = dataRecordLogin.GetInt32(dataRecordLogin.GetOrdinal("TickerSymbolId")),
                    TickerSymbolName = dataRecordLogin.GetString(dataRecordLogin.GetOrdinal("TickerSymbolName")),
                    TickerSymbolDesc = dataRecordLogin.GetString(dataRecordLogin.GetOrdinal("TickerSymbolDesc")),
                    CurrentPrice = dataRecordLogin.GetDecimal(dataRecordLogin.GetOrdinal("CurrentPrice")),
                    IncrementDecrementValue = dataRecordLogin.GetString(dataRecordLogin.GetOrdinal("IncrementDecrementValue")),
                    IncrementDecrementPercentage = dataRecordLogin.GetString(dataRecordLogin.GetOrdinal("IncrementDecrementPercentage")),
                    ClosingTime = dataRecordLogin.GetString(dataRecordLogin.GetOrdinal("ClosingTime")),
                    PreviousClosingPrice = dataRecordLogin.GetDecimal(dataRecordLogin.GetOrdinal("PreviousClosingPrice")),
                    OpenPrice = dataRecordLogin.GetDecimal(dataRecordLogin.GetOrdinal("OpenPrice")),
                    _52WeekRange = dataRecordLogin.GetString(dataRecordLogin.GetOrdinal("52WeekRange")),
                    EarningDate = dataRecordLogin.GetString(dataRecordLogin.GetOrdinal("EarningDate")),

                    Bid = dataRecordLogin.GetString(dataRecordLogin.GetOrdinal("Bid")),
                    Ask = dataRecordLogin.GetString(dataRecordLogin.GetOrdinal("Ask")),
                    DaysRange = dataRecordLogin.GetString(dataRecordLogin.GetOrdinal("DaysRange")),
                    Volume = dataRecordLogin.GetString(dataRecordLogin.GetOrdinal("Volume")),
                    AvgVolume = dataRecordLogin.GetString(dataRecordLogin.GetOrdinal("AvgVolume")),
                    MarketCap = dataRecordLogin.GetString(dataRecordLogin.GetOrdinal("MarketCap")),
                    FirstYearTargetEst = dataRecordLogin.GetString(dataRecordLogin.GetOrdinal("FirstYearTargetEst")),

                };

                return tickerSymbols;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<SecurityQuestion> GetSecurityQuestions()
        {
            try
            {
                List<SecurityQuestion> tempList = new List<SecurityQuestion>();

                using (SqlDataReader readerSecurityQue = SqlHelper.ExecuteReader(Common.FPSConn, CommandType.StoredProcedure, "Sp_GetSecurityQuestions"))
                {
                    if (readerSecurityQue.HasRows)
                    {
                        tempList = new List<SecurityQuestion>();
                        while (readerSecurityQue.Read())
                        {
                            tempList.Add(FillDataRecordSecurityQue(readerSecurityQue));
                        }
                    }
                    readerSecurityQue.Close();
                }


                return tempList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static SecurityQuestion FillDataRecordSecurityQue(IDataRecord dataRecordLogin)
        {
            try
            {
                var securityQuestion = new SecurityQuestion()
                {
                    QuestionId = dataRecordLogin.GetInt32(dataRecordLogin.GetOrdinal("QuestionId")),
                    Question = dataRecordLogin.GetString(dataRecordLogin.GetOrdinal("Question")),
                    Active = dataRecordLogin.GetBoolean(dataRecordLogin.GetOrdinal("Active")),
                };

                return securityQuestion;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static User GetUserDetailsByUserName(string userName)
        {
            try
            {
                User user = new User();
                SqlParameter paramUserName;
                paramUserName = new SqlParameter("@UserName", userName);

                using (SqlDataReader readerUser = SqlHelper.ExecuteReader(Common.FPSConn, CommandType.StoredProcedure, "Sp_GetUserDetailsByUserName", paramUserName))
                {
                    if (readerUser.HasRows)
                    {
                        while (readerUser.Read())
                        {
                            user.UserId = readerUser.GetInt32(readerUser.GetOrdinal("UserId"));
                            user.UserName = readerUser.GetString(readerUser.GetOrdinal("UserName"));
                            user.Password = Crypto.Decrypt(readerUser.GetString(readerUser.GetOrdinal("Password")));
                            user.FirstName = readerUser.GetString(readerUser.GetOrdinal("FirstName"));
                            user.LastName = readerUser.GetString(readerUser.GetOrdinal("LastName"));
                            user.DOB = readerUser.GetDateTime(readerUser.GetOrdinal("DOB"));
                            user.EmailAddress = readerUser.GetString(readerUser.GetOrdinal("EmailAddress"));
                            user.Address= readerUser.GetString(readerUser.GetOrdinal("Address"));
                            user.QuestionId1 = readerUser.GetInt32(readerUser.GetOrdinal("QuestionId1"));
                            user.Question1 = readerUser.GetString(readerUser.GetOrdinal("Question1"));
                            user.Answer1 = readerUser.GetString(readerUser.GetOrdinal("Answer1")); 
                            user.QuestionId2 = readerUser.GetInt32(readerUser.GetOrdinal("QuestionId2"));
                            user.Question2 = readerUser.GetString(readerUser.GetOrdinal("Question2"));
                            user.Answer2 = readerUser.GetString(readerUser.GetOrdinal("Answer2"));
                        };
                    }

                    readerUser.Close();
                }
                return user;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static int UpdateUserDetails(User userEntity)
        {
            try
            {
                SqlParameter paramUserName = new SqlParameter("@UserName", SqlDbType.VarChar);
                paramUserName.Direction = ParameterDirection.Input;
                paramUserName.Value = userEntity.UserName;

                SqlParameter paramPassword = new SqlParameter("@Password", SqlDbType.VarChar);
                paramPassword.Direction = ParameterDirection.Input;
                paramPassword.Value = Crypto.Encrypt(userEntity.Password);

                SqlParameter paramFirstName = new SqlParameter("@FirstName", SqlDbType.VarChar);
                paramFirstName.Direction = ParameterDirection.Input;
                paramFirstName.Value = userEntity.FirstName;

                SqlParameter paramLastName = new SqlParameter("@LastName", SqlDbType.VarChar);
                paramLastName.Direction = ParameterDirection.Input;
                paramLastName.Value = userEntity.LastName;

                SqlParameter paramDBO = new SqlParameter("@DOB", SqlDbType.DateTime);
                paramDBO.Direction = ParameterDirection.Input;
                paramDBO.Value = userEntity.DOB;

                SqlParameter paramEmailAddress = new SqlParameter("@EmailAddress", SqlDbType.VarChar);
                paramEmailAddress.Direction = ParameterDirection.Input;
                paramEmailAddress.Value = userEntity.EmailAddress;

                SqlParameter paramAddress = new SqlParameter("@Address", SqlDbType.VarChar);
                paramAddress.Direction = ParameterDirection.Input;
                paramAddress.Value = userEntity.Address;

                SqlParameter paramUserId = new SqlParameter("@UserId", SqlDbType.Int);
                paramUserId.Direction = ParameterDirection.Input;
                paramUserId.Value = userEntity.UserId;

                SqlHelper.ExecuteNonQuery(Common.FPSConn,CommandType.StoredProcedure, "Sp_UpdateUserDetails", paramUserName, paramPassword, paramFirstName, paramLastName, paramDBO, paramEmailAddress, paramAddress, paramUserId);
                return Convert.ToInt32(paramUserId.Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



    }
}