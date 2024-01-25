

using MyApp.Service;
using MyApp.ViewModel;

namespace MyApp.View.Login
{
    public partial class MobilePhone : ContentPage
    {
        
        // private string enteredPhoneNumber;

        public MobilePhone(PostLoginViewModel _viewModel)
        {
            InitializeComponent();
            // Provide necessary dependencies here
            BindingContext = _viewModel;
        }


    }
}
