using CommunityToolkit.Mvvm.ComponentModel;
using MyApp.IService;
using MyApp.Model;
using MyApp.Models;
using MyApp.Service;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;


namespace MyApp.ViewModel

{
    public class ProfileViewmodel : ObservableObject
    {
        private readonly IProfileInformationService _accountService;
        private ObservableCollection<ProfileInformationDTO> _duePayments;
        private ProfileInformationDTO _selectedCustomer;

        public ProfileInformationDTO SelectedCustomer
        {
            get => _selectedCustomer;
            set => SetProperty(ref _selectedCustomer, value);
        }

        public ObservableCollection<ProfileInformationDTO> DuePayments
        {
            get => _duePayments ??= new ObservableCollection<ProfileInformationDTO>();
            set => SetProperty(ref _duePayments, value);
        }

        public ProfileViewmodel(IProfileInformationService accountService)
        {
            _accountService = accountService ?? throw new ArgumentNullException(nameof(accountService));
        }

        public async Task LoadPaymentStatus()
        {
            try
            {
                var paymentStatus = await _accountService.GetProfileInformationAsync();

                if (paymentStatus is IEnumerable<ProfileInformationDTO> enumerablePaymentStatus)
                {
                    DuePayments = new ObservableCollection<ProfileInformationDTO>(enumerablePaymentStatus);
                    SelectedCustomer = DuePayments.FirstOrDefault(); // Set the first item as selected (modify as needed)
                }
                else if (paymentStatus is ProfileInformationDTO singlePaymentStatus)
                {
                    DuePayments = new ObservableCollection<ProfileInformationDTO> { singlePaymentStatus };
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
                // Log or display the error to the user
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}
