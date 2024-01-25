using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using MyApp.IService;
using MyApp.Service;
using MyApp.Services;
using MyApp.View.Account;
using MyApp.View.Home;
using MyApp.View.Login;
using MyApp.View.Payment;
using MyApp.View.PurchaseVehicle;
using MyApp.ViewModel;
using Syncfusion.Maui.Core.Hosting;
using The49.Maui.BottomSheet;

namespace MyApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                  .ConfigureSyncfusionCore()
                .UseMauiCommunityToolkit()
                .UseBottomSheet()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("NotoSans-Medium.ttf", "AppTextFont");
                });
            builder.Services.AddSingleton<ProcurementDetailView>();
            builder.Services.AddSingleton<HomePage>();
            builder.Services.AddSingleton<ImageViewModel>();
            builder.Services.AddSingleton<HomeViewModel>();
            builder.Services.AddSingleton<StockAuditView>();
            builder.Services.AddSingleton<PaymentView>();
            builder.Services.AddSingleton<ImageView1>();
            builder.Services.AddSingleton<BasicDetailView>();
            builder.Services.AddSingleton<CarViewModel>();
            builder.Services.AddSingleton<BasicDetailsViewModel>();
            builder.Services.AddSingleton<UnregisteredView>();
            builder.Services.AddSingleton<VerificationViewModel>();
            builder.Services.AddSingleton<PaymentViewModel>();
            builder.Services.AddSingleton<PayAmount>();
            builder.Services.AddSingleton<IProcurementService, ProcurementService>();
            builder.Services.AddSingleton<UpcomingPaymentPage>();
            builder.Services.AddSingleton<PayAmountViewModel>();
            builder.Services.AddSingleton<ProcurementViewModel>();
            builder.Services.AddSingleton<VehicleRecordsViewModel>();
            builder.Services.AddSingleton<PurchaseVehicleView>();
            builder.Services.AddSingleton<IPaymnetService, PaymentService>();
            builder.Services.AddSingleton<IStockAuditService, StockAuditService>();
            builder.Services.AddSingleton<IPurchaseVehicleService, PurchaseVehicleService>();
            builder.Services.AddSingleton<HttpClient>();
            builder.Services.AddScoped<IBasicDetailsService, BasicDetailsService>();
            builder.Services.AddTransient<BasicDetailsViewModel>();
            builder.Services.AddTransient<LoginPage>();
            builder.Services.AddTransient<BasicDetailView>();
            builder.Services.AddTransient<BasicDetailsViewModel>();
            builder.Services.AddTransient<IBasicDetailsService, BasicDetailsService>();
            builder.Services.AddTransient<IFullAggragatorService, FullAggragatorService>();
            builder.Services.AddTransient<FullAggragatorViewModel>();
            builder.Services.AddSingleton<IPostLogInService, PostLogInService>();
            builder.Services.AddSingleton<ProfileInfo>();
            builder.Services.AddSingleton<EnterOtpPageSign>();
            builder.Services.AddSingleton<SignUpViewModel>();
            builder.Services.AddSingleton<SignUpViewModel>();
            builder.Services.AddSingleton<SignUpPhone>();
            builder.Services.AddSingleton<PostLoginViewModel>();
            builder.Services.AddSingleton<MobilePhone>();
            builder.Services.AddSingleton<EnterOtpPage>();
            builder.Services.AddSingleton<DocPaymentProofPage>();
            //   builder.Services.AddSingleton<ISignUpService, SignUpService>();

            builder.Services.AddHttpClient();

#if DEBUG
            builder.Logging.AddDebug();
#endif
            Microsoft.Maui.Handlers.EntryHandler.Mapper.AppendToMapping(nameof(Entry), (handler, view) =>
            {
#if ANDROID
handler.PlatformView.SetBackgroundColor(Android.Graphics.Color.Transparent); 
#elif IOS
handler.PlatformView.BackgroundColor = UIKit.UIColor.Clear;   
handler.PlatformView.BorderStyle = UIKit.UITextBorderStyle.None;
#endif
            });

            Microsoft.Maui.Handlers.PickerHandler.Mapper.AppendToMapping("Borderless", (handler, view) =>
            {
#if ANDROID
            handler.PlatformView.Background = null;
            handler.PlatformView.SetBackgroundColor(Android.Graphics.Color.Transparent);
#elif IOS
                handler.PlatformView.BackgroundColor = UIKit.UIColor.Clear;
                handler.PlatformView.Layer.BorderWidth = 0;
                handler.PlatformView.BorderStyle = UIKit.UITextBorderStyle.None;
#endif
            });
            Microsoft.Maui.Handlers.DatePickerHandler.Mapper.AppendToMapping("Borderless", (handler, view) =>
            {
#if ANDROID
                handler.PlatformView.Background = null;
                handler.PlatformView.SetBackgroundColor(Android.Graphics.Color.Transparent);
#elif IOS
                handler.PlatformView.BackgroundColor = UIKit.UIColor.Clear;
                handler.PlatformView.Layer.BorderWidth = 0;
                handler.PlatformView.BorderStyle = UIKit.UITextBorderStyle.None;
#endif
            });

            return builder.Build();
        }
    }
}