using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Net;
using System.Text;
using System.IO;

namespace SkypeBot {
    static class Program {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
            Application.Run(new ConfigForm());
        }

        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e) {
            WebRequest webReq = WebRequest.Create("http://mathemaniac.org/apps/skypebot/errorlogger.php");
            webReq.Method = "POST";
            webReq.ContentType = "application/x-www-form-urlencoded";

            String msg;
            try {
                Exception ex = (Exception)e.ExceptionObject;
                msg = ex.ToString();
            }
            catch (Exception ex) {
                msg = ex.ToString();
            }

            byte[] byteArray = Encoding.UTF8.GetBytes("error=" + msg);
            webReq.ContentLength = byteArray.Length;
            Stream dataStream = webReq.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();

            webReq.GetResponse();
        }
    }
}
