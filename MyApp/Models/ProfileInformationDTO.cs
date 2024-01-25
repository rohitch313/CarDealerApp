using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Models
{
    public class ProfileInformationDTO
    {
        public string ContactNumber { get; set; }
        public string Email { get; set; }
        public string ShopAddress { get; set; }
        public string ResidenceAddress { get; set; }
        public string AlternativeNumber { get; set; }
        public string Type { get; set; }
        public string City { get; set; }
        public int AccountDetails { get; set; }
    }
}
