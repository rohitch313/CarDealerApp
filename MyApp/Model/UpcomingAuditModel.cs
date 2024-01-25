using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Model
{
    public enum AudStatus
    {
        Failed,
        Inprocess,
        Sold
        // Add other statuses as needed
    }
    public class UpcomingAuditModel:ObservableObject
    {

        public int CarId { get; set; } // Foreign key referencing CarId

        // Navigation property for the related Car

        private string _selectedStatus;
        public string SelectedStatus
        {
            get { return _selectedStatus; }
            set
            {
                if (_selectedStatus != value)
                {
                    _selectedStatus = value;
                    OnPropertyChanged(nameof(SelectedStatus));
                }
            }
        }
        // CarName, Variant, and Image properties from Car
        public string CarName { get; set; }
        public string Variant { get; set; }
        public string DaysLeftToVerify { get; set; }
        private bool _isVerified;
        public bool IsVerified
        {
            get { return _isVerified; }
            set { SetProperty(ref _isVerified, value); }
        }
        public DateTime AuditDate { get; set; }
        public AudStatus?Status { get; set; }
    }
}
