using FinancePortfolioAPI.DataLayer;
using FinancePortfolioAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace FinancePortfolioAPI.BusinessLayer
{
    public class TickerSymbolBL
    {
        public List<TickerSymbolForParser> GetAllTickerSymbol()
        {
            try
            {
                List<TickerSymbolForParser> tickerSymbols = TickerSymbolDL.GetAllTickerSymbol();
                return tickerSymbols;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void InsertUpdateTickerSymbols(List<TickerSymbol> tickerSymbolList)
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
                        foreach (TickerSymbol tickerSymbols in tickerSymbolList)
                        {
                            
                            if (tickerSymbols.IncrementDecrementValue.Substring(0,1) != "+" && tickerSymbols.IncrementDecrementValue.Substring(0,1) != "-")
                            {
                                tickerSymbols.IncrementDecrementValue = "+" + tickerSymbols.IncrementDecrementValue;
                            }
                            if (tickerSymbols.IncrementDecrementPercentage.Substring(0, 1) != "+" && tickerSymbols.IncrementDecrementPercentage.Substring(0, 1) != "-")
                            {
                                tickerSymbols.IncrementDecrementPercentage = "+" + tickerSymbols.IncrementDecrementPercentage;
                            }
                            TickerSymbolDL.InsertUpdateTickerSymbols(tickerSymbols, objTrans);
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
    }
}