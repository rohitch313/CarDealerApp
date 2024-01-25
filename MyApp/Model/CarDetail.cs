using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Model
{
    public class CarDetail
    {
        public string CarName { get; set; }
        public string Variant { get; set; }
        public int PurchaseId { get; set; }
        public int FilterId { get; set; }
    }
}
