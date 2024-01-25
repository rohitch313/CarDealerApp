using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MyApp.Model;
using MyApp.Services;
using MyApp.View.Payment;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MyApp.IService;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using MyApp.View.Account;
using MyApp.Models;

namespace MyApp.ViewModel
{
    public partial class PaymentViewModel:ObservableObject
    {
        private readonly IStockAuditService _stockAuditService;
        private readonly IPaymnetService _paymentService;

        public PaymentViewModel(IPaymnetService paymentService, IStockAuditService stockAuditService)
        {
           
            _paymentService = paymentService;
            _stockAuditService = stockAuditService;

        }

        private PaymentProofImgDTO proofImage = new PaymentProofImgDTO();
        public PaymentProofImgDTO ProofImage
        {
            get => proofImage;
            set
            {
                proofImage = value;
                OnPropertyChanged(nameof(ProofImage));
            }
        }
        private bool _isFrameVisible;

        public bool IsFrameVisible
        {
            get { return _isFrameVisible; }
            set
            {
                if (_isFrameVisible != value)
                {
                    _isFrameVisible = value;
                    OnPropertyChanged(nameof(IsFrameVisible));
                }
            }
        }
        private PaymentDetailDto _selectPayment;
        private int _selectedPaymentId;

        public PaymentDetailDto SelectPayment
        {
            get => _selectPayment;
            set
            {
                _selectPayment = value;
                OnPropertyChanged(nameof(SelectPayment));

                // Update the SelectedPaymentId whenever SelectedPayment changes
                SelectedPaymentId = value?.Id ?? 0; // Assuming Id is an int property
            }
        }

        public int SelectedPaymentId
        {
            get => _selectedPaymentId;
            set
            {
                _selectedPaymentId = value;
                OnPropertyChanged(nameof(SelectedPaymentId));
            }
        }

