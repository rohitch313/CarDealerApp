
using CommunityToolkit.Mvvm.ComponentModel;
using MyApp.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MyApp.IService;
using System.Text;
using MyApp.Services;

namespace MyApp.ViewModel
{


    public partial class PayAmountViewModel:ObservableObject
    {
        //[ObservableProperty]
        //PaymentDetailDto paymentDetailDto;
        //private readonly IPaymnetService _paymentService;

        ////private PaymentDetailDto _paymentDetails;
        ////public PaymentDetailDto PaymentDetails
        ////{
        ////    get => _paymentDetails;
        ////    set => SetProperty(ref _paymentDetails, value);
        ////}

        //public PayAmountViewModel(IPaymnetService paymentService)
        //{
        //    _paymentService = paymentService;

        //}

        //private async void LoadPaymentDetails()
        //{
        //    if (_navigation.TryGetRouteParameter("paymentId", out var paymentId))
        //    {
        //        if (int.TryParse(paymentId?.ToString(), out var id))
        //        {
        //            // Call your service method to get payment details by ID
        //            var paymentDetails = await _paymentService.GetPaymentDetails(id);

        //            // Update ViewModel properties with retrieved payment details
        //            PaymentDetails = paymentDetails;
        //        }
    }
}
