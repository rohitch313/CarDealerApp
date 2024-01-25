using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using MyApp.View.Account;
using MyApp.View.Home;
using MyApp.ViewModel;

namespace MyApp;

public partial class UpdatePictureView : ContentPage 
{
    private string localFilePath;
    private bool IsImageUploaded1 = false;
    private bool IsImageUploaded2 = false;
    private bool IsImageUploaded3 = false;
    public UpdatePictureView()
    {
        InitializeComponent();
        BindingContext = new UpdatePictureViewModel();

    }
    private async void CameraTapped1(object sender, EventArgs e)
    {
        try
        {
            if (IsImageUploaded1)
            {
                await Shell.Current.Navigation.PushAsync(new ImageView1(localFilePath));
            }
            else
            {
                if (MediaPicker.Default.IsCaptureSupported)
                {
                    FileResult myPhoto = await MediaPicker.Default.CapturePhotoAsync();
                    if (myPhoto != null)
                    {
                         localFilePath = Path.Combine(FileSystem.CacheDirectory, myPhoto.FileName);
                        using (Stream sourceStream = await myPhoto.OpenReadAsync())
                        {
                            using (FileStream localFileStream = File.OpenWrite(localFilePath))
                            {
                                await sourceStream.CopyToAsync(localFileStream);
                            }
                        }

                        // Set the Source property of the ImageButton to display the captured photo
                        capturedImage.Source = ImageSource.FromFile(localFilePath);

                        stepper.IsVisible = false;
                        stepperdark.IsVisible = true;
                        changeImageButton1.IsVisible = true;

                        IsImageUploaded1 = true;

                    }
                    else
                    {
                        await Shell.Current.DisplayAlert("Error", "Failed to capture a photo.", "Ok");
                    }
                }
                else
                {
                    await Shell.Current.DisplayAlert("Error", "Your device isn't supported for capturing photos.", "Ok");
                }
            }
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error", $"An error occurred: {ex.Message}", "Ok");
        }
        plusiconOne.IsVisible = false;
    }
    private async void ChangeImage1(object sender, EventArgs e)
    {
        try
        {
            FileResult myPhoto = await MediaPicker.Default.CapturePhotoAsync();
            if (myPhoto != null)
            {
                string localFilePath = Path.Combine(FileSystem.CacheDirectory, myPhoto.FileName);
                using (Stream sourceStream = await myPhoto.OpenReadAsync())
                {
                    using (FileStream localFileStream = File.OpenWrite(localFilePath))
                    {
                        await sourceStream.CopyToAsync(localFileStream);
                    }
                }

                // Set the Source property of the ImageButton to display the captured photo
                capturedImage.Source = ImageSource.FromFile(localFilePath);
                IsImageUploaded1 = true;


            }
            else
            {
                await Shell.Current.DisplayAlert("Error", "Failed to capture a photo.", "Ok");
            }
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error", $"An error occurred: {ex.Message}", "Ok");
        }
    }

    // Similar methods for ChangeImage2 and ChangeImage3
    private async void CameraTapped2(object sender, EventArgs e)
    {
        try
        {
            if (IsImageUploaded2)
            {
                await Shell.Current.DisplayAlert("Alert", "Image already uploaded!", "Ok");
            }
            else
            {
                if (MediaPicker.Default.IsCaptureSupported)
                {
                    FileResult myPhoto = await MediaPicker.Default.CapturePhotoAsync();
                    if (myPhoto != null)
                    {
                        localFilePath = Path.Combine(FileSystem.CacheDirectory, myPhoto.FileName);
                        using (Stream sourceStream = await myPhoto.OpenReadAsync())
                        {
                            using (FileStream localFileStream = File.OpenWrite(localFilePath))
                            {
                                await sourceStream.CopyToAsync(localFileStream);
                            }
                        }

                        // Set the Source property of the ImageButton to display the captured photo
                        capturedImage2.Source = ImageSource.FromFile(localFilePath);

                        stepper1.IsVisible = false;
                        stepper1dark.IsVisible = true;
                        changeImageButton2.IsVisible = true;

                        IsImageUploaded2 = true;


                    }
                    else
                    {
                        await Shell.Current.DisplayAlert("Error", "Failed to capture a photo.", "Ok");
                    }
                }
                else
                {
                    await Shell.Current.DisplayAlert("Error", "Your device isn't supported for capturing photos.", "Ok");
                }
            }
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error", $"An error occurred: {ex.Message}", "Ok");
        }
        plusiconTwo.IsVisible = false;
    }

