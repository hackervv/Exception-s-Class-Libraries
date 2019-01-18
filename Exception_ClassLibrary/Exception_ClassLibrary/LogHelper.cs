using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exception_ClassLibrary
{
    public class LogHelper
    {
        /// <summary>
        /// 简单的记录log日志功能
        /// </summary>
        /// <param name="msg">需要记录的日志信息</param>
        public static void writeLog(string msg)
        {
            string LogPath = "";
            string LogFilePrefix = "";
            try
            {
                System.IO.StreamWriter sw = System.IO.File.AppendText(
                    LogPath + LogFilePrefix + " " + DateTime.Now.ToString("yyyyMMdd") + ".log"
                    );
                sw.Write(DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss: ") + msg);
                sw.Close();
            }catch(Exception ex)
            {
                throw;
            }
        }
    }
}
