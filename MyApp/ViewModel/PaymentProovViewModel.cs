using CommunityToolkit.Mvvm.ComponentModel;
using MyApp.View.PurchaseVehicle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp
{
    class PaymentProovViewModel : ObservableObject
    {
        private DocPaymentProofPage _parentPage;
        private PurchaseVehicleView _purchaseVehicleView;
        public PaymentProovViewModel(DocPaymentProofPage parentPage)
        {
            _parentPage = parentPage;
        }
        public PaymentProovViewModel(PurchaseVehicleView purchaseVehicleView)
        {
            _purchaseVehicleView = purchaseVehicleView;
            
        }
        public void Show()
        {
            PaymentProovView PaymentProov = new PaymentProovView(_parentPage);
            PaymentProov.ShowAsync();
            PaymentProovView PaymentProov2 = new PaymentProovView(_purchaseVehicleView);
            PaymentProov2.ShowAsync();
        }
    }
}
