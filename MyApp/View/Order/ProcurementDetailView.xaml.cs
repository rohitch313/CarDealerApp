using MyApp.View.Account;
using MyApp.ViewModel;

namespace MyApp;

public partial class ProcurementDetailView : ContentPage
{
	public ProcurementDetailView(ProcurementViewModel procurementViewModel)
	{
   
        InitializeComponent();
        BindingContext = procurementViewModel;
     

    }
    private void ToolbarItem_Clicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync(nameof(NotificationPage));
    }
}