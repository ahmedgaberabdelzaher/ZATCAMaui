using System;
using System.IO;
using Newtonsoft.Json;
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
            }
            catch (Exception ex)
            {
                
                
                LogUnhandledException(ex);
            }
        }

        internal static void LogUnhandledException(Exception exception)
        {
            try
            {
                const string errorFileName = "Fatal.log";
                var libraryPath = Environment.GetFolderPath(Environment.SpecialFolder.Resources); // iOS: Environment.SpecialFolder.Resources
                var errorFilePath = Path.Combine(libraryPath, errorFileName);
                File.WriteAllText(errorFilePath, JsonConvert.SerializeObject(exception));
            }
            catch(Exception)
            {
            }
        }
        
    }
}
