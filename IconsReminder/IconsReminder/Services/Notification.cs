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

        public void RemoveTileAndToastNotification(string notificationId)
        {
            RemoveTileNotification(notificationId);
            RemoveToastNotification(notificationId);
        }

        private void CreateTileNotification(IItem item, TimeSpan timeSpan)
        {
            if (timeSpan <= TimeSpan.Zero) return;

            var _tileSchedules = tileNotifier.GetScheduledTileNotifications();
            if (_tileSchedules.Where(x => x.Id == item.Reminder.NotificationId) != null)
            {
                RemoveTileNotification(item.Reminder.NotificationId);
            }

            XmlDocument doc = new XmlDocument();
            doc.LoadXml(TileNotificationContent);

            foreach (XmlElement textEl in doc.SelectNodes("//text").OfType<XmlElement>())
                if (textEl.InnerText == "Title")
                    textEl.InnerText = String.Format("You have a reminder for '{0}'!", item.Title);

            ScheduledTileNotification scheduleTile = new ScheduledTileNotification(doc, DateTime.Now.Add(timeSpan))
            {
                Id = item.Reminder.NotificationId,
                ExpirationTime = DateTime.Now.Add(timeSpan).AddHours(5)
            };
            tileNotifier.AddToSchedule(scheduleTile);
        }

        private void CreateToastNotification(IItem item, TimeSpan timeSpan)
        {
            if (timeSpan <= TimeSpan.Zero) return;

            var _toastSchedule = toastNotifier.GetScheduledToastNotifications();
            if (_toastSchedule.Where(x => x.Id == item.Reminder.NotificationId) != null)
            {
                RemoveToastNotification(item.Reminder.NotificationId);
            }

            ToastTemplateType toastType = ToastTemplateType.ToastText01;
            XmlDocument toastXml = ToastNotificationManager.GetTemplateContent(toastType);
            XmlNodeList toastTextElement = toastXml.GetElementsByTagName("text");
            toastTextElement[0].AppendChild(toastXml.CreateTextNode(String.Format("You have a reminder for '{0}'!", item.Title)));

            ScheduledToastNotification scheduleToast =
                new ScheduledToastNotification(toastXml, DateTime.Now.Add(timeSpan))
                {
                    Id = item.Reminder.NotificationId
                };
            toastNotifier.AddToSchedule(scheduleToast);
        }

        private void RemoveTileNotification(string notificationId)
        {
            var _tileSchedules = tileNotifier.GetScheduledTileNotifications().FirstOrDefault(x => x.Id == notificationId);
            if (_tileSchedules != null)
            {
                tileNotifier.RemoveFromSchedule(_tileSchedules);
            }
            tileNotifier.Clear();
        }

        private void RemoveToastNotification(string notificationId)
        {
            var _toastSchedule = toastNotifier.GetScheduledToastNotifications().FirstOrDefault(x => x.Id == notificationId);
            if (_toastSchedule != null)
            {
                toastNotifier.RemoveFromSchedule(_toastSchedule);
            }
        }
    }
}
