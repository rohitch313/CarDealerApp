using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;


namespace MyApp.ViewModel
{
    public class VehicleRecordsViewModel : INotifyPropertyChanged
    {
        private VehicleRecordsModel _vehicleRecords;

        public VehicleRecordsViewModel()
        {
            _vehicleRecords = new VehicleRecordsModel(); // Initialize your model

            // Set properties in the view model (example values)
            Challan = false;
            RSStatus = true;
            Fitness = true;
            OwnerName = true;
            Hypothecation = true;
            BlackList = true;
        }

        // Vehicle information properties (replace with actual data)
        public string CarNameAndVariant => "YourCarNameAndVariant";
        public string PurchaseID => "PurchaseID:77553775";

        // Status properties
        private bool _challan;
        private bool _rsStatus;
        private bool _fitness;
        private bool _ownerName;
        private bool _hypothecation;
        private bool _blackList;

        public bool Challan
        {
            get { return _challan; }
            set { SetProperty(ref _challan, value); }
        }

        public bool RSStatus
        {
            get { return _rsStatus; }
            set { SetProperty(ref _rsStatus, value); }
        }

        public bool Fitness
        {
            get { return _fitness; }
            set { SetProperty(ref _fitness, value); }
        }

        public bool OwnerName
        {
            get { return _ownerName; }
            set { SetProperty(ref _ownerName, value); }
        }

        public bool Hypothecation
        {
            get { return _hypothecation; }
            set { SetProperty(ref _hypothecation, value); }
        }

        public bool BlackList
        {
            get { return _blackList; }
            set { SetProperty(ref _blackList, value); }
        }

        // Status icon properties
        public string ChallanStatusIcon => Challan ? "green.png" : "red.png";
        public string RSStatusIcon => RSStatus ? "green.png" : "red.png";
        public string FitnessStatusIcon => Fitness ? "green.png" : "red.png";
        public string OwnerNameStatusIcon => OwnerName ? "green.png" : "red.png";
        public string HypothecationStatusIcon => Hypothecation ? "green.png" : "red.png";
        public string BlackListStatusIcon => BlackList ? "green.png" : "red.png";

        // Implement the INotifyPropertyChanged interface
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // Set property value and raise PropertyChanged event
        private bool SetProperty<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (!EqualityComparer<T>.Default.Equals(field, value))
            {
                field = value;
                OnPropertyChanged(propertyName);
                OnPropertyChanged($"{propertyName}Icon"); // Notify icon property change
                OnPropertyChanged(nameof(FalseProperties)); // Notify FalseProperties change
                return true;
            }
            return false;
        }

        // Get a list of property names with false values
        public List<string> FalseProperties
        {
            get
            {
                var falseProperties = new List<string>();

               
                

                // Get all properties of the class
                var properties = GetType().GetProperties().Where(prop => prop.PropertyType == typeof(bool));

                // Check each property's value
                foreach (var property in properties)
                {
                    var value = (bool)property.GetValue(this);
                    if (!value)
                    {
                        falseProperties.Add(property.Name);
                    }
                }

                return falseProperties;
            }
        }

        // Add methods and logic related to your view here
    }
}
