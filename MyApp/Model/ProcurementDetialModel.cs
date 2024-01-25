using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Model
{
    public class ProcurementDetialModel:CarDetail
    {
        

        public decimal? Facility_Availed { get; set; }
        public decimal? Invoice_Charges { get; set; }
        public decimal? Amount_due { get; set; }
        public decimal? Amount_paid { get; set; }
        public decimal? Processing_charges { get; set; }
    }
}
