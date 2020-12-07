using System;
using System.Collections.Generic;
using System.Linq;
using AppDynamics.Agent;
using Foundation;
using UIKit;
namespace GAZT.iOS
{
    public class Application
    {
        // This is the main entry point of the application.
        static void Main(string[] args)
        {
            // if you want to use a different Application Delegate class from "AppDelegate"
            // you can specify it here.
            try
            {
                //rohith-login
                UIApplication.Main(args, typeof(CustomApplication), typeof(AppDelegate));
                //UIApplication.Main(args, null, "AppDelegate");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Instrumentation.ReportError(ex, ErrorSeverityLevel.CRITICAL);
            }
        }
    }
}
