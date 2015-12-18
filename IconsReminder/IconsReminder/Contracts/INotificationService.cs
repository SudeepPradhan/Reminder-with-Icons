namespace IconsReminder.Services
{
    using Model;
    using System;

    public interface INotificationService
    {
        void CreateTileAndToastNotification(IItem item, TimeSpan timeSpan);
        void UpdateTileAndToastNotification(string notificationId, DateTime dateTime);
        void RemoveTileAndToastNotification(string notificationId);
    }
}
