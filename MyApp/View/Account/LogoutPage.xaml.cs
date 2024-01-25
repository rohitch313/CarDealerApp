using CommunityToolkit.Maui.Views;
using MyApp.IService;
using MyApp.Service;
using MyApp.View.Login;
using MyApp.ViewModel;

namespace MyApp.View.Account;

public partial class LogoutPage : Popup
{
    private readonly PostLoginViewModel postLogin;
    private readonly PostLoginViewModel _viewModel;
    private readonly IPostLogInService _postLogInService;
    public LogoutPage(IPostLogInService postLogInService)
    {
        InitializeComponent();
        _postLogInService = postLogInService;
        BindingContext = _viewModel;
        // Save the reference to the ViewModel
    }
    private async void Button_Clicked_Yes(object sender, EventArgs e)
    {
        try
        {
            bool loggedOut = await _postLogInService.Logout();

            if (loggedOut)
            {
                SecureStorage.Remove("JWTToken");

                await Shell.Current.Navigation.PopToRootAsync(); // Clears the navigation stack

                await Shell.Current.GoToAsync($"/{nameof(LoginPage)}");

            }
            else
            {
                // Show an error message or handle accordingly if logout fails
                await Shell.Current.DisplayAlert("Logout Failed", "Failed to logout. Please try again.", "OK");
            }

            popupmessage.IsVisible = false;
        }
        catch (Exception ex)
        {
            // Handle exceptions
        }
    }



    private void Button_Clicked_No(object sender, EventArgs e)
    {
        // Shell.Current.GoToAsync(nameof(ProfileInfo));

        popupmessage.IsVisible = false;
    }
}