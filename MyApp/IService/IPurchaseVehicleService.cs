using MyApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.IService
{
    public interface IPurchaseVehicleService
    {
        Task<List<PurchaseVehicleRecordModel>> GetVehicleRecord();
        Task<List<PurchaseVehicleDocModel>> GetVehicleRecordById(int Id);
    }
}
