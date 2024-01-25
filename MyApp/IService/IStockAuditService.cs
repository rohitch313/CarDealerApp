using MyApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.IService
{
    public interface IStockAuditService
    {
        Task<List<UpcomingAuditModel>> GetUpcomingAudit();
        Task<List<UpcomingAuditModel>> GetPendingAudit();

        Task<List<UpcomingAuditModel>> GetAuditStatus();
        Task<List<AddressDto>> GetAddress();
        //Task<List<AddressDto>> UploadImage(int id, AddressDto address);
    }
}
