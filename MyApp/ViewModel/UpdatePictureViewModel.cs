using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.ViewModel
{
   public partial class UpdatePictureViewModel:ObservableObject
    {
        [RelayCommand]
        public async Task Back()
        {
            await Shell.Current.GoToAsync(nameof(StockAuditView));
        }
    }
}
