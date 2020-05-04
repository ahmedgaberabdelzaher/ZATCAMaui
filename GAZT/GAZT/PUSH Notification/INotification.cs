using System;
using System.Collections.Generic;
using System.Text;
namespace GAZT.PUSH_Notification
{
    public interface INotification
    {
        void CreateNotification(string title, string message);
    }
}
