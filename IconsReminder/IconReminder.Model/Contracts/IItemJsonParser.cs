namespace IconsReminder.Model
{
    using System.Collections.ObjectModel;

    public interface IItemJsonParser
    {
        ObservableCollection<IItem> CreateItemCollection(string jsonString);
        string Stringify(ObservableCollection<IItem> items);
        string Stringify(IItem item);
    }
}
