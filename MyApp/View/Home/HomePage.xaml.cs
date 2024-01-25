using MyApp.View.Account;
using MyApp.ViewModel;

namespace MyApp.View.Home;

[XamlCompilation(XamlCompilationOptions.Skip)]
public partial class HomePage : ContentPage
{
    private readonly PaymentViewModel _viewModel;
    
    public HomePage(PaymentViewModel viewModel)
	{
		InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;



    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await LoadData();
    }

    private async Task LoadData()
    {
        await _viewModel.LoadUpcomingPayment();
        await _viewModel.LoadUpcomingAudit();
        await _viewModel.RetrieveAndSetUsername();

    }
    private void ToolbarItem_Clicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync(nameof(NotificationPage));
    }
}