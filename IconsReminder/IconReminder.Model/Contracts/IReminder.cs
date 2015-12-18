namespace IconsReminder.Model
{
    using System;
    using System.ComponentModel;

    public interface IReminder : INotifyPropertyChanged
    {
        DateTime ReminderDateTime { get; set; }
        TimeSpan ReminderDateTimeTime { get; set; }
        TimeSpan RemainingTime { get; set; }
        bool EnableTimer { get; set; }
        string ReminderDescription { get; set; }
        string NotificationId { get; set; }
    }
}