        [RelayCommand]
        private async Task PaymentProof()
        {
            try
            {
                if (SelectPayment != null)
                {
                    // Navigate to the page where you'll upload the image
                    await Shell.Current.GoToAsync(nameof(DocPaymentProofPage));
                }
                else
                {
                    await Shell.Current.DisplayAlert("Error", "Please select Payment", "Ok");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
        private string _userName;

        public string UserName
        {
            get => _userName;
            set
            {
                if (_userName != value)
                {
                    _userName = value;
                    OnPropertyChanged(nameof(UserName));
                }
            }
        }
        public async Task RetrieveAndSetUsername()
        {
            try
            {
                // Retrieve the token from secure storage
                var token = await SecureStorage.GetAsync("JWTToken");

                if (!string.IsNullOrEmpty(token))
                {
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var jwtToken = tokenHandler.ReadJwtToken(token);

                    // Retrieve the claim containing the username
                    var usernameClaim = jwtToken.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Name);

                    if (usernameClaim != null)
                    {
                        UserName = usernameClaim.Value;
                    }
                    else
                    {

                        UserName = "Username not found";
                    }
                }
                else
                {

                    UserName = "Token not found";
                }
            }
            catch (Exception ex)
            {
                // Handle token parsing exception
                UserName = "Error retrieving username";
            }
        }
        [RelayCommand]
        private async Task Pay(PaymentDetailDto selectedPayment)
        {
            try
            {
                var paymentDetails = await _paymentService.GetPaymentDetails(selectedPayment.Id);
                SelectedPayment = paymentDetails;
                // Navigate to the details page and pass the paymentDetails
                await Shell.Current.GoToAsync($"{nameof(PayAmount)}?paymentDetails={Uri.EscapeDataString(JsonConvert.SerializeObject(paymentDetails))}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
        private PaymentDetailDto _selectedPayment;

        public PaymentDetailDto SelectedPayment
        {
            get { return _selectedPayment; }
            set
            {
                _selectedPayment = value;
                OnPropertyChanged(nameof(SelectedPayment));
            }
        }
        private ObservableCollection<PaymentDetailDto> _duePayments;
        private ObservableCollection<PaymentDetailDto> _paystatus;

        public ObservableCollection<PaymentDetailDto> DuePayments
        {
            get => _duePayments;
            set
            {
                _duePayments = value;
                OnPropertyChanged(nameof(DuePayments));
            }
        }
        public ObservableCollection<PaymentDetailDto> Paystatus
        {
            get => _paystatus;
            set
            {
                _paystatus = value;
                OnPropertyChanged(nameof(Paystatus));
            }
        }
        public async Task LoadPaymentStatus()
        {
            try
            {
                var paymentStatus = await _paymentService.GetPaymentStatus();
                Paystatus = new ObservableCollection<PaymentDetailDto>(paymentStatus);
                // Handle the payment status as needed (e.g., update UI, process data)
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
        public async  Task LoadDuePayments()
        {
            try
            {
                var paymentDtos = await _paymentService.GetDuePayments();
                DuePayments = new ObservableCollection<PaymentDetailDto>(paymentDtos);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    

        [RelayCommand]
        public static async Task Back()
        {
            await Shell.Current.GoToAsync("//HomePage");
        }




        //home

        private ObservableCollection<PaymentDetailDto> _upcomingPayment;
        public ObservableCollection<PaymentDetailDto> UpcomingPayment
        {
            get => _upcomingPayment;
            set
            {
                _upcomingPayment = value;
                OnPropertyChanged(nameof(UpcomingPayment));
            }
        }
        private ObservableCollection<UpcomingAuditModel> _upcomingAudit;
        public ObservableCollection<UpcomingAuditModel> UpcomingAudit
        {
            get => _upcomingAudit;
            set
            {
                _upcomingAudit = value;
                OnPropertyChanged(nameof(UpcomingAudit));
            }
        }
        public async Task LoadUpcomingAudit()
        {
            try
            {
                var UpcomingDetail = await _stockAuditService.GetUpcomingAudit();
                UpcomingAudit = new ObservableCollection<UpcomingAuditModel>(UpcomingDetail);
                // Handle the payment status as needed (e.g., update UI, process data)
                IsFrameVisible = UpcomingAudit.Count > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
        public async Task LoadUpcomingPayment()
        {
            try
            {
                var payDetail = await _paymentService.GetUpcomingPayment();
                UpcomingPayment = new ObservableCollection<PaymentDetailDto>(payDetail);
                // Handle the payment status as needed (e.g., update UI, process data)
                IsFrameVisible = UpcomingPayment.Count > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
        [RelayCommand]
        public async Task PaymentPage()
        {
            await Shell.Current.GoToAsync(nameof(PaymentView));
        }


        [RelayCommand]

        public async Task StockAudit()
        {
            await Shell.Current.GoToAsync(nameof(StockAuditView));
        }

        [RelayCommand]
        private async Task Payy(PaymentDetailDto selectedPayment)
        {
            try
            {
                var paymentDetails = await _paymentService.GetPaymentDetails(selectedPayment.Id);
                SelectedPayment = paymentDetails;
                // Navigate to the details page and pass the paymentDetails
                await Shell.Current.GoToAsync($"{nameof(UpcomingPaymentPage)}?paymentDetails={Uri.EscapeDataString(JsonConvert.SerializeObject(paymentDetails))}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
        [RelayCommand]
        public async Task ProcurementDetail()
        {
            await Shell.Current.GoToAsync(nameof(ProcurementDetailView));
        }


        [RelayCommand]
        private async Task UploadImageForPayment()
        {
            try
            {
                if (SelectedPaymentId != 0)
                {
                    int selectedPaymentId = SelectedPaymentId;

                    if (selectedPaymentId != 0) // Replace this condition with the appropriate check
                    {

                        string result = await _paymentService.PaymentProof(selectedPaymentId, ProofImage);

                        if (result == "Sucess")
                        {
                            await Shell.Current.GoToAsync("///HomePage");
                        }
                        else
                        {
                            Console.WriteLine("Fail");
                        }
                    }
                    else
                    {
                        Console.WriteLine("No payment selected to upload an image.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }




        private ImageSource _capturedImageSource;
        public ImageSource CapturedImageSource
        {
            get => _capturedImageSource;
            set
            {
                _capturedImageSource = value;
                OnPropertyChanged(nameof(CapturedImageSource));
            }
        }








        [RelayCommand]
        private async Task NavigateToNotificationPage()
        {
            await Shell.Current.GoToAsync(nameof(NotificationPage));
        }





















        [RelayCommand]
        public async Task Back11()
        {
            await Shell.Current.GoToAsync("/PaymentView");
        }
    }
}
    
