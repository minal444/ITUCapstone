using FinancePortfolioAPI.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using FinancePortfolioAPI;

namespace FinancePortfolioAPI.DataLayer
{
    public class TickerSymbolDL
    {
        
        /// <summary>Returns a list with ContactPerson objects.</summary>
        /// <returns>A ContactPersonCollection with the ContactPerson objects.</returns>
        public static List<TickerSymbolForParser> GetAllTickerSymbol()
        {
            try
            {
                List<TickerSymbolForParser> tempList = new List<TickerSymbolForParser>();

                using (SqlDataReader readerTickerSymbol = SqlHelper.ExecuteReader(Common.FPSConn, CommandType.StoredProcedure, "Sp_GetTickerSymbols"))
                {
                    if (readerTickerSymbol.HasRows)
                    {
                        tempList = new List<TickerSymbolForParser>();
                        while (readerTickerSymbol.Read())
                        {
                            tempList.Add(FillDataRecord(readerTickerSymbol));
                        }
                    }
                    readerTickerSymbol.Close();
                }


                return tempList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private static TickerSymbolForParser FillDataRecord(IDataRecord dataRecordLogin)
        {
            try
            {
                var tickerSymbols = new TickerSymbolForParser()
                {
                    TickerSymbolName = dataRecordLogin.GetString(dataRecordLogin.GetOrdinal("TickerSymbolName")),
                };

                return tickerSymbols;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void InsertUpdateTickerSymbols(TickerSymbol tickerSymbols, SqlTransaction tran)
        {
            try
            {
                SqlParameter paramTickerSymbolName = new SqlParameter("@TickerSymbolName", SqlDbType.VarChar);
                paramTickerSymbolName.Direction = ParameterDirection.Input;
                paramTickerSymbolName.Value = tickerSymbols.TickerSymbolName.ToUpper();

                SqlParameter paramTickerSymbolDesc = new SqlParameter("@TickerSymbolDesc", SqlDbType.VarChar);
                paramTickerSymbolDesc.Direction = ParameterDirection.Input;
                paramTickerSymbolDesc.Value = tickerSymbols.TickerSymbolDesc;

                SqlParameter paramCurrentPrice = new SqlParameter("@CurrentPrice", SqlDbType.Decimal);
                paramCurrentPrice.Direction = ParameterDirection.Input;
                paramCurrentPrice.Value = tickerSymbols.CurrentPrice;

                SqlParameter paramIncDecVal = new SqlParameter("@IncrementDecrementValue", SqlDbType.VarChar);
                paramIncDecVal.Direction = ParameterDirection.Input;
                paramIncDecVal.Value = tickerSymbols.IncrementDecrementValue;

                SqlParameter paramIncDecPer = new SqlParameter("@IncrementDecrementPercentage", SqlDbType.VarChar);
                paramIncDecPer.Direction = ParameterDirection.Input;
                paramIncDecPer.Value = tickerSymbols.IncrementDecrementPercentage;

                SqlParameter paramClosingTime = new SqlParameter("@ClosingTime", SqlDbType.VarChar);
                paramClosingTime.Direction = ParameterDirection.Input;
                paramClosingTime.Value = tickerSymbols.ClosingTime;

                SqlParameter paramPreviousClosingPrice = new SqlParameter("@PreviousClosingPrice", SqlDbType.Decimal);
                paramPreviousClosingPrice.Direction = ParameterDirection.Input;
                paramPreviousClosingPrice.Value = tickerSymbols.PreviousClosingPrice;

                SqlParameter paramOpenPrice= new SqlParameter("@OpenPrice", SqlDbType.Decimal);
                paramOpenPrice.Direction = ParameterDirection.Input;
                paramOpenPrice.Value = tickerSymbols.OpenPrice;

                SqlParameter param52WeekRange = new SqlParameter("@52WeekRange", SqlDbType.VarChar);
                param52WeekRange.Direction = ParameterDirection.Input;
                param52WeekRange.Value = tickerSymbols._52WeekRange;

                SqlParameter paramEarningDate = new SqlParameter("@EarningDate", SqlDbType.VarChar);
                paramEarningDate.Direction = ParameterDirection.Input;
                paramEarningDate.Value = tickerSymbols.EarningDate;

                SqlParameter paramActive = new SqlParameter("@Active", SqlDbType.Bit);
                paramActive.Direction = ParameterDirection.Input;
                paramActive.Value = tickerSymbols.Active;

                SqlParameter paramBid= new SqlParameter("@Bid", SqlDbType.VarChar);
                paramBid.Direction = ParameterDirection.Input;
                paramBid.Value = tickerSymbols.Bid;

                SqlParameter paramAsk= new SqlParameter("@Ask", SqlDbType.VarChar);
                paramAsk.Direction = ParameterDirection.Input;
                paramAsk.Value = tickerSymbols.Ask;

                SqlParameter paramDaysRange = new SqlParameter("@DaysRange", SqlDbType.VarChar);
                paramDaysRange.Direction = ParameterDirection.Input;
                paramDaysRange.Value = tickerSymbols.DaysRange;

                SqlParameter paramVolume = new SqlParameter("@Volume", SqlDbType.VarChar);
                paramVolume.Direction = ParameterDirection.Input;
                paramVolume.Value = tickerSymbols.Volume;

                SqlParameter paramAvgVolume = new SqlParameter("@AvgVolume", SqlDbType.VarChar);
                paramAvgVolume.Direction = ParameterDirection.Input;
                paramAvgVolume.Value = tickerSymbols.AvgVolume;

                SqlParameter paramMarketCap = new SqlParameter("@MarketCap", SqlDbType.VarChar);
                paramMarketCap.Direction = ParameterDirection.Input;
                paramMarketCap.Value = tickerSymbols.MarketCap;

                SqlParameter paramFirstYearTargetEst = new SqlParameter("@FirstYearTargetEst", SqlDbType.VarChar);
                paramFirstYearTargetEst.Direction = ParameterDirection.Input;
                paramFirstYearTargetEst.Value = tickerSymbols.FirstYearTargetEst;

                SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "Sp_InsertUpdateTickerSymbols", paramTickerSymbolName, paramTickerSymbolDesc, paramCurrentPrice, paramIncDecVal, paramIncDecPer, paramClosingTime, paramPreviousClosingPrice, paramOpenPrice, param52WeekRange, paramEarningDate, paramActive, paramBid, paramAsk, paramDaysRange, paramVolume, paramAvgVolume, paramMarketCap, paramFirstYearTargetEst);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}