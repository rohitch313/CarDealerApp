using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using MyApp.Models;
using MyApp.IService;
using MyApp.Service;
using CommunityToolkit.Mvvm.Input;
using MyApp.View.Login;
using Newtonsoft.Json.Linq;

namespace MyApp.ViewModel
{
    public partial class SignUpViewModel : ObservableObject
    {


        private readonly IPostLogInService _postLoginService;



        public SignUpViewModel(IPostLogInService postLoginService)
        {
            _postLoginService = postLoginService;
            OTPDigit1 = null;
            OTPDigit2 = null;
            OTPDigit3 = null;
            OTPDigit4 = null;
            isResendEnabled = false;
            resendTextColor = Color.Parse("Black");
            ResendOtpWithDelay();




        }
        private ObservableCollection<DropDownStateDTO> _states;
        private DropDownStateDTO _selectedState;


        public ObservableCollection<DropDownStateDTO> States
        {
            get => _states;
            set
            {
                _states = value;
                OnPropertyChanged(nameof(States));
            }
        }

        public DropDownStateDTO SelectedState
        {
            get => _selectedState;
            set
            {
                _selectedState = value;
                OnPropertyChanged(nameof(SelectedState));
            }
        }
        private BasicDetailsDTO _userDetails = new BasicDetailsDTO(); // Initialize with default values if needed

        public BasicDetailsDTO UserDetails
        {
            get => _userDetails;
            set
            {
                _userDetails = value;
                OnPropertyChanged(nameof(UserDetails));
            }
        }


        public async Task LoadStates()
        {
            try
            {
                var states = await _postLoginService.GetState();
                States = new ObservableCollection<DropDownStateDTO>(states);

                // Optionally, set a default selected state if needed
                // SelectedState = States.FirstOrDefault();
            }
            catch (Exception ex)
            {
                // Handle exceptions
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }


        // userphone
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
        [RelayCommand]
        public async Task<bool> PostSignup()
        {
            try
            {
                if (phone == null || phone.Length != 10 || !IsPhoneNumberValid(phone))
                {
                    // Handle the case where the phone number is null, not 10 digits, or contains non-digit characters
                    // Display an alert with an appropriate error message
                    await Shell.Current.DisplayAlert("Failed", "Please enter a valid 10-digit mobile number", "OK");
                    return false;
                }

                // Call the service to check if the phone number is valid and get the token
                bool result = await _postLoginService.Signup(phone);
                if (result)
                {
                    // Perform navigation upon successful signup
                    await Shell.Current.GoToAsync($"{nameof(EnterOtpPageSign)}?phoneNumber={phone}");

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
                // Handle exceptions appropriately, e.g., log the exception
                return false;
            }
        }

        private bool IsPhoneNumberValid(string phoneNumber)
        {
            // Use a regular expression to check if the phone number contains only digits
            // Adjust the regex pattern to disallow characters other than digits
            return System.Text.RegularExpressions.Regex.IsMatch(phoneNumber, @"^[0-9]+$");
        }


        [RelayCommand]
        public async Task<bool> ResendOtpSignup()
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
        public async Task<string> VerifySignup()
        {
            try
            {
                // Call the service to check if the phone number is valid and get the token
                string jwtToken = await _postLoginService.VerifySignup(phone, OTP);
                if (!string.IsNullOrEmpty(jwtToken))
                {
                    // Store the JWT token in Secure Storage





                    //await Shell.Current.DisplayAlert("Success", "Login successful!", "OK");
                    await Shell.Current.GoToAsync($"{nameof(BasicDetailView)}?phoneNumber={phone}");

                    // Perform navigation upon successful signup

                    return "Login succesfully";
                }
                else
                {
                    await Shell.Current.DisplayAlert("Failed", "Login Failed!", "OK");

                }
                return jwtToken;
            }
            catch (Exception ex)
            {
                return "Failed";

            }
        }



        [RelayCommand]
        public async Task<string> SignupComplete()
        {

            try
            {
                if (SelectedState == null || string.IsNullOrEmpty(phone))
                {
                    // Handle the case where no state is selected, or phone is null or empty
                    // For example, show an alert or message to the user
                    await Shell.Current.DisplayAlert("Failed", "Please select a state .", "OK");
                    return "Failed";
                }

                if (string.IsNullOrEmpty(UserDetails.UserName))
                {
                    // Handle the case where UserName is null or empty
                    // For example, show an alert or message to the user
                    await Shell.Current.DisplayAlert("Failed", "UserName is required!", "OK");
                    return "Failed";
                }

                if (!string.IsNullOrEmpty(UserDetails.UserEmail) && !UserDetails.UserEmail.Contains("@gmail.com"))
                {
                    // Handle the case where the email is not a Gmail address
                    // For example, show an alert or message to the user
                    await Shell.Current.DisplayAlert("Failed", "Enter a valid Gmail address", "OK");
                    return "Failed";
                }


                // Create or update the UserDetails object with the selected state ID
                UserDetails.SId = SelectedState.StateId;

                // Call the service to check if the phone number is valid and get the token
                var result = await _postLoginService.SignUpComplete(phone, UserDetails); ;

                // Check the result and navigate based on user status
                if (!string.IsNullOrEmpty(result))
                {
                    // Call another service method to get user status after signup
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
                    else if(userStatus.Active == false && userStatus.Rejected == false) 
                    {
                        await Shell.Current.GoToAsync(nameof(ProcessPage));
                        return "User status pending, navigated to Pending Page.";
                    }else
                    {
                        return "Internal server error";
                    }
                }
                else
                {
                    // Handle the case where signup failed
                    await Shell.Current.DisplayAlert("Failed", "Signup Failed!", "OK");
                    return "Signup Failed!";
                }
            }
            catch (Exception ex)
            {
                // Handle other exceptions that might occur
                Console.WriteLine($"Exception: {ex.Message}");
                return "Failed";
            }
        }
    }
}