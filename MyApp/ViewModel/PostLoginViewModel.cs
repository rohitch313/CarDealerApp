// Viewmodel

using System;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MyApp.IService;
using MyApp.Service;
using MyApp.View.Login;

namespace MyApp.ViewModel
{
    public partial class PostLoginViewModel : ObservableObject
    {
        private readonly IPostLogInService _postLoginService;

        public PostLoginViewModel(IPostLogInService postLoginService)
        {
            _postLoginService = postLoginService;
            OTPDigit1 = null;
            OTPDigit2 = null;
            OTPDigit3 = null;
            OTPDigit4 = null;


        }
        private string phone;
        public string Phone
        {
            get => phone;
            set
            {
                phone = value;
                OnPropertyChanged(nameof(Phone));
                MaskedPhoneNumber = GetMaskedPhoneNumber();
            }
        }


        private int? _otpDigit1;
        private int? _otpDigit2;
        private int? _otpDigit3;
        private int? _otpDigit4;
        private int? _otp; // Combined OTP

        public int? OTPDigit1
        {
            get => _otpDigit1;
            set
            {
                _otpDigit1 = value;
                OnPropertyChanged(nameof(OTPDigit1));
                UpdateCombinedOTP();
            }
        }
        public int? OTPDigit2
        {
            get => _otpDigit2;
            set
            {
                _otpDigit2 = value;
                OnPropertyChanged(nameof(OTPDigit2));
                UpdateCombinedOTP();
            }
        }
        public int? OTPDigit3
        {
            get => _otpDigit3;
            set
            {
                _otpDigit3 = value;
                OnPropertyChanged(nameof(OTPDigit3));
                UpdateCombinedOTP();
            }
        }
        public int? OTPDigit4
        {
            get => _otpDigit4;
            set
            {
                _otpDigit4 = value;
                OnPropertyChanged(nameof(OTPDigit4));
                UpdateCombinedOTP();
            }
        }
        // Implement OTPDigit2, OTPDigit3, and OTPDigit4 similarly...

        public int? OTP
        {
            get => _otp;
            set
            {
                _otp = value;
                OnPropertyChanged(nameof(OTP));
            }
        }
        private bool isResendEnabled;
        private Color resendTextColor;


        public bool IsResendEnabled
        {
            get => isResendEnabled;
            set => SetProperty(ref isResendEnabled, value);
        }

        public Color ResendTextColor
        {
            get => resendTextColor;
            set => SetProperty(ref resendTextColor, value);
        }
        private string maskedPhoneNumber;
        public string MaskedPhoneNumber
        {
            get => maskedPhoneNumber;
            set
            {
                maskedPhoneNumber = value;
                OnPropertyChanged(nameof(MaskedPhoneNumber));
            }
        }

        // This method will return the masked phone number
        private string GetMaskedPhoneNumber()
        {
            if (!string.IsNullOrEmpty(Phone) && Phone.Length > 2)
            {
                return new string('*', Phone.Length - 2) + Phone.Substring(Phone.Length - 2);
            }
            return string.Empty;
        }
        private async Task ResendOtpWithDelay()
        {
            // Simulate a delay of 1 minute
            await Task.Delay(TimeSpan.FromMinutes(1));

            IsResendEnabled = true;
            ResendTextColor = Color.FromHex("#4AA09B"); // Green color
        }

        private void UpdateCombinedOTP()
        {
            // Concatenate individual OTP digits to form the complete OTP
            OTP = OTPDigit1 * 1000 + OTPDigit2 * 100 + OTPDigit3 * 10 + OTPDigit4;
        }
        private bool _isLoggingOut;

        public bool IsLoggingOut
        {
            get => _isLoggingOut;
            set => SetProperty(ref _isLoggingOut, value);
        }
        [RelayCommand]
        public async Task<bool> PostLoginAsync()
        {
            try
            {
                // Call the service to check if the phone number is valid and get the token
                bool result = await _postLoginService.PostLoginNumberAsync(phone);
                if (result)
                {
                    isResendEnabled = false;
                    resendTextColor = Color.Parse("Black");

                    ResendOtpWithDelay();
                    // Perform navigation upon successful signup
                    await Shell.Current.GoToAsync($"{nameof(EnterOtpPage)}?phoneNumber={phone}");

                    return true;
                }
                else
                {
                    await Shell.Current.DisplayAlert("Failed", "Invalid Number", "OK");
                }
                return result;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        [RelayCommand]
        public async Task<bool> ResendOtp()
        {
            try
            {
                // Call the service to check if the phone number is valid and get the token
                bool result = await _postLoginService.ResendOtp(phone);
                if (result)
                {
                    // Perform navigation upon successful signup

                    IsResendEnabled = false;
                    ResendTextColor = Color.Parse("Black");

                    // Start the timer to enable the button after 1 minute
                    await ResendOtpWithDelay();

                    return true;
                }
                return result;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        [RelayCommand]
        public async Task<string> Verify()
        {
            try
            {
                // Call the service to check if the phone number is valid and get the token
                string jwtToken = await _postLoginService.Verify(phone, OTP);
                if (!string.IsNullOrEmpty(jwtToken))
                {
                    // Store the JWT token in Secure Storage
                    // Reset the phone and OTP fields
                    

                    // Call another service method to get user status after login
                    var userStatus = await _postLoginService.GetUserStatus(phone);

                    // Process the user status to navigate accordingly
                    if (userStatus.Active == true)
                    {
                        await Shell.Current.GoToAsync("//HomePage");
                        return "User is active, navigated to Home.";
                    }
                    else if (userStatus.Rejected == true)
                    {
                        await Shell.Current.GoToAsync(nameof(RejectedPage));
                        return "User is rejected, navigated to Rejected Page.";
                    }
                    else if (userStatus.Active == false && userStatus.Rejected == false)
                    {
                        await Shell.Current.GoToAsync(nameof(ProcessPage));
                        return "User status pending, navigated to Pending Page.";
                    }
                    else
                    {
                        return "Internal server error";
                    }
                }
                else
                {
                    await Shell.Current.DisplayAlert("Failed", "Login Failed!", "OK");
                    return "Login Failed!";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                return "Failed";
            }
        }
            [RelayCommand]
        private async Task LogoutAsync()
        {
            try
            {
                IsLoggingOut = true;
                bool loggedOut = await _postLoginService.Logout();

                if (loggedOut)
                {
                    IsLoggingOut = false;
                    SecureStorage.Default.Remove("JWTToken");



                    await Shell.Current.GoToAsync("/LoginPage");

                }
                else
                {
                    // Show an error message or handle accordingly if logout fails
                    // For example:
                    await Shell.Current.DisplayAlert("Logout Failed", "Failed to logout. Please try again.", "OK");
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions (logging, error messages, etc.)
            }
        }
    }
}
