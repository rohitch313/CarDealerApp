using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MyApp.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MyApp.ViewModel
{
    public partial class VerificationViewModel : ObservableObject
    {

        public Car SelectedCar { get; set; }

        // Additional properties for errors
        private string locationErrors;
        private string purposeErrors;
        private string dateErrors;

        public string LocationErrors
        {
            get { return locationErrors; }
            set
            {
                if (locationErrors != value)
                {
                    locationErrors = value;
                    OnPropertyChanged(nameof(LocationErrors));
                }
            }
        }

        public string PurposeErrors
        {
            get { return purposeErrors; }
            set
            {
                if (purposeErrors != value)
                {
                    purposeErrors = value;
                    OnPropertyChanged(nameof(PurposeErrors));
                }
            }
        }
        [RelayCommand]
        public async Task Back()
        {
            await Shell.Current.GoToAsync("/StockAuditView");
        }
        public string DateErrors
        {
            get { return dateErrors; }
            set
            {
                if (dateErrors != value)
                {
                    dateErrors = value;
                    OnPropertyChanged(nameof(DateErrors));

                }
            }
        }



        private string location;

        [Required(ErrorMessage = "Location is required.")]
        public string Location
        {
            get { return location; }
            set
            {
                if (location != value)
                {
                    location = value;
                    OnPropertyChanged(nameof(Location));
                    ValidateLocation();

                }
            }
        }

        private void ValidateLocation()
        {
            if (string.IsNullOrEmpty(Location))
            {
                LocationErrors = "Location is required.";
            }
            else
            {
                LocationErrors = null; // Clear the error if name is provided
            }
        }

        private List<string> purpose;

        private DateTime choseDate;

        [DataType(DataType.Date)]
        [DateNotLessThanCurrent(ErrorMessage = "Selected date must not be in the past.")]
        public DateTime ChoseDate
        {
            get { return choseDate; }
            set
            {
                if (choseDate != value)
                {
                    choseDate = value;
                    OnPropertyChanged(nameof(ChoseDate));
                }
            }
        }

        // Other properties and methods



        public VerificationDetails VerificationDetails { get; set; }

        // ... (existing properties)

        public List<string> Purpose
        {
            get { return purpose; }
            set
            {
                if (purpose != value)
                {
                    purpose = value;
                    OnPropertyChanged(nameof(Purpose));
                }
            }
        }

        private string selectedPurpose;

        [Required(ErrorMessage = "Purpose is required.")]
        public string SelectedPurpose
        {
            get { return selectedPurpose; }
            set
            {
                if (selectedPurpose != value)
                {
                    selectedPurpose = value;
                    OnPropertyChanged(nameof(SelectedPurpose));
                    ValidateSelectedPurpose();
                }
            }
        }
        private void ValidateSelectedPurpose()
        {
            if (string.IsNullOrEmpty(SelectedPurpose))
            {
                PurposeErrors = "Purpose is required.";
            }
            else
            {
                PurposeErrors = null; // Clear the error if state is selected
            }
        }
        public VerificationViewModel(Car selectedCar)
        {
            SelectedCar = selectedCar;
            VerificationDetails = new VerificationDetails(); // Initialize the details
            Purpose = GetPurposeList();

        }
        private List<string> GetPurposeList()
        {
            return new List<string>
            {
                "Repair",
                "Sub Unit/Showroom",
                "To sell at others dealers place",
                "Other",


            };
        }
        [RelayCommand]
        private async Task Submit()
 {
            try
            {
                LocationErrors = null;
                PurposeErrors = null;
                DateErrors = null;

                var location =Location;
                var purpose = SelectedPurpose;
                var choseDate = ChoseDate;

                var context = new ValidationContext(this);
                var results = new List<ValidationResult>();

                if (!Validator.TryValidateObject(this, context, results, true))
                {
                    foreach (var result in results)
                    {
                        foreach (var memberName in result.MemberNames)
                        {
                            Console.WriteLine($"Validation Error for {memberName}: {result.ErrorMessage}");
                            switch (memberName)
                            {
                                case nameof(Location):
                                    LocationErrors = result.ErrorMessage;
                                    break;
                                case nameof(ChoseDate):

                                    DateErrors = result.ErrorMessage;

                                    break;
                                case nameof(SelectedPurpose):
                                    PurposeErrors = result.ErrorMessage;
                                    break;
                            }
                        }
                    }

                    await Application.Current.MainPage.DisplayAlert("Failed", "Form submitted Failed!", "OK");
                    return;
                }

                // Validation passed, you can proceed with the submission logic here
               
                // Clear fields after successful submission




                // Create a new instance of VerificationDetails and associate it with the selected car
                SelectedCar.VerificationDetails = new VerificationDetails
                {
                    Location = location,
                    Purpose = purpose,
                    ChoseDate = choseDate
                };

                // Now the selected car has the associated verification details.
                ResetFields();
                await Shell.Current.CurrentPage.DisplayAlert("Success", "Verification details submitted successfully.", "OK");
                await Shell.Current.GoToAsync("/StockAuditView");

            }
            catch (Exception ex)
            {
                // Handle any unexpected errors
                await Shell.Current.CurrentPage.DisplayAlert("Error", "An unexpected error occurred. Please try again later.", "OK");
                Console.WriteLine(ex);
            }
        }
        private void ResetFields()
        {
            Location = string.Empty;
            SelectedPurpose = null;
            ChoseDate = DateTime.Today;
             LocationErrors = null;
                PurposeErrors = null;
                DateErrors = null;
        }
        public class DateNotLessThanCurrentAttribute : ValidationAttribute
        {
            public override bool IsValid(object value)
            {
                if (value is DateTime date)
                {
                    return date >= DateTime.Now.Date;
                }

                return false;
            }
        }
    }
}
