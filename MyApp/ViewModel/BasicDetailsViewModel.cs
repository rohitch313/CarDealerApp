// BasicDetailsViewModel.cs
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MyApp.IService;
using MyApp.Models;
using MyApp.View.Login;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace MyApp.ViewModel
{
    public partial class BasicDetailsViewModel : ObservableObject
    {
        private readonly IBasicDetailsService _basicDetailsService;

        public BasicDetailsViewModel(IBasicDetailsService basicDetailsService)
        {
            _basicDetailsService = basicDetailsService ?? throw new ArgumentNullException(nameof(basicDetailsService));
            LoadStates();
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

        private async Task LoadStates()
        {
            try
            {
                var states = await _basicDetailsService.GetState();
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
        [RelayCommand]
        public async Task<bool> PostUserDetailsAsync()
        {
            try
            {
                if (SelectedState == null)
                {
                    // Handle the case where no state is selected
                    // For example, show an alert or message to the user
                    return false;
                }

                // Create or update the UserDetails object with the selected state ID
                UserDetails.SId = SelectedState.StateId;

                bool apiSuccess = await _basicDetailsService.PostUserDetails(UserDetails);

                if (apiSuccess)
                {
                    // Navigate to the home page on successful API response
                    await Shell.Current.GoToAsync(nameof(ProcessPage)); // Adjust the navigation URI as needed
                }
                else
                {
                    // Handle the case where the API response is not successful
                    // await DisplayAlert("API Error", "Failed to post user details.", "OK");
                }

                return apiSuccess;
            }
            catch (Exception ex)
            {
                // Log the error using a logging framework or Debug.WriteLine
                Debug.WriteLine($"Error posting user details: {ex.Message}");
                // Rethrow the exception for consistency
                throw;
            }
        }

    }
}

