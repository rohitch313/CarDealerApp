using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Model
{
    public enum ProcurementStatus
    {
        Failed,
        Pending,
        Successful

    }
    public class ProcurmentInProcessModel:CarDetail
    {
        public ProcurementStatus? Status { get; set; }
        public string Purchased_Amount { get; set; }
    }
}
