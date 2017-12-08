using FinancePortfolioAPI.BusinessLayer;
using FinancePortfolioAPI.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FinancePortfolioAPI.Controllers
{
    public class TickerSymbolController : ApiController
    {
        [HttpGet]
        [ActionName("GetAllTickerSymbols")]

        public IEnumerable<TickerSymbolForParser> GetAllTickerSymbol()
        {

                
            try
            {
                TickerSymbolBL tickerSymbolBL = new TickerSymbolBL();
                List<TickerSymbolForParser> tickerSymbols = tickerSymbolBL.GetAllTickerSymbol();
                return tickerSymbols;
            }
            catch (Exception ex)
            {
                Common.WriteLog(ex);
                List<TickerSymbolForParser> tickerSymbols = new List<TickerSymbolForParser>();
                tickerSymbols.Add(new TickerSymbolForParser { TickerSymbolName = ex.Message });
                tickerSymbols.Add(new TickerSymbolForParser { TickerSymbolName = ex.InnerException.ToString() });
                return tickerSymbols;
            }
            
           
          
            //return tickerSymbols.ToList().ToList().Select(p => new TickerSymbolForParser {
            //    TickerSymbolName = p.TickerSymbolName
            //}).AsQueryable();
        }
        [HttpPost()]
        [ActionName("InsertUpdateTickerSymbols")]
        public void InsertUpdateTickerSymbols([FromBody] List<TickerSymbol> tickerSymbolList)
        {
            try
            {

                TickerSymbolBL tickerSymbolBL = new TickerSymbolBL();
                tickerSymbolBL.InsertUpdateTickerSymbols(tickerSymbolList);

            }
            catch (Exception ex)
            {
                Common.WriteLog(ex);
                throw ex;
            }
        }

        //[HttpGet()]
        //public void RunPhythonCode()
        //{
        //    string fileName = @"C:\Development\ITU\Capstone\Phython\Scrape_2.py";

        //    Process p = new Process();
        //    p.StartInfo = new ProcessStartInfo(@"C:\Python27\python.exe", fileName)
        //    {
        //        RedirectStandardOutput = true,
        //        UseShellExecute = false,
        //        CreateNoWindow = true
        //    };
        //    p.Start();

        //    string output = p.StandardOutput.ReadToEnd();
        //    p.WaitForExit();

        //    Console.WriteLine(output);

        //    Console.ReadLine();
        //}


        //public IHttpActionResult GetTickerSymbolDetails(int id)
        //{
        //    TickerSymbol tickerSymbol = new TickerSymbol();
        //    var product = tickerSymbol.FirstOrDefault((p) => p.Id == id);
        //    if (product == null)
        //    {
        //        return NotFound();
        //    }
        //    return Ok(product);   
        //}
    }
}
