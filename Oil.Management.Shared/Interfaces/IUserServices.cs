using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Oil.Management.Shared.ViewModels.ApplMgt;

namespace Oil.Management.Shared.Interfaces
{
    public interface IUserServices
    {
        UserModel Login(string UsernameOrEmail, string Password, out string oMessage);
        List<UserModel> GetUsers(out string oMessage);
        UserModel GetUser(int IdUser, out string oMessage);

        string AddUser(int IdUser, UserAddModel Data);
        string EditUser(int IdUser, UserModel Data);
        string ChangePassword(string UsernamOrEmail, string OldPassword, string NewPassword1, string NewPassword2);
        string ResetPassword(string UsernameOrEmail, out string oMessage);
        string UpdateProfile(int IdUser, string Email, string FirstName, string MiddleName, string LastName, string Address, string PhoneNumber, string MobileNumber, IFormFile FileImage);
        string SetUserActive(int IdUser, int SetIdUser, out string oMessage);
        string SetUserInActive(int IdUser, int SetIdUser, out string oMessage);
    }
}
