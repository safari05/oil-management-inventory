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
using System.Transactions;

namespace Oil.Management.Services
{
    public class UserServices : IUserServices
    {
        private readonly string ServiceName = "UserAppl.Services.UserService.";
        private readonly Common common = new Common();

        private string ValidateDataUser(UserAddModel Data)
        {
            if (Data == null)
                return "data harus diisi";
            if (string.IsNullOrEmpty(Data.UserName))
                return "Username harus diisi";
            

            if (string.IsNullOrEmpty(Data.Password))
                return "Password harus diisi";
            
            if (string.IsNullOrEmpty(Data.Email))
                return "Email harus diisi";
            try
            {
                var addr = new System.Net.Mail.MailAddress(Data.Email);
                if (addr.Address != Data.Email)
                    return "alamat email salah";
            }
            catch (Exception)
            {
                return "alamat email salah";

            }
            if (string.IsNullOrEmpty(Data.FirstName))
                return "FirstName harus diisi";

            if (Data.Roles == null)
                return "Roles harus dipilih";

            return string.Empty;
        }

        public string AddUser(IDbConnection conn, int IdUser, UserAddModel Data, out int oIdUser)
        {
            oIdUser = 0;
            try
            {

                string err = ValidateDataUser(Data);
                if (!string.IsNullOrEmpty(err))
                    return err;
                var tbUsers = (from a in conn.GetList<TbUser>()
                               where a.Username == Data.UserName || a.Email == Data.Email
                               select a).ToList();

                if (tbUsers != null && tbUsers.Count > 0)
                {
                    return "Username/email sudah terdaftar";
                }
                string password = common.Encrypt(Data.Password);

                

                TbUser tbUser = new TbUser
                {
                     
                    Email = Data.Email,
                    Password = password,
                    Status = StatusDataConstant.Aktif,
                    Username = Data.UserName,
                    Address = Data.Address,
                    FirstName = Data.FirstName,
                    LastName = Data.LastName,
                    MiddleName = Data.MiddleName,
                    IdSubsidiaryCompany = Data.Subsidiary,
                    IdRole = Data.Roles[0].IdRole                    
                    
                };
                var _idUser = conn.Insert(tbUser);
                oIdUser = (int)_idUser;
                foreach (var item in Data.Roles)
                {
                    TbUserRole tbUserRole = new TbUserRole
                    {
                        IdRole = item.IdRole,
                        IdUser = (int)_idUser
                    };
                    conn.Insert(tbUserRole);
                }
                return string.Empty;
            }
            catch (Exception ex)
            {
                return common.GetErrorMessage(ServiceName + "AddUser", ex);
            }

        }
        public string AddUser(int IdUser, UserAddModel Data)
        {
            try
            {
                using (var transactionScope = new TransactionScope())
                {

                    using (IDbConnection conn = common.DbConnection)
                    {
                        conn.Open();
                        using (var tx = conn.BeginTransaction())
                        {
                            string err = AddUser(conn, IdUser, Data, out int oIdUser);
                            if (!string.IsNullOrEmpty(err)) return err;
                            transactionScope.Complete();
                            tx.Commit();
                            return string.Empty;
                        }
                    }

                    
                }
            }
            catch (Exception ex)
            {
                return common.GetErrorMessage(ServiceName + "AddUser", ex);
            }
        }

