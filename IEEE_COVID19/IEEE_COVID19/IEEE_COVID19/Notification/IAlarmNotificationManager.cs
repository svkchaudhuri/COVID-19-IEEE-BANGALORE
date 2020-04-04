using System;
using System.Collections.Generic;
using System.Text;

namespace IEEE_COVID19.Notification
{
    public interface IAlarmNotificationManager
    {
        event EventHandler AlarmNotificationReceived;
        void SetReminder(int hour, int min, string title, string content);
        void CancelReminder();
        void ShowNotification(string title, string content);
        void ReceiveNotification(string title, string message);
    }
}
