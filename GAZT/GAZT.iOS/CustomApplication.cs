using System;
using System.Linq;

namespace GAZT.iOS
{
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
                    //EGAZT.App.stopWatch.Reset();
                    //EGAZT.App.stopWatch.Start();
                }
            }

            base.SendEvent(uievent);
        }
    }
}
