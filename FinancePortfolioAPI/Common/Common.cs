using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;

namespace FinancePortfolioAPI
{
    public class Common
    {
        public static string FPSConn = Convert.ToString(ConfigurationManager.ConnectionStrings["FPSConn"]);
        public static string logFilePath = Convert.ToString(ConfigurationManager.AppSettings["LogFilePath"]);
        public static void WriteLog(Exception ex)
        {
            StreamWriter log;
            FileStream fileStream = null;
            DirectoryInfo logDirInfo = null;
            FileInfo logFileInfo;
            String strLogFilePath = string.Empty;


            strLogFilePath = logFilePath + "Log_" + System.DateTime.Today.ToString("MM-dd-yyyy") + "." + "txt";
            logFileInfo = new FileInfo(strLogFilePath);
            logDirInfo = new DirectoryInfo(logFileInfo.DirectoryName);
            if (!logDirInfo.Exists) logDirInfo.Create();
            if (!logFileInfo.Exists)
            {
                fileStream = logFileInfo.Create();
            }
            else
            {
                fileStream = new FileStream(strLogFilePath, FileMode.Append);
            }
            log = new StreamWriter(fileStream);
            log.WriteLine("--------------------------------------Error Message Start------------------------------------------------------------");
            log.WriteLine(ex.Message);
            log.WriteLine(ex.StackTrace);
            log.WriteLine("--------------------------------------Error Message Ends-----------------------------------------------------------");
            log.Close();
        }   
    }


}