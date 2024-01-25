using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MyApp.IService;
using MyApp.Model;
using MyApp.Services;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace MyApp.ViewModel
{
    public partial class ProcurementViewModel : ObservableObject
    {
        private readonly IProcurementService _procurementService;

        private ObservableCollection<ProcurementFilterModel> _filters;
        public ObservableCollection<ProcurementFilterModel> Filters
        {
            get { return _filters; }
            set
            {
                _filters = value;
                OnPropertyChanged(nameof(Filters));
            }
        }

        private ObservableCollection<ProcurementDetialModel> _procurements;
        public ObservableCollection<ProcurementDetialModel> Procurements
        {
            get { return _procurements; }
            set
            {
                _procurements = value;
                OnPropertyChanged(nameof(Procurements));
            }
        }

        private ObservableCollection<ProcurmentInProcessModel> _procurementsinProcess;
        public ObservableCollection<ProcurmentInProcessModel> ProcurementsinProcess
        {
            get { return _procurementsinProcess; }
            set
            {
                _procurementsinProcess = value;
                OnPropertyChanged(nameof(ProcurementsinProcess));
            }
        }

        private ObservableCollection<ProcurementClosedModel> _procurementsClosed;
        public ObservableCollection<ProcurementClosedModel> ProcurementsClosed
        {
            get { return _procurementsClosed; }
            set
            {
                _procurementsClosed = value;
                OnPropertyChanged(nameof(ProcurementsClosed));
            }
        }
        private bool _isAggregatorVisible;
        public bool IsAggregatorVisible
        {
            get { return _isAggregatorVisible; }
            set
            {
                _isAggregatorVisible = value;
                OnPropertyChanged(nameof(IsAggregatorVisible));
            }
        }

        private bool _isNewCarDealerVisible;
        public bool IsNewCarDealerVisible
        {
            get { return _isNewCarDealerVisible; }
            set
            {
                _isNewCarDealerVisible = value;
                OnPropertyChanged(nameof(IsNewCarDealerVisible));
            }
        }

        private bool _isOpenMarketVisible;
        public bool IsOpenMarketVisible
        {
            get { return _isOpenMarketVisible; }
            set
            {
                _isOpenMarketVisible = value;
                OnPropertyChanged(nameof(IsOpenMarketVisible));
            }
        }

        private ProcurementFilterModel _selectedFilter;
        public ProcurementFilterModel SelectedFilter
        {
            get { return _selectedFilter; }
            set
            {
                _selectedFilter = value;
                OnPropertyChanged(nameof(SelectedFilter));

                // Update the visibility of frames based on the selected filter
                if (_selectedFilter != null)
                {
                    IsAggregatorVisible = _selectedFilter.Name == "Aggregator";
                    IsNewCarDealerVisible = _selectedFilter.Name == "New Car Dealer";
                    IsOpenMarketVisible = _selectedFilter.Name == "Open Market";
                }

                LoadProcurements(); // Trigger data loading when the filter changes
            }
        }
        [RelayCommand]
        private void HideAggregatorFrame()
        {
            IsAggregatorVisible = false;
            SelectedFilter = null; // Reset the selected filter
        }
        [RelayCommand]
        private void HideNewCarDealerFrame()
        {
            IsNewCarDealerVisible = false;
            SelectedFilter = null; // Reset the selected filter
        }

        [RelayCommand]
        private void HideOpenMarketFrame()
        {
            IsOpenMarketVisible = false;
            SelectedFilter = null; // Reset the selected filter
        }

        public  ProcurementViewModel(IProcurementService procurementService)
        {
            _procurementService = procurementService;
            LoadFilters();
            LoadProcurements(); // Load procurements initially with no filter
            LoadClosedProcurements();
            LoadInProcessProcurements();
        }

        public async Task LoadFilters()
        {
            try
            {
                var procurementFilters = await _procurementService.GetFilters();
                Filters = new ObservableCollection<ProcurementFilterModel>(procurementFilters);
            }
            catch (Exception ex)
            {
                // Handle exceptions
                throw new Exception($"Exception: {ex.Message}");
            }
        }

        public async Task LoadProcurements()
        {
            try
            {
                if (SelectedFilter == null)
                {
                    // If no filter selected, load all procurements
                    var allProcurements = await _procurementService.GetAllProcurements();
                    Procurements = new ObservableCollection<ProcurementDetialModel>(allProcurements);
                    return;
                }

                // Load procurements based on the selected filter
                var filteredProcurements = await _procurementService.GetFilterProcurement(SelectedFilter.Id);
                Procurements = new ObservableCollection<ProcurementDetialModel>(filteredProcurements);
            }
            catch (Exception ex)
            {
                // Handle exceptions
                throw new Exception($"Exception: {ex.Message}");
            }
        }

        public async Task LoadClosedProcurements()
        {
            try
            {
                var procurementFilters = await _procurementService.GetClosedProcurements();
                ProcurementsClosed = new ObservableCollection<ProcurementClosedModel>(procurementFilters);
            }
            catch (Exception ex)
            {
                // Handle exceptions
                throw new Exception($"Exception: {ex.Message}");
            }
        }

        public async Task LoadInProcessProcurements()
        {
            try
            {
                var procurementFilters = await _procurementService.GetInprocessProcurements();
                ProcurementsinProcess = new ObservableCollection<ProcurmentInProcessModel>(procurementFilters);
            }
            catch (Exception ex)
            {
                // Handle exceptions
                throw new Exception($"Exception: {ex.Message}");
            }
        }
    }
}
