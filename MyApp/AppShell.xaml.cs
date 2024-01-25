using MyApp.View.Account;
using MyApp.View.Login;
using MyApp.View.Home;
using MyApp.View.PurchaseVehicle;
using MyApp.View.Payment;

namespace MyApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(HomePage), typeof(HomePage));
            Routing.RegisterRoute(nameof(BasicDetailView), typeof(BasicDetailView));
            Routing.RegisterRoute(nameof(ProcurementDetailView), typeof(ProcurementDetailView));
            Routing.RegisterRoute(nameof(StockAuditView), typeof(StockAuditView));
            Routing.RegisterRoute(nameof(PaymentView), typeof(PaymentView));
            Routing.RegisterRoute(nameof(UpdatePictureView), typeof(UpdatePictureView));
            Routing.RegisterRoute(nameof(ImageView1), typeof(ImageView1));
            Routing.RegisterRoute(nameof(UnregisteredView), typeof(UnregisteredView));
            Routing.RegisterRoute(nameof(PayAmount), typeof(PayAmount));


            Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
            Routing.RegisterRoute(nameof(MobilePhone), typeof(MobilePhone));
            Routing.RegisterRoute(nameof(EnterOtpPage), typeof(EnterOtpPage));
            Routing.RegisterRoute(nameof(ProcessPage), typeof(ProcessPage));
            Routing.RegisterRoute(nameof(RejectedPage), typeof(RejectedPage));
            Routing.RegisterRoute(nameof(Terms), typeof(Terms));
            Routing.RegisterRoute(nameof(DocPaymentProofPage), typeof(DocPaymentProofPage));
            Routing.RegisterRoute(nameof(PaymentProovView), typeof(PaymentProovView));
           
            Routing.RegisterRoute(nameof(ProfileInformationPage), typeof(ProfileInformationPage));
            Routing.RegisterRoute(nameof(ProfileInformationPage), typeof(ProfileInformationPage));
            Routing.RegisterRoute(nameof(SignUpPhone), typeof(SignUpPhone));
            Routing.RegisterRoute(nameof(CustomerSupportViewPage), typeof(CustomerSupportViewPage));
            Routing.RegisterRoute(nameof(ProfileInfo), typeof(ProfileInfo));
            Routing.RegisterRoute(nameof(LogoutPage), typeof(LogoutPage));
            Routing.RegisterRoute(nameof(PurchaseVehicleView), typeof(PurchaseVehicleView));
            Routing.RegisterRoute(nameof(MobilePhone), typeof(MobilePhone));
            Routing.RegisterRoute(nameof(EnterOtpPageSign), typeof(EnterOtpPageSign));
            Routing.RegisterRoute(nameof(UpcomingPaymentPage), typeof(UpcomingPaymentPage));

            Application.Current.UserAppTheme = AppTheme.Light;

        }

        //private void ToolbarItem_Clicked(object sender, EventArgs e)
        //{
        //    Shell.Current.GoToAsync(nameof(NotificationPage));
        //}
    }
}