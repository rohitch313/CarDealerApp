using MyApp.View.Account;
using MyApp.ViewModel;

namespace MyApp;

public partial class UnregisteredView : ContentPage
{
	public UnregisteredView()
	{
		InitializeComponent();



    }

    private void ToolbarItem_Clicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync(nameof(NotificationPage));
    }
}