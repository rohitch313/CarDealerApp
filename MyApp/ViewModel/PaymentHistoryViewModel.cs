using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.ViewModel
{
    public partial class PaymentHistoryViewModel:ObservableObject
    {

        [ObservableProperty]
            public string status;

        [ObservableProperty]
            public string errorMessage;

        }
    }

