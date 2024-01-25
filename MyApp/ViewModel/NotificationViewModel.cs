using CommunityToolkit.Mvvm.Input;
using MyApp.Model;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.ViewModel
{
    public partial class NotificationViewModel
    {
        public ObservableCollection<NotificationModel> Notifications { get; set; }

        public NotificationViewModel()
        {
            // Initialize with some static data
            Notifications = new ObservableCollection<NotificationModel>
            {
               


             new NotificationModel
            {
                Id = 3,
                Subject = "Your Payment is due",
                Message = "You have crossed 45th day since. Please complete your repayment to free your limit.",
                Pic = "admin.png",
                MessageSendTime = DateTime.Now.AddMinutes(-30)
            },
            new NotificationModel
            {
                Id = 4,
                Subject = "Purchase Vehicle",
                Message = "You have limit available and you have not purchased a vehicle for long. benefit of your limit",
                Pic = "admin.png",
                MessageSendTime = DateTime.Now.AddMinutes(-15)
            },
             new NotificationModel
            {
                Id = 5,
                Subject = "Your Payment is due",
                Message = "You have crossed 45th day since. Please complete your repayment to free your limit.",
                Pic = "admin.png",
                MessageSendTime = DateTime.Now.AddMinutes(-30)
            },
            new NotificationModel
            {
                Id = 6,
                Subject = "Purchase Vehicle",
                Message = "You have limit available and you have not purchased a vehicle for long. benefit of your limit",
                Pic = "admin.png",
                MessageSendTime = DateTime.Now.AddMinutes(-55)
            }
                // Add more notification items here
            };

            FormatMessageSendTimes();
        }

        private void FormatMessageSendTimes()
        {
            foreach (var notification in Notifications)
            {
                notification.FormattedMessageSendTime = FormatTime(notification.MessageSendTime);
            }
        }

        private string FormatTime(DateTime messageSendTime)
        {
            var currentTime = DateTime.Now;
            var timeDifference = currentTime - messageSendTime;

            if (timeDifference.TotalDays < 1)
            {
                if (timeDifference.TotalHours < 24)
                {
                    // Message was sent today or yesterday
                    if (messageSendTime.Date == currentTime.Date)
                    {
                        return "Today " + messageSendTime.ToString("hh:mm tt");
                    }
                    else if (messageSendTime.Date == currentTime.Date.AddDays(-1))
                    {
                        return "Yesterday " + messageSendTime.ToString("hh:mm tt");
                    }
                    else
                    {
                        return "Today " + messageSendTime.ToString("hh:mm tt");
                    }
                }
                else
                {
                    return "Yesterday " + messageSendTime.ToString("hh:mm tt");
                }
            }
            else
            {
                return messageSendTime.ToString("MM/dd/yyyy hh:mm tt");
            }
        }


        [RelayCommand]
        public async Task BackToAll()
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}
