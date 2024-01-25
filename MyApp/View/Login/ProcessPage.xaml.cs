using MyApp.View.Home;

namespace MyApp.View.Login;

public partial class ProcessPage : ContentPage
{
    [Obsolete]
    public ProcessPage()
    {
        InitializeComponent();


      //  LoadRejectedPageWithDelay();
    }

    //[Obsolete]
    //private  async void LoadRejectedPageWithDelay()
    //{
    //    // Create a timer with a delay of 5 seconds (5000 milliseconds)
    //    Timer timer = new Timer(TimerCallback, null, 1000, Timeout.Infinite);
    
      
    //}

    //[Obsolete]
    //private async void TimerCallback(object state)
    //{
    //    // This method will be executed after the specified delay (5 seconds)

    //    // You can navigate to the RejectedPage here
    //    await Device.InvokeOnMainThreadAsync(async () =>
    //    {
    //         Shell.Current.Navigation.PopToRootAsync(); // Clears the navigation stack
    //       await  Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
    //    });
    //}
}

