using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace GAZT.CustomControl
{
    [Preserve(AllMembers = true)]
    class RedTriggerActionEntry : TriggerAction<Frame>
    {
        protected override void Invoke(Frame sender)
        {
            sender.BackgroundColor = Color.Red;
        }
    }
}