        public string ChangePassword(string UsernamOrEmail, string OldPassword, string NewPassword1, string NewPassword2)
        {
            try
            {
                using (var transactionScope = new TransactionScope())
                {
                    using(IDbConnection conn = common.DbConnection)
                    {
                        conn.Open();
                        using(var tx = conn.BeginTransaction())
                        {

                            var tbUser = conn.GetList<TbUser>("where username = @UsernamOrEmail or email = @UsernamOrEmail ", new { UsernamOrEmail }).FirstOrDefault();
                            if (tbUser == null)
                            {
                                return "User tidak terdaftar";

                            }

                            string password = common.Decrypt(tbUser.Password);
                            if (password != OldPassword)
                            {
                                return "Password lama salah";
                            }

                            if (string.IsNullOrEmpty(NewPassword1))
                            {
                                return "Password baru kosong";
                            }
                            if (string.IsNullOrEmpty(NewPassword2))
                            {
                                return "Konfirmasi password kosong";
                            }
                            if (NewPassword1 != NewPassword2)
                            {
                                return "Konfirmasi password tidak sama dengan password baru";
                            }

                            tbUser.Password = common.Encrypt(NewPassword1);
                            tbUser.LastLogin = DateTime.Now;

                            conn.Update(tbUser);

                            tx.Commit();
                            transactionScope.Complete();
                            return string.Empty;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return common.GetErrorMessage(ServiceName + "ChangePassword", ex);
            }
        }

        public string EditUser(int IdUser, UserModel Data)
        {
            throw new NotImplementedException();
        }

        public List<RoleModel> GetRoles(out string oMessage)
        {
            try
            {
                oMessage = String.Empty;
                using (IDbConnection conn = common.DbConnection)
                {
                    conn.Open();
                    return (from a in conn.GetList<TbRole>()
                            select new RoleModel
                            {
                                IdRole = a.IdRole,
                                RoleName = a.RoleName
                            }).OrderBy(x => x.IdRole).ToList();
                }
            }
            catch (Exception ex)
            {
                oMessage = common.GetErrorMessage(ServiceName + "GetRoles", ex);
                return null;
            }
        }

        public UserModel GetUser(int IdUser, out string oMessage)
        {
            try
            {
                oMessage = string.Empty;
                using(IDbConnection conn = common.DbConnection)
                {
                    var user = (from a in conn.GetList<TbUser>()
                                where a.IdUser == IdUser
                                select a).FirstOrDefault();

                    if (user == null)
                    {
                        oMessage = "data tidak ada";
                        return null;
                    }

                    var roles = (from a in conn.GetList<TbRole>()
                                 join b in conn.GetList<TbUserRole>() on a.IdRole equals b.IdRole
                                 where b.IdUser == user.IdUser
                                 select new RoleModel { IdRole = a.IdRole, RoleName = a.RoleName }).ToList();

                    return new UserModel
                    {
                        Address = user.Address,
                        Email = user.Email,
                        FirstName = user.FirstName,
                        IdUser = user.IdUser,
                        LastLogin = user.LastLogin.ToString(),
                        LastName = user.LastName,
                        MiddleName = user.MiddleName,
                        Password = "",
                        Roles = roles,
                        StrStatus = StatusDataConstant.DictStatusData[user.Status],
                        UserName = user.Username,
                        FullName = common.SetFullName(user.FirstName, user.MiddleName, user.LastName)

                    };
                }
            }
            catch (Exception ex)
            {
                oMessage = common.GetErrorMessage(ServiceName + "GetUser", ex);
                return null;
            }
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
                        var roles = (from a in conn.GetList<TbRole>()
                                     join b in conn.GetList<TbUserRole>() on a.IdRole equals b.IdRole
                                     where b.IdUser == user.a.IdUser
                                     select new RoleModel { IdRole = a.IdRole, RoleName = a.RoleName }).ToList();
                        UserModel m = new UserModel()
                        {
                            Email = user.a.Email,
                            Roles = roles,
                            IdUser = user.a.IdUser,
                            StrStatus = StatusDataConstant.DictStatusData[user.a.Status],
                            Address = user.a.Address,
                            FirstName = user.a.FirstName,
                            MiddleName = user.a.MiddleName,
                            UserName= user.a.Username,
                            LastName = user.a.LastName, 

                            
                            FullName= common.SetFullName(user.a.FirstName, user.a.MiddleName, user.a.LastName),

                        };

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
                    if (tbUser == null)
                    {
                        oMessage = "User tidak terdaftar";
                        return null;

                    }
                    if(tbUser.a.Status != StatusDataConstant.Aktif)
                    {
                        oMessage = "User sudah terdaftar , sudah tidak aktif/ belum aktif";
                        return null;
                    }

                    string _password = common.Decrypt(tbUser.a.Password);
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

        public string setPassword(string Password, out string oMessage)
        {
            oMessage = string.Empty;
            if (string.IsNullOrEmpty(Password))
            {
                oMessage = "Password tidak boleh kosong";
                return null;
            }
            try
            {
                string _createRandomPassword = common.Encrypt(Password);
                string _decryp = common.Decrypt(_createRandomPassword);
                if (Password != _decryp)
                {
                    oMessage = "Password tidak sesuai";
                    return null;
                }
                return _createRandomPassword;
            }catch (Exception ex)
            {
                oMessage = common.GetErrorMessage(ServiceName + "SetPassword", ex);
                return null;
            }

        }

        public string SetUserActive(int IdUser, int SetIdUser, out string oMessage)
        {
            try
            {
                oMessage = string.Empty;
                using (var conn = common.DbConnection)
                {
                    conn.Open();
                    TbUser tbUser = conn.Get<TbUser>(SetIdUser);
                    if (tbUser == null) { oMessage = "data tidak ada"; return null; }
                    if (tbUser.Status == StatusDataConstant.Aktif) { oMessage = "user sudah aktif"; return null; }
                    tbUser.Status = StatusDataConstant.Aktif;

                    conn.Update(tbUser);
                    return string.Empty;
                }
            }
            catch (Exception ex)
            {
                oMessage = common.GetErrorMessage(ServiceName + "SetUserActive", ex);
                return null;
            }
        }

        public string SetUserInActive(int IdUser, int SetIdUser, out string oMessage)
        {
            try
            {
                oMessage = string.Empty;
                using (var conn = common.DbConnection)
                {
                    conn.Open();
                   
                        TbUser tbUser = conn.Get<TbUser>(SetIdUser);
                        if (tbUser == null) { oMessage = "data tidak ada"; return null; }
                        if (tbUser.Status == StatusDataConstant.NoAktif) { oMessage = "user sudah tidak aktif"; return null; }

                        tbUser.Status = StatusDataConstant.NoAktif;
                        
                        conn.Update(tbUser);
                        return string.Empty;
                }
            }
            catch (Exception ex)
            {
                oMessage = common.GetErrorMessage(ServiceName + "SetUserInActive", ex);
                return null;
            }
        }

        public string UpdateProfile(int IdUser, string Email, string FirstName, string MiddleName, string LastName, string Address, string PhoneNumber, string MobileNumber, IFormFile FileImage)
        {
            throw new NotImplementedException();
        }

        
    }
}
