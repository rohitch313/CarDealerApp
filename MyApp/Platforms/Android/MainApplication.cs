using Android.App;
using Android.Runtime;
using AndroidX.AppCompat.App;

namespace MyApp
{
#if DEBUG
    [Application(UsesCleartextTraffic = true)]
#else
    [Application]
#endif

    public class MainApplication : MauiApplication
    {
        public MainApplication(IntPtr handle, JniHandleOwnership ownership)
            : base(handle, ownership)
        {
            AppCompatDelegate.DefaultNightMode = AppCompatDelegate.ModeNightNo; // Set the night mode to "night no"
        }

        protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();
    }
}