namespace IconsReminder.Services
{
    public interface INavigationService
    {
        void NavigateToAddReminderPage();
        void NavigateToPastReminderPage();
        void NavigateToAddNewItemsPage();
        void NavigateToAddNewItemsPage(int pivotIndex);
        void NavigateToAddNotePage();
        void NavigateToMainPage();
        void NavigateToAboutPage();
        void NavigateBack();
    }
}
