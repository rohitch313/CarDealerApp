using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using MyApp.IService;
using MyApp.Model;
using MyApp.Models;
using MyApp.View.Home;
using MyApp.View.Login;
using MyApp.View.PurchaseVehicle;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

namespace MyApp.ViewModel
{
    public partial class FullAggragatorViewModel : ObservableObject
    {
        private readonly IFullAggragatorService _aggService;

        public FullAggragatorViewModel() : this(null)
        {
            // Default constructor can call the parameterized constructor with null or default values
            try
            {
                // Logging to help diagnose the issue
                System.Diagnostics.Debug.WriteLine("Default constructor called");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Exception in default constructor: {ex.Message}");
            }
        }

        // Parameterized constructor
        public FullAggragatorViewModel(IFullAggragatorService aggService)
        {
            try
            {
                // Logging to help diagnose the issue
                System.Diagnostics.Debug.WriteLine($"Parameterized constructor called with {aggService}");

                _aggService = aggService ?? throw new ArgumentNullException(nameof(aggService));

                StateListOpenMarket = new ObservableCollection<PV_OpenMarketDTO>();
                StateListNewCardDealer = new ObservableCollection<PV_NewCarDealerDTO>();

                LoadMake();
                LoadModel();
                LoadVariant();
                Loadyear();
                LoadVehicleRecords();
            }
            catch (Exception ex)
            {
                // Logging to help diagnose the issue
                System.Diagnostics.Debug.WriteLine($"Exception in parameterized constructor: {ex.Message}");
            }
        }



        private ObservableCollection<PV_NewCarDealerDTO> _NewCarDealerDTOs;
        private ObservableCollection<PV_OpenMarketDTO> _OpenMarketDTOs;
        public ObservableCollection<PV_OpenMarketDTO> StateListOpenMarket
        {
            get => _OpenMarketDTOs;
            private set => SetProperty(ref _OpenMarketDTOs, value);
        }
        private ObservableCollection<Agg_DropDownMakeDTO> _make;
        private Agg_DropDownMakeDTO _selectedMake;




        public ObservableCollection<Agg_DropDownMakeDTO> Make
        {
            get => _make;
            set
            {
                _make = value;
                OnPropertyChanged(nameof(Make));
            }
        }

        public Agg_DropDownMakeDTO SelectedMake
        {
            get => _selectedMake;
            set
            {
                _selectedMake = value;
                OnPropertyChanged(nameof(SelectedMake));
            }
        }

      

        //Model
        private ObservableCollection<Agg_DropDownModelDTO> _model;
        private Agg_DropDownModelDTO _selectedModel;




        public ObservableCollection<Agg_DropDownModelDTO> Model
        {
            get => _model;
            set
            {
                _model = value;
                OnPropertyChanged(nameof(Model));
            }
        }

        public Agg_DropDownModelDTO SelectedModel
        {
            get => _selectedModel;
            set
            {
                _selectedModel = value;
                OnPropertyChanged(nameof(SelectedModel));
            }
        }







        //Variant
        private ObservableCollection<Agg_DropDownVariantDTO> _variant;
        private Agg_DropDownVariantDTO _selectedVariant;




        public ObservableCollection<Agg_DropDownVariantDTO> Variant
        {
            get => _variant;
            set
            {
                _variant = value;
                OnPropertyChanged(nameof(Variant));
            }
        }

        public Agg_DropDownVariantDTO SelectedVariant
        {
            get => _selectedVariant;
            set
            {
                _selectedVariant = value;
                OnPropertyChanged(nameof(SelectedVariant));
            }
        }






        private ObservableCollection<Agg_DropDownYORegisDTO> _year;
        private Agg_DropDownYORegisDTO _selectedYear;




        public ObservableCollection<Agg_DropDownYORegisDTO> Year
        {
            get => _year;
            set
            {
                _year = value;
                OnPropertyChanged(nameof(Year));
            }
        }

        public Agg_DropDownYORegisDTO SelectedYear
        {
            get => _selectedYear;
            set
            {
                _selectedYear = value;
                OnPropertyChanged(nameof(SelectedYear));
            }
        }

