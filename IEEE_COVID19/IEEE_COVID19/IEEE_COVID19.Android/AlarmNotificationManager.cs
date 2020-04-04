using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Graphics;
using Android.Media;
using Android.Support.V4.App;
using IEEE_COVID19.Notification;
using Java.Lang;
using Java.Util;
using Xamarin.Forms;
using Application = Android.App.Application;
using Calendar = Android.Icu.Util.Calendar;
using CalendarField = Android.Icu.Util.CalendarField;
using String = System.String;
using TaskStackBuilder = Android.App.TaskStackBuilder;

[assembly: Dependency(typeof(IEEE_COVID19.Droid.AlarmNotificationManager))]

namespace IEEE_COVID19.Droid
{
    public class AlarmNotificationManager : IAlarmNotificationManager
    {
        public const int DAILY_REMINDER_REQUEST_CODE = 100;
        public const String TAG = "AlarmNotificationScheduler";
        public const string TitleKey = "title";
        public const string MessageKey = "message";
        private string _title;
        private string _content;

        private static List<AlarmReceiver> _alarmReceivers = new List<AlarmReceiver>();

        public event EventHandler AlarmNotificationReceived;

        public void SetReminder(int hour, int min, string title, string content)
        {
            _title = title;
            _content = content;
            var calendar = Calendar.GetInstance(Locale.Default);
            var setCalendar = Calendar.GetInstance(Locale.Default);
            setCalendar.Set(CalendarField.HourOfDay, hour);
            setCalendar.Set(CalendarField.Minute, min);
            setCalendar.Set(CalendarField.Second, 0);
            CancelReminder();
            if (setCalendar.Before(calendar))
                setCalendar.Add(CalendarField.Date, 1);

            var alarmReceiver = new AlarmReceiver(title, content);

            var componentName = new ComponentName(Application.Context, alarmReceiver.Class);
            var packageManager = Application.Context.PackageManager;
            packageManager.SetComponentEnabledSetting(componentName, ComponentEnabledState.Enabled, ComponentEnableOption.DontKillApp);

            var intent = new Intent(Application.Context, alarmReceiver.Class);
            intent.PutExtra(TitleKey, title);
            intent.PutExtra(MessageKey, content);
            var pendingIntent = PendingIntent.GetBroadcast(Application.Context, DAILY_REMINDER_REQUEST_CODE, intent,
                PendingIntentFlags.UpdateCurrent);
            var alarmManager = (AlarmManager)Application.Context.GetSystemService(Context.AlarmService);
            alarmManager.SetInexactRepeating(AlarmType.RtcWakeup, setCalendar.TimeInMillis, AlarmManager.IntervalDay, pendingIntent);

        }

        public void ShowNotification(string title, string content)
        {
            var alarmSound = RingtoneManager.GetDefaultUri(RingtoneType.Notification);
            var intent = new Intent(Application.Context, Class.FromType(typeof(MainActivity)));
            intent.PutExtra(TitleKey, _title);
            intent.PutExtra(MessageKey, _content);
            intent.SetFlags(ActivityFlags.ClearTop);
            var taskBuilder = TaskStackBuilder.Create(Application.Context);
            taskBuilder.AddParentStack(Class.FromType(typeof(MainActivity)));
            taskBuilder.AddNextIntent(intent);

            var pendingIntent =
                taskBuilder.GetPendingIntent(DAILY_REMINDER_REQUEST_CODE, PendingIntentFlags.UpdateCurrent);
            var noticeBuilder = new NotificationCompat.Builder(Application.Context, "default");
            var notification = noticeBuilder.SetContentTitle(_title)
                .SetContentText(_content).SetSound(alarmSound)
                .SetLargeIcon(
                    BitmapFactory.DecodeResource(Application.Context.Resources, Resource.Drawable.COVCOV))
                .SetSmallIcon(Resource.Drawable.abc_ic_star_black_36dp)
                .SetContentIntent(pendingIntent).Build();
            var notificationManager =
                (NotificationManager)Application.Context.GetSystemService(Context.NotificationService);
            notificationManager.Notify(DAILY_REMINDER_REQUEST_CODE, notification);            
        }

        public void ReceiveNotification(string title, string message)
        {
            var args = new NotificationEventArgs()
            {
                Title = title,
                Message = message,
            };
            AlarmNotificationReceived?.Invoke(null, args);
        }


        public void CancelReminder()
        {
            _alarmReceivers.ForEach(x =>
            {
                var receiver = new ComponentName(Application.Context, x.Class);
                var packageManager = Application.Context.PackageManager;
                packageManager.SetComponentEnabledSetting(receiver, ComponentEnabledState.Disabled,
                    ComponentEnableOption.DontKillApp);
                var intent = new Intent(Application.Context, x.Class);
                var pendingIntent = PendingIntent.GetBroadcast(Application.Context, DAILY_REMINDER_REQUEST_CODE,
                    intent, PendingIntentFlags.UpdateCurrent);
                var alarmManager = (AlarmManager)Application.Context.GetSystemService(Context.AlarmService);
                alarmManager.Cancel(pendingIntent);
                pendingIntent.Cancel();
            });
        }
    }
}