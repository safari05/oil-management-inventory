using Dapper;
using Oil.Management.Entities.References;
using Oil.Management.Shared;
using Oil.Management.Shared.Constants;
using Oil.Management.Shared.Interfaces;
using Oil.Management.Shared.ViewModels.Reference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oil.Management.Services
{
    public class ApplReferenceService : IApplReferenceService
    {
        private readonly string ServiceName = "ApplReference.Services.ApplReferenceService.";
        private readonly Common common = new Common();
        public string AddTypeUser(int IdUser, TypeUserAddModel data)
        {
            try
            {
                string message = string.Empty;
                if (data == null)
                {
                    message = "Validate data cannot null";
                    return message;
                }
                if (string.IsNullOrEmpty(data.TypeName))
                {
                    message = "Type user cannot null";
                    return message;
                }
                using(var conn = common.DbConnection)
                {
                    conn.Open();
                    var tbTypeUser = (from a in conn.GetList<TbTypeUser>()
                                      where a.TypeName == data.TypeName
                                      select a).ToList();

                    if (tbTypeUser != null && tbTypeUser.Count > 0)
                    {
                        message = "Type Username already exist";
                        return message;
                    }

                    TbTypeUser newTbTypeUser = new TbTypeUser
                    {
                        TypeName = data.TypeName,
                    };
                    var _typeUser = conn.Insert(newTbTypeUser);

                    return message;
                }
                
            }
            catch (Exception ex)
            {
                return common.GetErrorMessage(ServiceName + "AddTypeUser", ex);
            }
        }

        public string EditTypeUser(int IdUser, TypeUserModel data)
        {
            if(IdUser == 0)
            {
                return "Id user not valid required";
            }
            try
            {
                using (var conn = common.DbConnection)
                {
                    conn.Open();

                    using(var tx = conn.BeginTransaction())
                    {
                        try
                        {
                           var checkUserTypeExist = (from a in conn.GetList<TbTypeUser>()
                                                     where a.IdTypeUser == data.IdTypeUser
                                                     select a).FirstOrDefault();
                            if (checkUserTypeExist == null)
                            {
                                return "Type user id not exits" + data.IdTypeUser;
                            }
                            checkUserTypeExist.TypeName = data.TypeName;
                            conn.Update(checkUserTypeExist);

                            tx.Commit();

                            return String.Empty;
                        }
                        catch (Exception ex)
                        {
                            tx.Rollback();
                            return common.GetErrorMessage(ServiceName + "EditTypeUser", ex);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return common.GetErrorMessage(ServiceName + "EditTypeUser", ex);
            }
        }

        public List<SelectComboStrModel> GetSatuanVolume()
        {
            return (from a in JenisSatuanConstant.DictJenisSatuan
                    select new SelectComboStrModel { Id = a.Key, Name = a.Value }).OrderBy(x => x.Id).ToList();
        }

        public List<SelectComboModel> GetStatusTransactionDict()
        {
            return (from a in StatusTransactionConstant.DictStatusTransaction
                    select new SelectComboModel { Id = a.Key, Name = a.Value }).OrderBy(x => x.Id).ToList();
        }

        public TypeUserModel GetTypeUser(int IdTypeUser, out string oMessage)
        {

            oMessage = string.Empty;
            try
            {
                using (var conn = common.DbConnection)
                {
                    conn.Open();
                    var getTypeUser = (from a in conn.GetList<TbTypeUser>()
                                       where a.IdTypeUser == IdTypeUser
                                       select a).FirstOrDefault();
                    if (getTypeUser == null)
                    {
                        oMessage = "data not found ";
                        return null;
                    }


                    return new TypeUserModel
                    {
                        IdTypeUser = getTypeUser.IdTypeUser,  
                        TypeName = getTypeUser.TypeName
                    };
                    
                }
            }
            catch (Exception ex)
            {
                common.GetErrorMessage(ServiceName + "GetTypeUser", ex);
                return null;
            }
        }

        public List<TypeUserModel> GetTypeUsers(out string oMessage)
        {
            oMessage= string.Empty;
            List<TypeUserModel> ret = new List<TypeUserModel>();
            try
            {
                using(var conn = common.DbConnection)
                {
                    var getTypeUsers = (from a in conn.GetList<TbTypeUser>()
                                        select a).ToList();

                    foreach (var item in getTypeUsers)
                    {
                        TypeUserModel mod = new TypeUserModel()
                        {
                            IdTypeUser= item.IdTypeUser,
                            TypeName = item.TypeName
                        };

                        ret.Add(mod);
                    }
                    
                }
                return ret;
            }
            catch (Exception ex)
            {
                common.GetErrorMessage(ServiceName + "GetTypeUsers", ex);
                return null;
            }
        }
    }
}
