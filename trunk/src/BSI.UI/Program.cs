using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Diagnostics;
using System.Reflection;

//using Microsoft.Practices.EnterpriseLibrary.Logging;
//using Microsoft.Practices.EnterpriseLibrary.Logging.ExtraInformation;
using MyZilla.BusinessEntities;
using Tremend.Logging;

using System.Threading;


namespace MyZilla.UI
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MainForm ()); 
                Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);
                AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);  
            }
            catch(Exception ex)
            {
                if (ex.InnerException!=null)
                    MyLogger.Write(ex.InnerException, "Main", LoggingCategory.Exception);
                else
                    MyLogger.Write(ex, "Main", LoggingCategory.Exception);

            }
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            MyLogger.Write((Exception)e.ExceptionObject, "CurrentDomain_UnhandledException", LoggingCategory.Exception);
        }

        static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            MyLogger.Write(e.Exception, "Application_ThreadException", LoggingCategory.Exception);
        }
    }
}
