namespace IconsReminder.Services
{
    using System;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;

    public class NavigationService : INavigationService
    {
        public void NavigateToAddReminderPage()
        {
            ((Frame)Window.Current.Content).Navigate(typeof(AddReminder));
        }

        public void NavigateToAddNewItemsPage()
        {
            ((Frame)Window.Current.Content).Navigate(typeof(AddNewItems));
        }

        public void NavigateToAddNewItemsPage(int pivotIndex)
        {
            ((Frame)Window.Current.Content).Navigate(typeof(AddNewItems), pivotIndex);
        }

        public void NavigateToAddNotePage()
        {
            ((Frame)Window.Current.Content).Navigate(typeof(AddNote));
        }

        public void NavigateToMainPage()
        {
            ((Frame)Window.Current.Content).Navigate(typeof(MainPage));
        }

        public void NavigateToAboutPage()
        {
            ((Frame)Window.Current.Content).Navigate(typeof(AboutPage));
        }

        public void NavigateBack()
        {
            ((Frame)Window.Current.Content).GoBack();
        }

        public void NavigateToPastReminderPage()
        {
            ((Frame)Window.Current.Content).Navigate(typeof(PastReminders));
        }
    }
}
