namespace IconsReminder.Model
{
    using System;
    using System.IO;
    using Windows.UI.Xaml.Media;
    using Windows.UI.Xaml.Media.Imaging;

    public class Item : ItemBase, IItem, IComparable<IItem>
    {
        public readonly static Uri ImageBaseUri = new Uri("ms-appx:///IconsReminder.DAL/Images/");

        public Item(string itemName, string image)
        {
            Title = itemName;
            ImageName = image;
            Image = SetImage(image);
        }

        private BitmapImage SetImage(string imageName)
        {
            try
            {
                return new BitmapImage(new Uri(ImageBaseUri, ImageName));
            }
            catch
            {
                return null;
            }
        }

        private string _title;
        public string Title
        {
            get
            {
                return this._title;
            }

            set
            {
                var title = Path.GetFileNameWithoutExtension(value);
                if (this._title == title) return;
                this._title = title;
                this.NotifyPropertyChanged();
            }
        }

        private ImageSource _image;
        public ImageSource Image
        {
            get
            {
                return this._image;
            }

            private set
            {
                if (this._image == value) return;

                this._image = value;
                this.NotifyPropertyChanged();
            }
        }

        public string ImageName { get; private set; }

        private IReminder _reminder;
        public IReminder Reminder
        {
            get
            {
                return _reminder;
            }
            set
            {
                _reminder = value;
            }
        }

        public IItem DeepCopy()
        {
            return new Item(Title, ImageName);
        }

        public int CompareTo(IItem compareItem)
        {
            if (compareItem.Reminder != null)
            {
                return this.Reminder.ReminderDateTime.CompareTo(compareItem.Reminder.ReminderDateTime);
            }
            return this.Title.CompareTo(compareItem.Title);
        }
    }
}
