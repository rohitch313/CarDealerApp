namespace MyApp.View.Account;

public partial class Terms : ContentPage
{
	public Terms()
	{
		InitializeComponent();
        Shell.SetTabBarIsVisible(this, false);
    
	}

    private void ToolbarItem_Clicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync(nameof(NotificationPage));
    }
}