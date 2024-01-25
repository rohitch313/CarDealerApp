using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Models
{
    public class LoginUserPhoneDTO
    {
        [Key]
        public int PhoneId { get; set; }
        public string PhoneNumber { get; set; }
    }
}
