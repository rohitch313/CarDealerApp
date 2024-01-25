using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MyApp.IService;
using MyApp.Models;
using MyApp.Service;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;

namespace MyApp.ViewModel
{
    public partial class CustomerSupportViewModel : ObservableObject
    {

        private readonly ICustomerSupoortService _customerSupoortService;
        private CustomerSupportDTO _selectedCustomer;

        public CustomerSupportDTO SelectedCustomer
        {
            get => _selectedCustomer;
            set
            {
                _selectedCustomer = value;
                OnPropertyChanged(nameof(SelectedCustomer));
            }
        }

        public CustomerSupportViewModel(ICustomerSupoortService customerSupoortService)
        {
            _customerSupoortService = customerSupoortService;
        }


        private ObservableCollection<CustomerSupportDTO> _dueCustomer;

        public ObservableCollection<CustomerSupportDTO> DueCustomer
        {
            get => _dueCustomer;
            set
            {
                _dueCustomer = value;
                OnPropertyChanged(nameof(DueCustomer));
            }
        }


        // Modify the LoadPaymentStatus method to set the SelectedCustomer
        public async Task LoadPaymentStatus()
        {
            try
            {
                var paymentStatus = await _customerSupoortService.CustomerServices();

                if (paymentStatus is IEnumerable<CustomerSupportDTO> enumerablePaymentStatus)
                {
                    DueCustomer = new ObservableCollection<CustomerSupportDTO>(enumerablePaymentStatus);
                    SelectedCustomer = DueCustomer.FirstOrDefault(); // Set the first item as selected (modify as needed)
                }
                else if (paymentStatus is CustomerSupportDTO singlePaymentStatus)
                {
                    DueCustomer = new ObservableCollection<CustomerSupportDTO> { singlePaymentStatus };
                    SelectedCustomer = singlePaymentStatus; // Set the single item as selected
                }
                else
                {
                    // Handle unexpected type
                    Console.WriteLine($"Unexpected type: {paymentStatus?.GetType().FullName}");
                }
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

    }
}



