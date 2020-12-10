using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms.Internals;

namespace GAZT.PUSH_Notification
{
    [Preserve(AllMembers = true)]
    public interface INotification
    {
        void CreateNotification(string title, string message);
    }
}
