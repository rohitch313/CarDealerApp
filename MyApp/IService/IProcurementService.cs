using MyApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.IService
{
    public interface IProcurementService
    {
        Task<List<ProcurementFilterModel>> GetFilters();
        Task<List<ProcurementDetialModel>> GetAllProcurements();
        Task<List<ProcurementDetialModel>> GetFilterProcurement(int Id);
        Task<List<ProcurementClosedModel>> GetClosedProcurements();
        Task<List<ProcurmentInProcessModel>> GetInprocessProcurements();
    }
}
