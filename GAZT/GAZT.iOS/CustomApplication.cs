using System;
using System.Linq;
using EGAZT;

namespace GAZT.iOS
{
    [Xamarin.Forms.Internals.Preserve(AllMembers = true)]
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
                    Xamarin.Forms.Application.Current.Properties["timeOut"] = DateTime.Now;
                    //App.ResetAndContinueSession();

                    //EGAZT.App.stopWatch.Reset();
                    //EGAZT.App.stopWatch.Start();
                }
            }

            base.SendEvent(uievent);
        }
    }
}
