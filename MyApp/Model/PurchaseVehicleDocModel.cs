using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Model
{
    public class PurchaseVehicleDocModel
    {
        public string CarName { get; set; }
        public string Variant { get; set; }
        public int PurchaseId { get; set; }
        public bool Challan { get; set; }
        public bool RcStatus { get; set; }
        public bool Fitness { get; set; }
        public bool OwnerName { get; set; }
        public bool Hypothecation { get; set; }
        public bool Blacklist { get; set; }
    }
}
