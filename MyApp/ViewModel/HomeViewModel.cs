using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using MyApp.Model;
using MyApp.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using MyApp.IService;
using System.Threading.Tasks;

namespace MyApp.ViewModel
{
    public partial class HomeViewModel:ObservableObject
    {
        private readonly IStockAuditService _stockAuditService;
        private readonly IPaymnetService _paymentService;

        public HomeViewModel(IStockAuditService stockAuditService, IPaymnetService paymentService)
        {
            _stockAuditService = stockAuditService;
            _paymentService = paymentService;
        }

        private ObservableCollection<PaymentDto> _upcomingPayment;
        public ObservableCollection<PaymentDto> UpcomingPayment
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
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
        public async Task CheckUpcomingPayment()
        {
            await LoadUpcomingPayment();

            if (UpcomingPayment != null && UpcomingPayment.Count == 0)
            {
                // The UpcomingPayment list has zero records.
                Console.WriteLine("UpcomingPayment has zero records.");
            }
            else
            {
                // The UpcomingPayment list has records.
                Console.WriteLine($"UpcomingPayment has {UpcomingPayment.Count} records.");
            }
        }

        public async Task LoadUpcomingPayment()
        {
            try
            {
                var payDetail = await _paymentService.GetUpcomingPayment();
                UpcomingPayment = new ObservableCollection<PaymentDto>(payDetail);
                // Handle the payment status as needed (e.g., update UI, process data)
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
        private async Task Pay(PaymentDetailDto selectedPayment)
        {
            try
            {
                var paymentDetails = await _paymentService.GetPaymentDetails(selectedPayment.Id);

                // Navigate to the details page and pass the paymentDetails
                await Shell.Current.GoToAsync($"{nameof(PayAmount)}?paymentDetails={Uri.EscapeDataString(JsonConvert.SerializeObject(paymentDetails))}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        public async Task ProcurementDetail()
        {
            await Shell.Current.GoToAsync(nameof(ProcurementDetailView));
        }
    }
}
