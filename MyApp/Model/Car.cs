using CommunityToolkit.Mvvm.ComponentModel;
using MyApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Model
{
    public class Car:ObservableObject
    {
        // Property for the name of the car
        public string Name { get; set; }

        // Property for the variant (model) of the car
        public string Variant { get; set; }

        // Property for the amount due for the purchase of the car
        public decimal AmountDue { get; set; }
        public string Status { get; set; }

        // Property for the purchase ID of the car
        public int PurchaseId { get; set; }
        public PaymentHistoryViewModel PaymentHistory { get; set; }
        public DateTime? PendingAuditDate { get; set; }
        public string AuditStatus {  get; set; }
        public int? RemainingDays
        {
            get
            {
                if (PendingAuditDate.HasValue)
                {
                    TimeSpan timeLeft = PendingAuditDate.Value - DateTime.Today;
                    return Math.Max((int)timeLeft.TotalDays, 0);
                }
                return null;
            }
        }
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
        public VerificationDetails VerificationDetails { get; set; }

        private bool _isVerified;
        public bool IsVerified
        {
            get { return _isVerified; }
            set { SetProperty(ref _isVerified, value); }
        }
    }

}

