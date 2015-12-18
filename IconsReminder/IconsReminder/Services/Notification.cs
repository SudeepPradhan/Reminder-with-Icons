namespace IconsReminder
{
    using System;
    using Windows.Data.Xml.Dom;
    using Windows.UI.Notifications;
    using System.Linq;
    using Model;
    using Services;

    public class Notification : INotificationService
    {
        private static TileUpdater tileNotifier;
        private static ToastNotifier toastNotifier;

        private string TileNotificationContent
        {
            get
            {
                return $@"
                <tile version='3'>
                    <visual branding='nameAndLogo'>
                        <binding template='TileMedium'>
                            <text hint-wrap='true'>Title</text>
                        </binding>
                        <binding template='TileWide'>
                            <text hint-wrap='true'>Title</text>
                        </binding>
                        <binding template='TileLarge'>
                            <text hint-wrap='true'>Title</text>
                        </binding>
                </visual>
            </tile>";
            }
        }

        public Notification()
        {
            tileNotifier = TileUpdateManager.CreateTileUpdaterForApplication();
            toastNotifier = ToastNotificationManager.CreateToastNotifier();
        }

        public void CreateTileAndToastNotification(IItem item, TimeSpan timeSpan)
        {
            CreateTileNotification(item, timeSpan);
            CreateToastNotification(item, timeSpan);
        }

        public void UpdateTileAndToastNotification(string notificationId, DateTime dateTime)
        {
            UpdateTileNotification(notificationId, dateTime);
            UpdateToastNotification(notificationId, dateTime);
        }

        public void RemoveTileAndToastNotification(string notificationId)
        {
            RemoveTileNotification(notificationId);
            RemoveToastNotification(notificationId);
        }

        private void CreateTileNotification(IItem item, TimeSpan timeSpan)
        {
            if (timeSpan < new TimeSpan(0, 0, 0)) return;

            XmlDocument doc = new XmlDocument();
            doc.LoadXml(TileNotificationContent);

            foreach (XmlElement textEl in doc.SelectNodes("//text").OfType<XmlElement>())
                if (textEl.InnerText == "Title")
                    textEl.InnerText = String.Format("You have a reminder for '{0}'!", item.Title);

            ScheduledTileNotification scheduleTile = new ScheduledTileNotification(doc, DateTime.Now.Add(timeSpan));
            scheduleTile.Id = item.Reminder.NotificationId;
            tileNotifier.AddToSchedule(scheduleTile);
        }

        private void CreateToastNotification(IItem item, TimeSpan timeSpan)
        {
            if (timeSpan < new TimeSpan(0, 0, 0)) return;

            ToastTemplateType toastType = ToastTemplateType.ToastText01;
            XmlDocument toastXml = ToastNotificationManager.GetTemplateContent(toastType);
            XmlNodeList toastTextElement = toastXml.GetElementsByTagName("text");
            toastTextElement[0].AppendChild(toastXml.CreateTextNode(String.Format("You have a reminder for '{0}'!", item.Title)));

            ScheduledToastNotification scheduleToast = new ScheduledToastNotification(
                toastXml,
                DateTime.Now.Add(timeSpan));
            scheduleToast.Id = item.Reminder.NotificationId;
            toastNotifier.AddToSchedule(scheduleToast);
        }

        private void UpdateTileNotification(string notificationId, DateTime dateTime)
        {
            var _tileSchedules = tileNotifier.GetScheduledTileNotifications();
            foreach (var schedule in _tileSchedules)
            {
                if (schedule.Id == notificationId)
                {
                    RemoveTileNotification(notificationId);

                    ScheduledTileNotification scheduleTile = new ScheduledTileNotification(schedule.Content, (DateTimeOffset)dateTime);
                    scheduleTile.Id = notificationId;
                    tileNotifier.AddToSchedule(scheduleTile);
                    break;
                }
            }
        }

        private void UpdateToastNotification(string notificationId, DateTime dateTime)
        {
            var _toastSchedules = toastNotifier.GetScheduledToastNotifications();
            foreach (var schedule in _toastSchedules)
            {
                if (schedule.Id == notificationId)
                {
                    RemoveToastNotification(notificationId);

                    ScheduledToastNotification scheduleToast = new ScheduledToastNotification(schedule.Content, (DateTimeOffset)dateTime);
                    scheduleToast.Id = notificationId;
                    toastNotifier.AddToSchedule(scheduleToast);
                    break;
                }
            }
        }

        private void RemoveTileNotification(string notificationId)
        {
            var _schedules = tileNotifier.GetScheduledTileNotifications();
            foreach (var schedule in _schedules)
            {
                if (schedule.Id == notificationId)
                {
                    tileNotifier.RemoveFromSchedule(schedule);
                    break; 
                }
            }
            tileNotifier.Clear();
        }

        private void RemoveToastNotification(string notificationId)
        {
            var _schedules = toastNotifier.GetScheduledToastNotifications();
            foreach (var schedule in _schedules)
            {
                if (schedule.Id == notificationId)
                {
                    toastNotifier.RemoveFromSchedule(schedule);
                    break;
                }
            }
        }
    }
}
