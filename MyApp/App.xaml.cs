using MyApp.IService;
using MyApp.Service;
using Syncfusion.Licensing;
using System.IdentityModel.Tokens.Jwt;

namespace MyApp
{
    public partial class App : Application
    {
        // private readonly ITokenService _tokenRefreshService;

        public App()
        {


            InitializeComponent();


            MainPage = new AppShell();
            //_tokenRefreshService = new TokenService(new HttpClient());
        }
        protected override async void OnStart()
       {
            base.OnStart();
            //_tokenRefreshService.StartTokenRefreshTimer();
            bool tokenExists = await CheckTokenExists();

            if (tokenExists)
            {
                bool isTokenExpired = await IsTokenExpired();

                if (isTokenExpired)
                {
                    // Token exists but is expired; remove it and redirect to login
                    SecureStorage.Remove("JWTToken");
                    await Shell.Current.GoToAsync("//LoginPage");
                }
                else
                {
                    // Token exists and is not expired; navigate to home page
                    await Shell.Current.GoToAsync("//HomePage");
                }
            }
            else
            {
                // No token found; redirect to login
                await Shell.Current.GoToAsync("//LoginPage");
            }
        }

        private async Task<bool> CheckTokenExists()
        {
            try
            {
                string token = await SecureStorage.GetAsync("JWTToken");
                return !string.IsNullOrEmpty(token);
            }
            catch (Exception ex)
            {
                // Handle exceptions related to token retrieval
                return false;
            }
        }

        private async Task<bool> IsTokenExpired()
        {
            try
            {
                string token = await SecureStorage.GetAsync("JWTToken");

                if (!string.IsNullOrEmpty(token))
                {
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var jwtToken = tokenHandler.ReadJwtToken(token);

                    // Check the token expiration
                    return jwtToken.ValidTo < DateTime.UtcNow;
                }

                return true; // Token not found or empty
            }
            catch (Exception ex)
            {
                // Handle exceptions related to token retrieval or parsing
                return true; // Consider expired for safety
            }
        }
    }
}

