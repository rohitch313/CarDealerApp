using MyApp.Model;
using MyApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.IService
{
    public interface IPostLogInService
    {
        Task<bool> PostLoginNumberAsync(string mobileNumber);
        Task<string> Verify(string mobileNumber, int? otp);
        Task<bool> ResendOtp(string mobileNumber);
        Task<bool> Logout();

        Task<string> VerifySignup(string mobileNumber, int? otp);
        Task<bool> Signup(string mobileNumber);
        Task<string> SignUpComplete(string mobileNumber, BasicDetailsDTO basicDetailsDTO);
        Task<List<DropDownStateDTO>> GetState();

        Task<UserStatusDto> GetUserStatus(string mobliePhone);
    }
}