        private async Task LoadMake()
        {
            try
            {
                var states = await _aggService.GetMakeData();
                Make = new ObservableCollection<Agg_DropDownMakeDTO>(states);

                // Optionally, set a default selected state if needed
                // SelectedState = States.FirstOrDefault();
            }
            catch (Exception ex)
            {
                // Handle exceptions
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }


        private async Task LoadModel()
        {
            try
            {
                var states = await _aggService.GetModelData();
                Model = new ObservableCollection<Agg_DropDownModelDTO>(states);

                // Optionally, set a default selected state if needed
                // SelectedState = States.FirstOrDefault();
            }
            catch (Exception ex)
            {
                // Handle exceptions
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }


        private async Task LoadVariant()
        {
            try
            {
                var states = await _aggService.GetVariantData();
                Variant = new ObservableCollection<Agg_DropDownVariantDTO>(states);

                // Optionally, set a default selected state if needed
                // SelectedState = States.FirstOrDefault();
            }
            catch (Exception ex)
            {
                // Handle exceptions
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }



        private async Task Loadyear()
        {
            try
            {
                var states = await _aggService.GetYearOfRegData();
                Year = new ObservableCollection<Agg_DropDownYORegisDTO>(states);

                // Optionally, set a default selected state if needed
                // SelectedState = States.FirstOrDefault();
            }
            catch (Exception ex)
            {
                // Handle exceptions
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }


        private ObservableCollection<VehicleRecordsDto> _vehicleRecords;

        public ObservableCollection<VehicleRecordsDto> VehicleRecords
        {
            get => _vehicleRecords;
            private set => SetProperty(ref _vehicleRecords, value);
        }

     
        private async Task LoadVehicleRecords()
        {
            try
            {
                // Assuming there is a method in _aggService to fetch vehicle records
                var records = await _aggService.GetCarVehicleRecord();
                VehicleRecords = new ObservableCollection<VehicleRecordsDto>(records);
            }
            catch (Exception ex)
            {
                // Handle exceptions
                Console.WriteLine($"An error occurred while loading vehicle records: {ex.Message}");
            }
        }

        //New car dealer
        private ObservableCollection<PV_NewCarDealerDTO> _dueCustomer;

        public ObservableCollection<PV_NewCarDealerDTO> StateListNewCardDealer
        {
            get => _dueCustomer;
            set => SetProperty(ref _dueCustomer, value);
        }



        private PV_AggregatorDTO aggregators = new PV_AggregatorDTO(); // Initialize with default values if needed

        public PV_AggregatorDTO Aggregators
        {
            get => aggregators;
            set
            {
                aggregators = value;
                OnPropertyChanged(nameof(aggregators));
            }
        }
        [RelayCommand]
        public async Task<bool> LoadAggregator()
        {
            try
            {
              
                if (SelectedMake == null && SelectedModel == null && SelectedVariant == null && SelectedYear == null)
                {
                    // Handle the case where no state is selected
                    // For example, show an alert or message to the user
                    return false;
                }
                Aggregators.ModelId = SelectedModel.ModelId;
                // Create or update the UserDetails object with the selected state ID
                Aggregators.MakeId = SelectedMake.MakeId;
                //    Aggregators.ModelId = SelectedModel.ModelId;
                Aggregators.VariantId = SelectedVariant.VariantId;
                Aggregators.YearOfRegistration = SelectedYear.YearId;

                bool apiSuccess = await _aggService.PostAggragatorDetails(Aggregators);

                if (apiSuccess)
                {
                    var popup = new PurchaseVehiclePopup();
                    Shell.Current.CurrentPage.ShowPopup(popup);

                   
                    Aggregators = new PV_AggregatorDTO();  // Resetting the object to default values
                    SelectedMake = null;
                    SelectedModel = null;
                    SelectedVariant = null;
                    SelectedYear = null;

                    // OR, if you prefer to reset individual properties:
                    // Aggregators.MakeId = 0;
                    // Aggregators.ModelId = 0;
                    // Aggregators.VariantId = 0;
                    // Aggregators.YearOfRegistration = 0;

                    OnPropertyChanged(nameof(Aggregators));
                    OnPropertyChanged(nameof(SelectedMake));
                    OnPropertyChanged(nameof(SelectedModel));
                    OnPropertyChanged(nameof(SelectedVariant));
                    OnPropertyChanged(nameof(SelectedYear));

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

        private PV_NewCarDealerDTO newCar = new PV_NewCarDealerDTO(); // Initialize with default values if needed

        public PV_NewCarDealerDTO NewCar
        {
            get => newCar;
            set
            {
                newCar = value;
                OnPropertyChanged(nameof(NewCar));
            }
        }
        [RelayCommand]
        public async Task<bool> LoadNewCarDetail()
        {
            try
            {
                bool apiSuccess = await _aggService.PostNewCarDealerDetails(NewCar);
                if (apiSuccess)
                {
                    var popup = new PurchaseVehiclePopup();
                    Shell.Current.CurrentPage.ShowPopup(popup);

                    // Set properties to empty strings or default values before creating a new instance
                    NewCar.OdometerPicture = "";
                    NewCar.VehiclePicFromFront = "";
                    NewCar.VehicleNumber = "";
                    NewCar.Invoice = "";
                    NewCar.PictOfOrginalRC = "";
                    NewCar.VehiclePicFromBack = "";
                    NewCar.PurchaseAmount = "";

                    // Create a new instance after setting properties
                    NewCar = new PV_NewCarDealerDTO();

                    OnPropertyChanged(nameof(NewCar));

                    // Navigate to the home page on successful API response
                    await Shell.Current.GoToAsync("//HomePage"); // Adjust the navigation URI as needed
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


        private PV_OpenMarketDTO openMarket = new PV_OpenMarketDTO(); // Initialize with default values if needed

        public PV_OpenMarketDTO OpenMarket
        {
            get => openMarket;
            set
            {
                openMarket = value;
                OnPropertyChanged(nameof(OpenMarket));
            }
        }



        [RelayCommand]

        public async Task<bool> LoadOpenMarketDetails()
        {
            try
            {
                bool apiSuccess = await _aggService.PostOpenMarketDetails(OpenMarket);
                if (apiSuccess)
                {
                    var popup = new PurchaseVehiclePopup();
                    Shell.Current.CurrentPage.ShowPopup(popup);

                    // Set properties to empty strings or default values before creating a new instance
                    OpenMarket.PurchaseAmount = "";
                    OpenMarket.TokenAmount = "";
                    OpenMarket.WithholdAmount = "";
                    OpenMarket.SellerContactNumber = "";
                    OpenMarket.SellerEmailAddress = "";

                    // Create a new instance after setting properties
                    OpenMarket = new PV_OpenMarketDTO();

                    OnPropertyChanged(nameof(OpenMarket));

                    // Navigate to the home page on successful API response
                    await Shell.Current.GoToAsync("//HomePage"); // Adjust the navigation URI as needed
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
