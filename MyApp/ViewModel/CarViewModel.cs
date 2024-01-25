using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyApp.IService;
using System.Threading.Tasks;

namespace MyApp.ViewModel
{
    using CommunityToolkit.Maui.Views;
    using CommunityToolkit.Mvvm.ComponentModel;
    using CommunityToolkit.Mvvm.Input;
    using MyApp.Model;
    using MyApp.Services;
    using System.Collections.ObjectModel;
    using System.ComponentModel;

    public partial class CarViewModel : ObservableObject
    {
        private readonly IStockAuditService _stockAuditService;


        public CarViewModel(IStockAuditService stockAuditService)
        {
            _stockAuditService = stockAuditService;
            LoadAddress();


        }
        private ObservableCollection<UpcomingAuditModel> _pendingAudit;
        public ObservableCollection<UpcomingAuditModel> PendingAudit
        {
            get => _pendingAudit;
            set
            {
                _pendingAudit = value;
                OnPropertyChanged(nameof(PendingAudit));
            }
        }

        private ObservableCollection<UpcomingAuditModel> _statusAudit;
        public ObservableCollection<UpcomingAuditModel> StatusAudit
        {
            get => _statusAudit;
            set
            {
               _statusAudit = value;
                OnPropertyChanged(nameof(StatusAudit));
            }
        }
        public async Task LoadPendingAudit()
        {
            try
            {
                var PendingDetail = await _stockAuditService.GetPendingAudit();
                PendingAudit = new ObservableCollection<UpcomingAuditModel>(PendingDetail);
                // Handle the payment status as needed (e.g., update UI, process data)
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
        public async Task LoadStatusAudit()
        {
            try
            {
                var StatusStock = await _stockAuditService.GetAuditStatus();
                StatusAudit = new ObservableCollection<UpcomingAuditModel>(StatusStock);
                // Handle the payment status as needed (e.g., update UI, process data)
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private ObservableCollection<AddressDto> _address;
        public ObservableCollection<AddressDto> Address
        {
            get { return _address; }
            set
            {
                _address = value;
                OnPropertyChanged(nameof(Address));
            }
        }

        private AddressDto _selectedAddress;
        public AddressDto SelectedAddress
        {
            get { return _selectedAddress; }
            set
            {
                _selectedAddress = value;
                OnPropertyChanged(nameof(SelectedAddress));

                // Update the visibility of frames based on the selected filter

            }
        }


        public async Task LoadAddress()
        {
            try
            {
                var StockAddress = await _stockAuditService.GetAddress();
                Address = new ObservableCollection<AddressDto>(StockAddress);
            }
            catch (Exception ex)
            {
                // Handle exceptions
                throw new Exception($"Exception: {ex.Message}");
            }
        }



        [RelayCommand]

        public async Task VerifyButton()
        {
            try
            {
                if (SelectedAddress != null)
                {
                    if (SelectedAddress.AddressType == "Sold")
                    {
                        // Handle displaying the sold address on the same page
                        await Shell.Current.DisplayAlert("Stock", "Your Stock is sold", "Ok");
                    }
                    else if (SelectedAddress.AddressType == "Register Address")
                    {
                        // Navigate to the show image page for registered addresses
                      //  await Shell.Current.GoToAsync($"ShowImagePage?addressId={SelectedAddress.Id}");
                      await Shell.Current.GoToAsync(nameof(UpdatePictureView));
                    }
                    else if (SelectedAddress.AddressType == "Unregister Address")
                    {
                        // Navigate to the basic image page for unregistered addresses
                       // await Shell.Current.GoToAsync($"BasicImagePage?addressId={SelectedAddress.Id}");
                       await Shell.Current.GoToAsync(nameof(UnregisteredView));
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions
                Console.WriteLine($"Exception: {ex.Message}");
            }
        }

        //[RelayCommand]
        //private async Task Payy(AddressDto selectstock)
        //{
        //    try
        //    {
        //        var paymentDetails = await _stockAuditService.UploadImage(selectstock.Id);

        //        // Navigate to the details page and pass the paymentDetails
               
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"Error: {ex.Message}");
        //    }
        //}









        //private ObservableCollection<Car> _cars;
        //public ObservableCollection<Car> Cars
        //{
        //    get => _cars;
        //    set => SetProperty(ref _cars, value);
        //}
        //private string _auditStatus;
        //public string AuditStatus
        //{
        //    get => _auditStatus;
        //    set => SetProperty(ref _auditStatus, value);
        //}


        //public ObservableCollection<Car> FewCars { get; }
        //public ObservableCollection<Car> FewCarsAudit { get; }
        //public ObservableCollection<Car> PendingAuditCars
        //{
        //    get
        //    {
        //        return new ObservableCollection<Car>(Cars.Where(car => car.PendingAuditDate.HasValue));
        //    }
        //}
        //[ObservableProperty]
        //public string name;

        //[ObservableProperty]
        //public string variant;
        //[ObservableProperty]
        //public string pendingAuditDate;

        //[ObservableProperty]
        //public string amountdue;

        //private List<string> status;
        //public List<string> Status
        //{
        //    get { return status; }
        //    set
        //    {
        //        if (status != value)
        //        {
        //            status = value;
        //            OnPropertyChanged(nameof(Status));
        //        }
        //    }
        //}

        //[ObservableProperty]
        //public int purchaseId;

        //[ObservableProperty]
        //public PaymentHistoryViewModel paymentHistory;


        //[ObservableProperty]

        //public int? remainingDays;





        //// Constructor to initialize the ObservableCollection
        //public CarViewModel()
        //{

        //}
        [RelayCommand]
        public async Task Back()
        {
            await Shell.Current.GoToAsync("//HomePage");
        }

       


        [RelayCommand]
        public async Task PaymentProf()
        {
            await Shell.Current.GoToAsync(nameof(DocPaymentProofPage));
        }

    }
}
