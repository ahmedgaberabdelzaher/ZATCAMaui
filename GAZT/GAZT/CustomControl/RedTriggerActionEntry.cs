using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace GAZT.CustomControl
{
    class RedTriggerActionEntry : TriggerAction<Frame>
    {
        protected override void Invoke(Frame sender)
        {
           
            sender.BackgroundColor = Color.Red;
        }
    }
}
