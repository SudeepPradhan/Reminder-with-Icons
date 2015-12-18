namespace IconsReminder.Model
{
    using System;
    using System.ComponentModel;
    using Windows.UI.Xaml.Media;

    public interface IItem : INotifyPropertyChanged, IComparable<IItem>
    {
        string Title { get; set; }
        string ImageName { get; }
        ImageSource Image { get; }
        IReminder Reminder { get; set; }

        IItem DeepCopy();
    }
}
