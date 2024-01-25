using MyApp.View.Account;
using MyApp.ViewModel;

namespace MyApp;

public partial class PaymentView : ContentPage
{
    
         private readonly PaymentViewModel _viewModel;

    public PaymentView(PaymentViewModel viewModel)
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
    await _viewModel.LoadDuePayments();
        await _viewModel.LoadPaymentStatus();
    }

private void ToolbarItem_Clicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync(nameof(NotificationPage));
    }
}