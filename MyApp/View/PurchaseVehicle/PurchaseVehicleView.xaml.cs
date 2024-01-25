using CommunityToolkit.Maui.Views;
using MyApp.Models;
using MyApp.Service;
using MyApp.View.Account;
using MyApp.View.Login;
using MyApp.ViewModel;

namespace MyApp.View.PurchaseVehicle;

[XamlCompilation(XamlCompilationOptions.Skip)]


public partial class PurchaseVehicleView : ContentPage
{
    private readonly FullAggragatorViewModel _viewModel;
    public PurchaseVehicleView(FullAggragatorViewModel model)
    {
        InitializeComponent();
        BindingContext = model;


        // BindingContext = new VehicleRecordsViewModel();
        SectionB.IsVisible = false;
        Section2.IsVisible = false;
        Section3.IsVisible = false;
        Section11.IsVisible = true;
        Section22.IsVisible = false;


        // Attach event handlers to the buttons
        SignButton1.Clicked += OnSignButtonClicked;
        DownloadButton.Clicked += OnDownloadButtonClicked;
        SignButton.Clicked += Sign_Clicked;
        MarketButton.Clicked += Market_Button_Clicked;
        New_CarButton.Clicked += New_Car_Clicked;
     


    }

    private ImageSource _capturedImageSource;

    public ImageSource CapturedImageSource
    {
        get { return _capturedImageSource; }
        set
        {
            _capturedImageSource = value;

        }
    }

    private void Sign_Clicked(object sender, EventArgs e)
    {
        // Show section A and hide section B
        Section1.IsVisible = true;
        Section2.IsVisible = false;
        Section3.IsVisible = false;
    }


    private void Market_Button_Clicked(object sender, EventArgs e)
    {
        // Show section A and hide section B
        Section2.IsVisible = true;
        Section1.IsVisible = false;
        Section3.IsVisible = false;
    }

    private void New_Car_Clicked(object sender, EventArgs e)
    {
        // Show section A and hide section B
        Section3.IsVisible = true;
        Section1.IsVisible = false;
        Section2.IsVisible = false;
    }

    private void OnSignButtonClicked(object sender, EventArgs e)
    {
        // Show section A and hide section B
        SectionC.IsVisible = true;
        SectionB.IsVisible = false;
    }

    private void OnDownloadButtonClicked(object sender, EventArgs e)
    {
        // Show section B and hide section A
        SectionC.IsVisible = false;
        SectionB.IsVisible = true;
    }

    private void Vehicle1_Clicked(object sender, EventArgs e)
    {
        // Show section B and hide section A
        Section11.IsVisible = false;
        Section22.IsVisible = true;
    }
    private void Vehicle2_Clicked(object sender, EventArgs e)
    {
        // Show section B and hide section A
        Section11.IsVisible = false;
        Section22.IsVisible = true;
    }

