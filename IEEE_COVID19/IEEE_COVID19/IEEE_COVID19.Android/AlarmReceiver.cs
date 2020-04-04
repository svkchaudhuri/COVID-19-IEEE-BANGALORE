using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using IEEE_COVID19.Notification;
using Xamarin.Forms;

namespace IEEE_COVID19.Droid
{
    [BroadcastReceiver]
    public class AlarmReceiver : BroadcastReceiver
    {
        private readonly string _title;
        private readonly string _content;

        public AlarmReceiver()
        {
            //_title = "Wash";
            //_content = "How you washed your hand?";
        }
        public AlarmReceiver(string title, string content) : base()
        {
            _title = title;
            _content = content;
        }
        public override void OnReceive(Context context, Intent intent)
        {
            var alarmManager = DependencyService.Get<IAlarmNotificationManager>();
            alarmManager.ShowNotification(_title, _content);
        }
    }
}