using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Model
{
    public class ProcurementClosedModel:CarDetail
    {
        public decimal? Amount_paid { get; set; }
        public DateTime? ColsedOn { get; set; }


    }
}
