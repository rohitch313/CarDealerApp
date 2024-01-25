
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Model
{
    public enum paymentStatus
    {
        Failed,
        Pending,
        Success
        // Add other statuses as needed
    }
    public class PaymentDto
    {
        public int Id { get; set; }
        public decimal? Amount_Due { get; set; }
        public int CarId { get; set; } 
        public string CarName { get; set; }
        public string Variant { get; set; }
        public paymentStatus? PaymentStatus { get; set; }

       
    }
}
