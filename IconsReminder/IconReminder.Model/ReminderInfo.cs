namespace IconsReminder.Model
{
    using System;
    using System.Linq;
    using Windows.UI.Xaml;

    public class Reminder : ItemBase, IReminder
    {
        public Reminder(): this(true)
        {
        }

        public Reminder(bool enableTimer)
        {
            DispatcherTimerSetup();
            EnableTimer = enableTimer;
        }

        private DateTime _reminderDateTime = DateTime.Now;
        public DateTime ReminderDateTime
        {
            get
            {
                return this._reminderDateTime;
            }
            set
            {
                if (this._reminderDateTime == value) return;
                this._reminderDateTime = value.Date + this.ReminderDateTime.TimeOfDay;
                this.NotifyPropertyChanged();
            }
        }

        public TimeSpan ReminderDateTimeTime
        {
            get
            {
                return _reminderDateTime - _reminderDateTime.Date;
            }
            set
            {
                if (ReminderDateTimeTime == value) return;
                _reminderDateTime = _reminderDateTime.Date.Add(value);
                NotifyPropertyChanged();
                NotifyPropertyChanged("ReminderDateTime");
            }
        }

        private string _notificationId;
        public string NotificationId
        {
            get
            {
                if (!String.IsNullOrEmpty(_notificationId)) return _notificationId;

                _notificationId = Guid.NewGuid().ToString().Split('-').First();
                return _notificationId;
            }
            set
            {
                _notificationId = value;
            }
        }

        private TimeSpan _remainingTime;
        public TimeSpan RemainingTime
        {
            get
            {
                return _remainingTime;
            }
            set
            {
                _remainingTime = value;
                this.NotifyPropertyChanged();
            }
        }

        private string _reminderDescription = String.Empty;
        public string ReminderDescription
        {
            get
            {
                return this._reminderDescription;
            }
            set
            {
                if (this._reminderDescription == value) return;
                this._reminderDescription = value;
                this.NotifyPropertyChanged();
            }
        }

        private bool _enableTimer = true;
        public bool EnableTimer
        {
            get
            {
                return this._enableTimer;
            }
            set
            {
                if (value == true)
                {
                    dispatcherTimer.Start();
                }
                else
                {
                    dispatcherTimer.Stop();
                    RemainingTime = TimeSpan.Zero;
                }
                this._enableTimer = value;
            }
        }

        DispatcherTimer dispatcherTimer;
        public void DispatcherTimerSetup()
        {
            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += DispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();
        }

        void DispatcherTimer_Tick(object sender, object e)
        {
            RemainingTime = ReminderDateTime - DateTime.Now;
        }
    }
}
