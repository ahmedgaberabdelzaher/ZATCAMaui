using Newtonsoft.Json;
using UIKit;

namespace ZATCAMAUI.Platforms.iOS;

public class Program
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
        catch (Exception)
        {
        }
    }
}
public class CustomApplication : UIKit.UIApplication
{
    public CustomApplication() : base()
    {

    }

    public CustomApplication(IntPtr handle) : base(handle)
    {

    }

    public CustomApplication(Foundation.NSObjectFlag t) : base(t)
    {

    }

    //rohith-login
    public override void SendEvent(UIKit.UIEvent uievent)
    {
        if (uievent.Type == UIKit.UIEventType.Touches)
        {
            if (uievent.AllTouches.Cast<UIKit.UITouch>().Any(t => t.Phase == UIKit.UITouchPhase.Began))
            {
                Preferences.Default.Set("timeOut", DateTime.Now);
                //App.ResetAndContinueSession();

                //EGAZT.App.stopWatch.Reset();
                //EGAZT.App.stopWatch.Start();
            }
        }

        base.SendEvent(uievent);
    }
}
