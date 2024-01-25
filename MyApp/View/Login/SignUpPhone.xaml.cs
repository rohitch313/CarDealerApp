using MyApp.Service;
using MyApp.ViewModel;

namespace MyApp.View.Login
{
    public partial class SignUpPhone : ContentPage
    {
       
        public SignUpPhone(SignUpViewModel view)

        {
            InitializeComponent();
            ; // Provide necessary dependencies here
            BindingContext = view;
        }
    }
}

