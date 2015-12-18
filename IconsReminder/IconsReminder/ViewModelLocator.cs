namespace IconsReminder
{
    using Services;
    using ViewModel;

    public class ViewModelLocator
    {
        private static DataService dataService = new DataService();
        private static NavigationService navigationService = new NavigationService();
        private static Notification notificationService = new Notification();

        private static MainViewModel mainViewModel = 
            new MainViewModel(dataService, navigationService, notificationService);
        private static AddReminderViewModel addReminderViewModel =
            new AddReminderViewModel(dataService, navigationService, notificationService);
        private static PastReminderViewModel pastReminderViewModel =
            new PastReminderViewModel(dataService, navigationService, notificationService);
        private static AddNewItemsViewModel addNewItemsViewModel =
            new AddNewItemsViewModel(dataService, navigationService);
        private static AddNoteViewModel addNoteViewModel =
            new AddNoteViewModel(dataService, navigationService, notificationService);
        private static AboutViewModel aboutViewModel =
            new AboutViewModel(navigationService);

        public static MainViewModel MainViewModel
        {
            get
            {
                return mainViewModel;
            }
        }

        public static AddReminderViewModel AddReminderViewModel
        {
            get
            {
                return addReminderViewModel;
            }
        }

        public static PastReminderViewModel PastReminderViewModel
        {
            get
            {
                return pastReminderViewModel;
            }
        }

        public static AddNewItemsViewModel AddNewItemsViewModel
        {
            get
            {
                return addNewItemsViewModel;
            }
        }

        public static AddNoteViewModel AddNoteViewModel
        {
            get
            {
                return addNoteViewModel;
            }
        }

        public static AboutViewModel AboutViewModel
        {
            get
            {
                return aboutViewModel;
            }
        }
    }
}
