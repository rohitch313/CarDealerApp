
using MyApp;
using MyApp.View.PurchaseVehicle;
using The49.Maui.BottomSheet;

namespace MyApp
{
    public partial class PaymentProovView : BottomSheet
    {
        private DocPaymentProofPage _parentPage;

        private PurchaseVehicleView _purchaseVehicleView;
        public string SelectedImageFileName { get; private set; }

        public PaymentProovView(PurchaseVehicleView purchaseVehicleView)
        {
            _purchaseVehicleView = purchaseVehicleView;
            InitializeComponent();
        }

        public PaymentProovView(DocPaymentProofPage parentPage)
        {
            _parentPage = parentPage;
            InitializeComponent();
        }



        public void HideBottomSheet()
        {
            // You may implement your own custom logic to hide the view.
            // For example, setting the view's visibility to false.
            IsVisible = false;
        }

        // Moved the common error handling to a separate method to avoid repetition.
        private async Task DisplayErrorAlert(string errorMessage)
        {
            if (_parentPage != null)
            {
                await _parentPage.DisplayAlert("Error", errorMessage, "Ok");
            }

        }

        private async void CameraTapped(object sender, EventArgs e)
        {
            try
            {
                if (MediaPicker.Default.IsCaptureSupported)
                {
                    FileResult myPhoto = await MediaPicker.Default.CapturePhotoAsync();

                    if (myPhoto != null)

                    {
                        // Save the captured photo to a local file
                        string localFilePath = Path.Combine(FileSystem.CacheDirectory, myPhoto.FileName);

                        SelectedImageFileName = myPhoto.FileName;
                        using (Stream sourceStream = await myPhoto.OpenReadAsync())
                        using (FileStream localFileStream = File.OpenWrite(localFilePath))
                        {
                            await sourceStream.CopyToAsync(localFileStream);
                        }

                        if (_parentPage != null)
                        {
                            _parentPage.CapturedImageSource = ImageSource.FromFile(localFilePath);
                        }

                    }
                    else
                    {
                        await DisplayErrorAlert("Failed to capture a photo.");
                    }
                }
                else
                {
                    await DisplayErrorAlert("Your device isn't supported for capturing photos.");
                }
            }
            catch (Exception ex)
            {
                await DisplayErrorAlert($"An error occurred: {ex.Message}");
            }
        }

        private async void PhotosTapped(object sender, EventArgs e)
        {
            if (MediaPicker.Default.IsCaptureSupported)
            {
                FileResult myPhoto = await MediaPicker.Default.PickPhotoAsync();
                if (myPhoto != null)
                {
                    string localFilePath = Path.Combine(FileSystem.CacheDirectory, myPhoto.FileName);
                    SelectedImageFileName = myPhoto.FileName;
                    using (Stream sourceStream = await myPhoto.OpenReadAsync())
                    using (FileStream localFileStream = File.OpenWrite(localFilePath))
                    {
                        await sourceStream.CopyToAsync(localFileStream);
                    }

                    if (_parentPage != null)
                    {
                        _parentPage.CapturedImageSource = ImageSource.FromFile(localFilePath);
                    }

                }
            }
            else
            {
                await DisplayErrorAlert("Your device isn't supported for picking photos.");
            }
        }

        private async void DocumentTapped(object sender, EventArgs e)
        {
            if (MediaPicker.Default.IsCaptureSupported)
            {
                FileResult selectedDocument = await MediaPicker.Default.PickPhotoAsync();
                if (selectedDocument != null)
                {
                    // Handle the selected document as needed
                    // For example, you can save it to a specific location
                    // and display it in your app, but the exact handling depends on your requirements.
                    string documentPath = await HandleSelectedDocument(selectedDocument);
                    // You can display the document if necessary.
                }
            }
            else
            {
                await DisplayErrorAlert("Your device isn't supported for picking documents.");
            }
        }

        private async Task<string> HandleSelectedDocument(FileResult selectedDocument)
        {
            // Handle the selected document here, such as saving it to a specific location.
            string localFilePath = Path.Combine(FileSystem.CacheDirectory, selectedDocument.FileName);
            SelectedImageFileName = selectedDocument.FileName;
            using (Stream sourceStream = await selectedDocument.OpenReadAsync())
            using (FileStream localFileStream = File.OpenWrite(localFilePath))
            {
                await sourceStream.CopyToAsync(localFileStream);
            }
            return localFilePath;
        }
    }
}
