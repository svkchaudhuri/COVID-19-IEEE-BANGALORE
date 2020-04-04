using System;
using System.Collections.Generic;
using System.Text;

namespace IEEE_COVID19.Notification
{
    public class NotificationEventArgs : EventArgs
    {
        public string Title { get; set; }
        public string Message { get; set; }
    }
}
