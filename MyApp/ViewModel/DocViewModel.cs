using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.ViewModel;  
public partial class DocViewModel : ObservableObject
{

    [RelayCommand]
    public async Task Back()
    {
        await Shell.Current.GoToAsync("/PaymentView");
    }

    [RelayCommand]
    public async Task Back2()
    {
        await Shell.Current.GoToAsync("/DocPaymentProofPage");
    }


    [RelayCommand]
    public async Task Back1()
    {
        await Shell.Current.GoToAsync("/DocPaymentProofPage");
    }
}
