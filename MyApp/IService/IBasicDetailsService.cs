using MyApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.IService
{
    public interface IBasicDetailsService
    {
        Task<bool> PostUserDetails(BasicDetailsDTO userDetails);
        Task<List<DropDownStateDTO>> GetState();
    }
}
