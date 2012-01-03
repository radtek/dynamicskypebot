using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Net;
using System.Text;
using System.IO;
using log4net;
using log4net.Config;
using log4net.Repository;
using log4net.Appender;
using System.Threading;
using System.Security.Permissions;
using ExceptionReporting;

// Initialize log4net logging.
[assembly: XmlConfiguratorAttribute(Watch=false)]

namespace SkypeBot {
    static class Program {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.ControlAppDomain)]
        static void Main() {
            log.Info("Starting application.");

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            AppDomain.CurrentDomain.UnhandledException += UnhandledException;
            Application.ThreadException += ThreadException;
                    
            Application.Run(new ConfigForm());

            log.Info("All done; closing down.");
        }

        private static void ThreadException(object sender, ThreadExceptionEventArgs e) {
            log.Fatal("Unhandled thread exception.", e.Exception);
            FlushLogBuffers();

            Properties.Settings.Default.Crashed = true;
            Properties.Settings.Default.Save();

            ExceptionReporter reporter = new ExceptionReporter();
            reporter.Show(e.Exception);
        }

        private static void UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
 	        log.Fatal("Unhandled exception to toplevel.", (Exception)e.ExceptionObject);
            FlushLogBuffers();

            Properties.Settings.Default.Crashed = true;
            Properties.Settings.Default.Save();

            ExceptionReporter reporter = new ExceptionReporter();
            reporter.Show((Exception)e.ExceptionObject);
        }

        private static void FlushLogBuffers() {
            ILoggerRepository rep = LogManager.GetRepository();
            foreach (IAppender appender in rep.GetAppenders()) {
                var buffered = appender as BufferingAppenderSkeleton;
                if (buffered != null) {
                    buffered.Flush();
                }
            }
        }
    }
}