    private void ToolbarItem_Clicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync(nameof(NotificationPage));
    }


    private async void OpenbootomPrice(object sender, EventArgs e)
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

            await Task.Delay(5000);
            // Access the selected image file name
            string imageName = paymentProovView.SelectedImageFileName;

            // Set the Text property of priceimg to the selected image file name
            priceimg.Text = imageName;
        }
        catch (Exception ex)
        {
            // Handle exceptions if any
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }
    private async void Openbootomstock(object sender, EventArgs e)
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

            await Task.Delay(5000);
            // Access the selected image file name
            string imageName = paymentProovView.SelectedImageFileName;

            // Set the Text property of priceimg to the selected image file name
            stockinimg.Text = imageName;
            //string old = imageName;
        }
        catch (Exception ex)
        {
            // Handle exceptions if any
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }
    private async void OpenbootomRc(object sender, EventArgs e)
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

            await Task.Delay(5000);
            // Access the selected image file name
            string imageName = paymentProovView.SelectedImageFileName;

            // Set the Text property of priceimg to the selected image file name
            rcimg.Text = imageName;
        }
        catch (Exception ex)
        {
            // Handle exceptions if any
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }
    private async void OpenbootomPaymentProof(object sender, EventArgs e)
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

            await Task.Delay(5000);
            // Access the selected image file name
            string imageName = paymentProovView.SelectedImageFileName;

            // Set the Text property of priceimg to the selected image file name
            paymentproof.Text = imageName;
        }
        catch (Exception ex)
        {
            // Handle exceptions if any
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }
    private async void OpenbootomSellerAdhar(object sender, EventArgs e)
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

            await Task.Delay(5000);
            // Access the selected image file name
            string imageName = paymentProovView.SelectedImageFileName;

            // Set the Text property of priceimg to the selected image file name
            sellerAdhar.Text = imageName;
        }
        catch (Exception ex)
        {
            // Handle exceptions if any
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }
    private async void OpenbootomSellerPan(object sender, EventArgs e)
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

            await Task.Delay(5000);
            // Access the selected image file name
            string imageName = paymentProovView.SelectedImageFileName;

            // Set the Text property of priceimg to the selected image file name
            sellerPan.Text = imageName;
        }
        catch (Exception ex)
        {
            // Handle exceptions if any
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }
    private async void OpenbootomPicOfOrigiRC(object sender, EventArgs e)
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

            await Task.Delay(5000);
            // Access the selected image file name
            string imageName = paymentProovView.SelectedImageFileName;

            // Set the Text property of priceimg to the selected image file name
            picoforgRc.Text = imageName;
        }
        catch (Exception ex)
        {
            // Handle exceptions if any
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }
    private async void OpenbootomOdometerPic(object sender, EventArgs e)
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

            await Task.Delay(5000);
            // Access the selected image file name
            string imageName = paymentProovView.SelectedImageFileName;

            // Set the Text property of priceimg to the selected image file name
            odoPic.Text = imageName;
        }
        catch (Exception ex)
        {
            // Handle exceptions if any
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }
    private async void OpenbootomVehiclePicFront(object sender, EventArgs e)
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

            await Task.Delay(5000);
            // Access the selected image file name
            string imageName = paymentProovView.SelectedImageFileName;

            // Set the Text property of priceimg to the selected image file name
            vehiclepicFront.Text = imageName;
        }
        catch (Exception ex)
        {
            // Handle exceptions if any
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }
    private async void OpenbootomVehiclePicBack(object sender, EventArgs e)
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

            await Task.Delay(5000);
            // Access the selected image file name
            string imageName = paymentProovView.SelectedImageFileName;

            // Set the Text property of priceimg to the selected image file name
            vehiclepicBack.Text = imageName;
        }
        catch (Exception ex)
        {
            // Handle exceptions if any
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }
    private async void OpenbootomNCDOdoPic(object sender, EventArgs e)
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

            await Task.Delay(5000);
            // Access the selected image file name
            string imageName = paymentProovView.SelectedImageFileName;

            // Set the Text property of priceimg to the selected image file name
            NCDOdoPic.Text = imageName;
        }
        catch (Exception ex)
        {
            // Handle exceptions if any
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }
    private async void OpenbootomNCDVehicleFrontPic(object sender, EventArgs e)
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

            await Task.Delay(5000);
            // Access the selected image file name
            string imageName = paymentProovView.SelectedImageFileName;

            // Set the Text property of priceimg to the selected image file name
            NCDFrontPic.Text = imageName;
        }
        catch (Exception ex)
        {
            // Handle exceptions if any
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }
    private async void OpenbootomNCDVehicleBackPic(object sender, EventArgs e)
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

            await Task.Delay(5000);
            // Access the selected image file name
            string imageName = paymentProovView.SelectedImageFileName;

            // Set the Text property of priceimg to the selected image file name
            NCDBackPic.Text = imageName;
        }
        catch (Exception ex)
        {
            // Handle exceptions if any
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }
    private async void OpenbootomNCDInvoicePic(object sender, EventArgs e)
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

            await Task.Delay(5000);
            // Access the selected image file name
            string imageName = paymentProovView.SelectedImageFileName;

            // Set the Text property of priceimg to the selected image file name
            NCDInvoice.Text = imageName;
        }
        catch (Exception ex)
        {
            // Handle exceptions if any
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }
    private async void OpenbootomNCDPicRcPic(object sender, EventArgs e)
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

            await Task.Delay(5000);
            // Access the selected image file name
            string imageName = paymentProovView.SelectedImageFileName;

            // Set the Text property of priceimg to the selected image file name
            NCDRcPic.Text = imageName;
        }
        catch (Exception ex)
        {
            // Handle exceptions if any
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }

}


