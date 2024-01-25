using MyApp.ViewModel;

namespace MyApp;

public partial class ImageView1 : ContentPage
{
    public ImageView1(string imagePath)
    {
        InitializeComponent();
        BindingContext = new ImageViewModel();
        fullScreenImage.Source = imagePath;

    }

    
}
