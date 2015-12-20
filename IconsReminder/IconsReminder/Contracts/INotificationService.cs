namespace IconsReminder.Services
{
    using Model;
    using System;

    public interface INotificationService
    {
        void CreateTileAndToastNotification(IItem item, TimeSpan timeSpan);
        void RemoveTileAndToastNotification(string notificationId);
    }
}
