

using MyApp.View.Account;
using MyApp.ViewModel;

namespace MyApp;

public partial class DocPaymentProofPage : ContentPage
{
    public DocPaymentProofPage(PaymentViewModel paymentViewModel)
    {
        InitializeComponent();
        BindingContext = paymentViewModel;
    }

    // Define a property to set the image source
    public ImageSource CapturedImageSource
    {
        get { return capturedImage.Source; }
        set
        {
            capturedImage.Source = value;


        }
    }

    private void ToolbarItem_Clicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync(nameof(NotificationPage));
    }

    private async void capturedImage_Clicked(object sender, EventArgs e)
    {

        try
        {
            // Open the bottom sheet (PaymentProovView)
            var paymentProovView = new PaymentProovView(this);

            // Show the bottom sheet
            await paymentProovView.ShowAsync();

            // Wait for a delay (if needed)
            await Task.Delay(5000);
           
            // Dismiss the bottom sheet
            await paymentProovView.DismissAsync();
            plusicon.IsVisible = false;

            await Task.Delay(10000);

            // Access the selected image file name
            string imageName = paymentProovView.SelectedImageFileName;

            // Set the Text property of priceimg to the selected image file name
            ImagebtnName.Text = imageName;
         
        }
        catch (Exception ex)
        {
            // Handle exceptions if any
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }

    private void Button_Clicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync(nameof(PayAmount));
    }


}
