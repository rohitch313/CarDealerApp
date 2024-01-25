using MyApp.View.Account;
using MyApp.ViewModel;

namespace MyApp;

public partial class StockAuditView : ContentPage
{
    private readonly CarViewModel _carViewModel;
    public StockAuditView(CarViewModel viewModel)
    {
        InitializeComponent();
        _carViewModel = viewModel;
        BindingContext = _carViewModel;
        Application.Current.UserAppTheme = AppTheme.Light;

    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await LoadData();
    }

    private async Task LoadData()
    {
        await _carViewModel.LoadStatusAudit();
        await _carViewModel.LoadPendingAudit();
    }


    private void ToolbarItem_Clicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync(nameof(NotificationPage));
    }
}