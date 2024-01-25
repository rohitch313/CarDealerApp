using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MyApp.IService;
using MyApp.Models;
using MyApp.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace MyApp.ViewModel
{
    public class AccountInfoViewmodel : ObservableObject
    {
        private readonly IAccountInfoService _accountService;
        private ObservableCollection<AccountInfoDTO> _duePayments;
        private AccountInfoDTO _selectedCustomer;

        public AccountInfoDTO SelectedCustomer
        {
            get => _selectedCustomer;
            set => SetProperty(ref _selectedCustomer, value);
        }

        public ObservableCollection<AccountInfoDTO> DuePayments
        {
            get => _duePayments ??= new ObservableCollection<AccountInfoDTO>();
            set => SetProperty(ref _duePayments, value);
        }

        public AccountInfoViewmodel(IAccountInfoService accountService)
        {
            _accountService = accountService ?? throw new ArgumentNullException(nameof(accountService));
        }

        public async Task LoadPaymentStatus()
        {
            try
            {
                var paymentStatus = await _accountService.GetAccountInfoDetails();

                if (paymentStatus is IEnumerable<AccountInfoDTO> enumerablePaymentStatus)
                {
                    DuePayments = new ObservableCollection<AccountInfoDTO>(enumerablePaymentStatus);
                    SelectedCustomer = DuePayments.FirstOrDefault(); // Set the first item as selected (modify as needed)
                }
                else if (paymentStatus is AccountInfoDTO singlePaymentStatus)
                {
                    DuePayments = new ObservableCollection<AccountInfoDTO> { singlePaymentStatus };
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
