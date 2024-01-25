using MyApp.Service;
using MyApp.ViewModel;

namespace MyApp.View.Account
{
    public partial class CustomerSupportViewPage : ContentPage
    {
        private readonly CustomerSupportViewModel _viewModel;

        public CustomerSupportViewPage()
        {
            InitializeComponent();
            _viewModel = new CustomerSupportViewModel(new CustomerSupoortService(new HttpClient())); // Provide necessary dependencies here
            BindingContext = _viewModel;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await LoadData();
        }

        private async Task LoadData()
        {
            await _viewModel.LoadPaymentStatus();
        }

        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync(nameof(NotificationPage));
        }
        private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
        {
            string phoneNumber = _viewModel.SelectedCustomer?.Call;

            if (!string.IsNullOrEmpty(phoneNumber))
            {
                // Launch the phone dialer with the phone number
                Launcher.OpenAsync($"tel:{phoneNumber}");
            }
        }

        private async void OnWhatsAppIconTapped(object sender, EventArgs e)
        {
            string phoneNumber = _viewModel.SelectedCustomer?.WhatsApp;

            if (!string.IsNullOrEmpty(phoneNumber))
            {
                // Launch WhatsApp with the phone number
                await Launcher.OpenAsync($"https://wa.me/{phoneNumber}");
            }
        }

        private async void OnEmailIconTapped(object sender, EventArgs e)
        {
            string email = _viewModel.SelectedCustomer?.Email;

            if (!string.IsNullOrEmpty(email))
            {
                // Launch the default email app with the specified email address
                await Launcher.OpenAsync($"mailto:{email}");
            }
        }
    }
}
