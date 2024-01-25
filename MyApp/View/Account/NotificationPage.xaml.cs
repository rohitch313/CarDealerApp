


using MyApp.ViewModel;

namespace MyApp.View.Account
{
    public partial class NotificationPage : ContentPage
    {
        public NotificationPage()
        {
            InitializeComponent();
            Shell.SetTabBarIsVisible(this, false);

            BindingContext = new NotificationViewModel();
        }
    }

}
