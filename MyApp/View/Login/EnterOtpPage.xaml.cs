
using MyApp.Service;
using MyApp.View.Home;
using MyApp.ViewModel;

namespace MyApp.View.Login
{
    public partial class EnterOtpPage : ContentPage
    {
        

        public EnterOtpPage(PostLoginViewModel _viewModel)
        {
            InitializeComponent();
            // Provide necessary dependencies here
            BindingContext = _viewModel;


        }

        private void OnDigitEntryTextChanged(object sender, TextChangedEventArgs e)
        {
            Entry currentEntry = (Entry)sender;

            if (!string.IsNullOrEmpty(e.NewTextValue) && e.NewTextValue.Length == 1)
            {
                // Move focus to the next Entry when a digit is entered
                switch (currentEntry)
                {
                    case Entry _ when currentEntry == Digit1Entry:
                        Digit2Entry.Focus();
                        break;
                    case Entry _ when currentEntry == Digit2Entry:
                        Digit3Entry.Focus();
                        break;
                    case Entry _ when currentEntry == Digit3Entry:
                        Digit4Entry.Focus();
                        break;
                    case Entry _ when currentEntry == Digit4Entry:
                        // Focus is already on the last Entry; you can perform additional actions here
                        break;
                }
            }
        }

        private async void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
            // Handle the tap gesture to navigate back to the login page
            await Shell.Current.GoToAsync(nameof(LoginPage));
        }

        [Obsolete]
        private void Button_Clicked(object sender, EventArgs e)
        {
            // Navigate to the ProcessPage
            Shell.Current.GoToAsync(nameof(ProcessPage));
        }
    }
}
