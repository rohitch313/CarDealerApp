//using NewDealerApp.Views.MyAccount;
//using NewDealerApp.Views.Payment;
//using NewDealerApp.Views.PurchaseVehicle;
//using NewDealerApp.Views.Rough;
using System.Reflection.Metadata;
namespace MyApp.View.Login;

public partial class LoginPage : ContentPage
{
    public LoginPage()
    {
        InitializeComponent();

        // Hide the tab bar for this page
        Shell.SetTabBarIsVisible(this, false);
    }
    private async void Button_Clicked(object sender, EventArgs e)
    {
        //     Shell.Current.GoToAsync(nameof(BasicDetailView));
        //  Navigation.PushAsync(new MobilePhone());
        await Shell.Current.GoToAsync(nameof(MobilePhone));
    }

    private void Button_Clicked_1(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync(nameof(SignUpPhone));

    }
}