using Dapper;
using Microsoft.AspNetCore.Http;
using Oil.Management.Entities.ApplMgt;
using Oil.Management.Shared;
using Oil.Management.Shared.Constants;
using Oil.Management.Shared.Interfaces;
using Oil.Management.Shared.ViewModels.ApplMgt;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oil.Management.Services
{
    public class UserServices : IUserServices
    {
        private readonly string ServiceName = "UserAppl.Services.UserService.";
        private readonly Common common = new Common();
        public string AddUser(int IdUser, UserAddModel Data)
        {
            throw new NotImplementedException();
        }

        public string ChangePassword(string UsernamOrEmail, string OldPassword, string NewPassword1, string NewPassword2)
        {
            throw new NotImplementedException();
        }

        public string EditUser(int IdUser, UserModel Data)
        {
            throw new NotImplementedException();
        }

        public UserModel GetUser(int IdUser, out string oMessage)
        {
            throw new NotImplementedException();
        }

        public List<UserModel> GetUsers(out string oMessage)
        {
            try
            {
                oMessage = string.Empty;
                List<UserModel> ret = new List<UserModel>();
                using (IDbConnection conn = common.DbConnection)
                {
                    conn.Open();
                    var users = (from a in conn.GetList<TbUser>()select new {a}).ToList();
                    foreach (var user in users)
                    {
                        UserModel m = new UserModel();
                        m.FirstName = user.a.FirstName;

                        ret.Add(m);
                    }

                }

                return ret;
            }
            catch (Exception ex)
            {
                oMessage = common.GetErrorMessage(ServiceName + "Get Users", ex);
                return null;
            }
        }

        public UserModel Login(string UsernameOrEmail, string Password, out string oMessage)
        {
            oMessage = string.Empty;
            try
            {
                if (string.IsNullOrEmpty(UsernameOrEmail))
                {
                    oMessage = "Username or email tidak boleh kosong";
                    return null;
                }
                if (string.IsNullOrEmpty(Password))
                {
                    oMessage = "Password tidak boleh kosong";
                    return null;
                }

                using(IDbConnection conn = common.DbConnection)
                {
                    conn.Open();
                    var tbUser = (from a in conn.GetList<TbUser>()
                                  where a.Username == UsernameOrEmail || a.Email == UsernameOrEmail
                                  select new { a }).FirstOrDefault();
                    if (tbUser != null)
                    {
                        oMessage = "User tidak terdaftar";
                        return null;

                    }
                    if(tbUser.a.Status != StatusDataConstant.Aktif)
                    {
                        oMessage = "User sudah terdaftar , sudah tidak aktif/ belum aktif";
                        return null;
                    }

                    string _password = common.DecryptString(Password);
                    if (_password != Password)
                    {
                        oMessage = "Password Salah";
                        return null;
                    }

                    var _userRoles = (from a in conn.GetList<TbUserRole>()
                                      join b in conn.GetList<TbRole>() on a.IdRole equals b.IdRole
                                      where a.IdUser == tbUser.a.IdUser
                                      select new RoleModel
                                      {
                                          IdRole = b.IdRole,
                                          RoleName = b.RoleName
                                      }).ToList();

                    return new UserModel
                    {
                        IdUser = tbUser.a.IdUser,
                        UserName = tbUser.a.Username,
                        Password = null,
                        Email = tbUser.a.Email,
                        StrStatus = StatusDataConstant.DictStatusData[tbUser.a.Status],
                        Address = tbUser.a.Address,
                        FirstName = tbUser.a.FirstName,
                        MiddleName = tbUser.a.MiddleName,
                        LastName = tbUser.a.LastName,
                        FileImage = tbUser.a.FileImage,
                        LastLogin = tbUser.a.LastLogin == null ? null : tbUser.a.LastLogin.ToString(),
                        Roles = _userRoles

                    };
                }
            }
            catch (Exception ex)
            {
                oMessage = common.GetErrorMessage(ServiceName + "Login", ex);
                return null;
            }
        }

        public string ResetPassword(string UsernameOrEmail, out string oMessage)
        {
            throw new NotImplementedException();
        }

        public string SetUserActive(int IdUser, int SetIdUser, out string oMessage)
        {
            throw new NotImplementedException();
        }

        public string SetUserInActive(int IdUser, int SetIdUser, out string oMessage)
        {
            throw new NotImplementedException();
        }

        public string UpdateProfile(int IdUser, string Email, string FirstName, string MiddleName, string LastName, string Address, string PhoneNumber, string MobileNumber, IFormFile FileImage)
        {
            throw new NotImplementedException();
        }
    }
}
