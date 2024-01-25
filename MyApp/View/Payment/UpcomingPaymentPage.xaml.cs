using MyApp.View.Account;
using MyApp.ViewModel;

namespace MyApp.View.Payment;

public partial class UpcomingPaymentPage : ContentPage
{
	public UpcomingPaymentPage(PaymentViewModel pay)
	{
		InitializeComponent();
		BindingContext = pay;
	}
    private void ToolbarItem_Clicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync(nameof(NotificationPage));
    }
    private void Button_Clicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//HomePage");
    }
}