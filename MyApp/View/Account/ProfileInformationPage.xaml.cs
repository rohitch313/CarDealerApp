

using MyApp.Service;
using MyApp.ViewModel;

namespace MyApp.View.Account;

public partial class ProfileInformationPage : ContentPage
{
    private readonly ProfileViewmodel _viewModel;
    public ProfileInformationPage()
    {

        InitializeComponent();
        _viewModel = new ProfileViewmodel(new ProfileInformationService(new HttpClient())); // Provide necessary dependencies here
        BindingContext = _viewModel;
        Shell.SetTabBarIsVisible(this, false);

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
}