    private void ToolbarItem_Clicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync(nameof(NotificationPage));
    }
    private async void ChangeImage2(object sender, EventArgs e)
    {
        try
        {
            FileResult myPhoto = await MediaPicker.Default.CapturePhotoAsync();
            if (myPhoto != null)
            {
                 localFilePath = Path.Combine(FileSystem.CacheDirectory, myPhoto.FileName);
                using (Stream sourceStream = await myPhoto.OpenReadAsync())
                {
                    using (FileStream localFileStream = File.OpenWrite(localFilePath))
                    {
                        await sourceStream.CopyToAsync(localFileStream);
                    }
                }

                // Set the Source property of the ImageButton to display the captured photo
                capturedImage2.Source = ImageSource.FromFile(localFilePath);
                IsImageUploaded2 = true;


            }
            else
            {
                await Shell.Current.DisplayAlert("Error", "Failed to capture a photo.", "Ok");
            }
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error", $"An error occurred: {ex.Message}", "Ok");
        }
    }

    private async void CameraTapped3(object sender, EventArgs e)
    {
        try
        {
            if (IsImageUploaded3)
            {
                await Shell.Current.DisplayAlert("Alert", "Image already uploaded!", "Ok");
            }
            else
            {
                if (MediaPicker.Default.IsCaptureSupported)
                {
                    FileResult myPhoto = await MediaPicker.Default.CapturePhotoAsync();
                    if (myPhoto != null)
                    {
                        localFilePath = Path.Combine(FileSystem.CacheDirectory, myPhoto.FileName);
                        using (Stream sourceStream = await myPhoto.OpenReadAsync())
                        {
                            using (FileStream localFileStream = File.OpenWrite(localFilePath))
                            {
                                await sourceStream.CopyToAsync(localFileStream);
                            }
                        }

                        // Set the Source property of the ImageButton to display the captured photo
                        capturedImage3.Source = ImageSource.FromFile(localFilePath);

                        stepper2.IsVisible = false;
                        stepper2dark.IsVisible = true;
                        changeImageButton3.IsVisible = true;

                        IsImageUploaded3 = true;


                    }
                    else
                    {
                        await Shell.Current.DisplayAlert("Error", "Failed to capture a photo.", "Ok");
                    }
                }
                else
                {
                    await Shell.Current.DisplayAlert("Error", "Your device isn't supported for capturing photos.", "Ok");
                }
            }
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error", $"An error occurred: {ex.Message}", "Ok");
        }
        plusiconThree.IsVisible=false;
    }

    private async void ChangeImage3(object sender, EventArgs e)
    {
        try
        {
            FileResult myPhoto = await MediaPicker.Default.CapturePhotoAsync();
            if (myPhoto != null)
            {
                 localFilePath = Path.Combine(FileSystem.CacheDirectory, myPhoto.FileName);
                using (Stream sourceStream = await myPhoto.OpenReadAsync())
                {
                    using (FileStream localFileStream = File.OpenWrite(localFilePath))
                    {
                        await sourceStream.CopyToAsync(localFileStream);
                    }
                }

                // Set the Source property of the ImageButton to display the captured photo
                capturedImage3.Source = ImageSource.FromFile(localFilePath);
                IsImageUploaded3 = true;


            }
            else
            {
                await Shell.Current.DisplayAlert("Error", "Failed to capture a photo.", "Ok");
            }
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error", $"An error occurred: {ex.Message}", "Ok");
        }
    }

    private bool AreAllImagesUploaded()
    {
        return IsImageUploaded1 && IsImageUploaded2 && IsImageUploaded3;
    }

    private async void back1(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(HomePage));
    }
    
    private async void SubmitButtonClicked(object sender, EventArgs e)
    {
        try
        {
            if (AreAllImagesUploaded())
            {
                await Shell.Current.GoToAsync(nameof(StockAuditView));
                var popup = new ImageVerificationPopup();
                Shell.Current.CurrentPage.ShowPopup(popup);
            }
            else
            {
                await DisplayAlert("Error", "Please upload all images.", "Ok");
            }
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error", $"An error occurred: {ex.Message}", "Ok");
        }
    }
}
