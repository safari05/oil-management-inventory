using Dapper;
using Oil.Management.Entities.Master;
using Oil.Management.Shared;
using Oil.Management.Shared.Constants;
using Oil.Management.Shared.Interfaces;
using Oil.Management.Shared.ViewModels.Master;
using Oil.Management.Shared.ViewModels.Reference;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Oil.Management.Services
{
    public class MasterService : IMasterService
    {
        private readonly string ServiceName = "Master.Services.MasterService.";
        private readonly Common common = new Common();

      

        public string AddBussinessUnit(int IdUser, BusinessUnitAddModel data)
        {
            try
            {
                if (data.BusinessUnitName == "")
                {
                    return "Business Name required";
                }
                using (IDbConnection conn = common.DbConnection)
                {
                    TmBusinessUnit tb = new TmBusinessUnit()
                    {
                        BussinessName = data.BusinessUnitName,
                        Status = 1,
                        Created = DateTime.Now,
                    };

                    conn.Insert(tb);
                    return string.Empty;
                }
            }
            catch (Exception ex)
            {
                return common.GetErrorMessage(ServiceName + "AddBussinessUnit", ex);
            }
        }

        public string AddFactory(int IdUser, FactoryAddModel Data)
        {
            try
            {
                using (IDbConnection conn = common.DbConnection)
                {
                    conn.Open();
                    var tbMFactory = (from a in conn.GetList<TbMFactoryOil>()
                                      where a.FactoryName == Data.FactoryName
                                      select a).ToList();

                    if (tbMFactory != null && tbMFactory.Count > 0)
                    {
                        return "Factory name already exist";
                    }

                    TbMFactoryOil _factoryAdd = new TbMFactoryOil
                    {
                        FactoryName = Data.FactoryName,
                        Nib = Data.Nib,
                        Address = Data.Address,
                        Email = Data.Email,
                        Phone = Data.Phone,
                        Pic = Data.Pic,
                        Status = 1,
                        CreatedBy = IdUser,
                        CreatedDt = DateTime.Now,

                    };

                    var _factory = conn.Insert(_factoryAdd);

                    return String.Empty;
                }
            }
            catch (Exception ex)
            {
                return common.GetErrorMessage(ServiceName + "AddFactory", ex);
            }
        }

        public string AddSubsidiaryCompany(int IdUser, SubsidiaryCompanyAdd Data)
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
                            TmSubsidiaryCompany entity = new TmSubsidiaryCompany();
                            entity.IdFactory = Data.IdFactory;
                            entity.IdBussinessUnit = Data.IdBusniessUnit;
                            entity.Name = Data.Name;
                            entity.Nib = Data.Nib;
                            entity.Npwp = Data.Npwp;
                            entity.Phone = Data.Phone;
                            entity.Fax = Data.Fax;
                            entity.Email = Data.Email;
                            entity.Status = 1;
                            entity.Description = Data.Description;
                            entity.PicName = Data.PicName;
                            entity.PicPhone = Data.PicPhone;
                            entity.PicEmail = Data.PicEmail;
                            entity.CreatedBy = IdUser;
                            entity.CreatedDt = DateTime.Now;
                            entity.Priority = 0;


                            conn.Insert(entity);
                            
                            tx.Commit();
                            transactionScope.Complete();

                            return String.Empty;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return common.GetErrorMessage(ServiceName + "AddSubsidiaryCompany", ex);
            }
        }

        public string CustomerCreate(int IdUser, CustomerAddModel Data)
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
                            TmCustomer entity = new TmCustomer();
                            
                            entity.IdSubsidiaryCompany = Data.IdSubsidiaryCompany;
                            entity.CustomerName = Data.CutomerName;
                            entity.Nib = Data.Nib;
                            entity.Npwp = Data.Npwp;
                            entity.Phone = Data.Phone;
                            entity.Email = Data.Email;
                            entity.Status = 1;
                            entity.Website = "-";
                            entity.PicName = Data.PicName;
                            entity.PicPhone = Data.PicPhone;
                            entity.PicEmail = Data.PicEmail;
                            entity.Address = Data.Address;
                            entity.CreatedBy = IdUser;
                            entity.CreatedDt = DateTime.Now;
                            entity.Priority = 0;
                            entity.IdVillage = 0;

                            conn.Insert(entity);

                            tx.Commit();
                            transactionScope.Complete();

                            return String.Empty;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return common.GetErrorMessage(ServiceName + "CustomerCreate", ex);
            }
        }

        public string CustomerEdit(int IdUser, CustomerModel Data)
        {
            throw new NotImplementedException();
        }

        public string EditBusiness(int IdUser, BusinessUnitModel Data)
        {
            if (IdUser == 0)
            {
                return "Id user not valid required";
            }
            try
            {
                using(IDbConnection conn = common.DbConnection)
                {

                    var businessUnit = (from a in conn.GetList<TmBusinessUnit>()
                                            where a.IdBussinessUnit == Data.IdBusinessUnit
                                        select a).FirstOrDefault();
                    if(businessUnit == null)
                    {
                        return "record not found";
                    }

                    businessUnit.BussinessName = Data.BusinessUnitName;
                    businessUnit.Status = 1;

                    conn.Update(businessUnit);
                    return String.Empty;
                }
            }
            catch (Exception ex)
            {
                return common.GetErrorMessage(ServiceName + "GetFactorys", ex);
            }
        }

        public string EditFactory(int IdUser, FactoryModel Data)
        {

            if (IdUser == 0)
            {
                return "Id user not valid required";
            }

            try
            {
                using(var transactionScope  = new TransactionScope())
                {
                    using (IDbConnection conn = common.DbConnection)
                    {
                        conn.Open();
                        using(var tx = conn.BeginTransaction())
                        {
                            var factory = (from a in conn.GetList<TbMFactoryOil>()
                                           where a.IdFactory == Data.IdFactory
                                           select a).FirstOrDefault();

                            if (factory == null)
                            {
                                return "Data not found";
                            }

                            factory.FactoryName = Data.FactoryName;
                            factory.Nib = Data.Nib;
                            factory.Address = Data.Address;
                            factory.Phone = Data.Phone;
                            factory.Pic = Data.Pic;
                            factory.Status = 1;
                            factory.UpdatedBy = IdUser;
                            factory.UpdatedDate = DateTime.Now;

                            conn.Update(factory);
                            tx.Commit();
                            transactionScope.Complete();
                            return String.Empty;
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                return common.GetErrorMessage(ServiceName + "GetFactorys", ex);
            }
        }

        public string EditSubsidiaryCompany(int IdUser, SubsidiaryCompanyAdd Data)
        {
            try
            {
                using(var TransactionScope = new TransactionScope())
                {
                    using(IDbConnection conn = common.DbConnection)
                    {
                        using (var tx = conn.BeginTransaction())
                        {
                            var getSubsidiaryCompany = (from a in conn.GetList<TmSubsidiaryCompany>()
                                                        join b in conn.GetList<TbMFactoryOil>() on a.IdFactory equals b.IdFactory
                                                        join c in conn.GetList<TmBusinessUnit>() on a.IdBussinessUnit equals c.IdBussinessUnit
                                                        where a.IdFactory == Data.IdFactory
                                                        select new { a, b, c }).FirstOrDefault();
                            if (getSubsidiaryCompany == null)
                            {
                                return " data no found";
                            }

                            getSubsidiaryCompany.a.Name = Data.Name;
                            getSubsidiaryCompany.a.Npwp = Data.Npwp;
                            getSubsidiaryCompany.a.Nib = Data.Nib;
                            getSubsidiaryCompany.a.Phone = Data.Phone;
                            getSubsidiaryCompany.a.Fax = Data.Fax;
                            getSubsidiaryCompany.a.Email = Data.Email;
                            getSubsidiaryCompany.a.PicName = Data.PicName;
                            getSubsidiaryCompany.a.PicEmail = Data.PicEmail;
                            getSubsidiaryCompany.a.PicPhone = Data.PicPhone;
                            getSubsidiaryCompany.a.Npwp = Data.Npwp;
                            getSubsidiaryCompany.a.IdFactory = Data.IdFactory;
                            getSubsidiaryCompany.a.Description = Data.Description;
                            conn.Update(getSubsidiaryCompany);
                            tx.Commit();
                            TransactionScope.Complete();

                            return String.Empty;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                return common.GetErrorMessage(ServiceName + "EditSubsidiaryCompany", ex);
            }
        }

        public BusinessUnitModel GetBusinessUnit(int IdBusinessModel, out string oMessage)
        {
            oMessage = String.Empty;
            try
            {
                using (IDbConnection conn = common.DbConnection)
                {

                    var businessUnit = (from a in conn.GetList<TmBusinessUnit>()
                                         select a).FirstOrDefault();

                    if(businessUnit == null)
                    {
                        oMessage = "data not found";
                    }

                    BusinessUnitModel mod = new BusinessUnitModel()
                    {
                        IdBusinessUnit = businessUnit.IdBussinessUnit,
                        BusinessUnitName = businessUnit.BussinessName,
                        StrStatus = StatusDataConstant.DictStatusData[businessUnit.Status],
                    };
                    return mod;
                }
            }
            catch (Exception ex)
            {
                common.GetErrorMessage(ServiceName + "GetFactorys", ex);
                return null;
            }
        }

        public List<BusinessUnitModel> GetBusinessUnitByNames(string busniessName,out string oMessage)
        {
            try
            {
                oMessage = String.Empty;
                List<BusinessUnitModel> rets = new List<BusinessUnitModel>();
                using(IDbConnection conn = common.DbConnection)
                {
                    var getBusiness = (from a in conn.GetList<TmBusinessUnit>()
                        select a).Where(a => a.BussinessName.Contains(busniessName)).ToList();
                    foreach (var item in getBusiness)
                    {
                        BusinessUnitModel mod = new BusinessUnitModel()
                        {
                            IdBusinessUnit = item.IdBussinessUnit,
                            BusinessUnitName = item.BussinessName


                        };
                        rets.Add(mod);
                    }

                    return rets;
                }
            }
            catch (Exception ex)
            {
                oMessage= common.GetErrorMessage(ServiceName + "GetBusinessUnitByNames", ex);
                return null;
            }
        }

        public List<BusinessUnitModel> GetBusinessUnits(out string oMessage)
        {
            oMessage = String.Empty;
            try
            {
                List<BusinessUnitModel> rets = new List<BusinessUnitModel>();
                using (IDbConnection conn = common.DbConnection)
                {

                    var businessUnits = (from a in conn.GetList<TmBusinessUnit>()
                                         select a).ToList();

                    foreach (var item in businessUnits)
                    {
                        BusinessUnitModel model = new BusinessUnitModel()
                        {
                            IdBusinessUnit = item.IdBussinessUnit,
                            BusinessUnitName = item.BussinessName,
                            StrStatus = StatusDataConstant.DictStatusData[item.Status],
                        };

                        rets.Add(model);
                    }

                    return rets;
                }
            }
            catch (Exception ex)
            {
                common.GetErrorMessage(ServiceName + "GetFactorys", ex);
                return null;
            }
        }

        public CustomerModel GetCustomer(out string oMessage)
        {
            throw new NotImplementedException();
        }

        public List<CustomerModel> GetCustomers(out string oMessage)
        {
            try
            {
                oMessage = string.Empty;
                List<CustomerModel> rets = new List<CustomerModel>();
                using (IDbConnection conn = common.DbConnection)
                {
                    conn.Open();
                    var customers = (from a in conn.GetList<TmCustomer>()
                                     join b in conn.GetList<TmSubsidiaryCompany>() on a.IdSubsidiaryCompany equals b.IdSubsidiaryCompany
                                    select new { a, b}).ToList();
                    foreach (var customer in customers)
                    {
                        CustomerModel m = new CustomerModel()
                        {
                            IdCustomer= customer.a.CustomerID,
                            CutomerName = customer.a.CustomerName,
                            Nib = customer.a.Nib,
                            Address = customer.a.Address,
                            Email = customer.a.Email,
                            NameSubsidiary = customer.b.Name,
                            Npwp = customer.a.Npwp,
                            Phone = customer.a.Phone,
                            PicEmail = customer.a.PicEmail,
                            PicName = customer.a.PicName,
                            PicPhone = customer.b.PicPhone,
                            Website = customer.a.Website,
                            StrStatus = StatusDataConstant.DictStatusData[customer.a.Status]
                        };

                        rets.Add(m);
                    }
                }
                return rets;
            }
            catch (Exception ex)
            {
                oMessage = common.GetErrorMessage(ServiceName + "GetFactorys", ex);
                return null;
            }
        }

        public FactoryModel GetFactory(int IdFactory, out string oMessage)
        {
            oMessage = String.Empty;
            try
            {
                using(IDbConnection conn = common.DbConnection)
                {
                    conn.Open();
                    var getFactory = (from a in conn.GetList<TbMFactoryOil>()
                                      where a.IdFactory == IdFactory
                                      select a).FirstOrDefault(); 
                    if(getFactory == null)
                    {
                        oMessage = "data not found";
                        return null;
                    }

                    return new FactoryModel
                    {
                        IdFactory = getFactory.IdFactory,
                        FactoryName = getFactory.FactoryName,
                        Nib = getFactory.Nib,
                        Pic = getFactory.Pic,
                        Phone = getFactory.Phone,
                        Email = getFactory.Email,
                        Address = getFactory.Address,
                        StrStatus = StatusDataConstant.DictStatusData[getFactory.Status]
                    };

                }
            }
            catch (Exception ex)
            {
                oMessage = common.GetErrorMessage(ServiceName + "GetFactorys", ex);
                return null;
            }
        }

        public List<FactoryModel> GetFactoryByNames(string Name, out string oMessage)
        {
            try
            {   
                oMessage = String.Empty;
                List<FactoryModel> ret = new List<FactoryModel>();
                using(IDbConnection conn = common.DbConnection)
                {
                    var factoryDatas = (from a in conn.GetList<TbMFactoryOil>()
                                        select a).Where(a => a.FactoryName.Contains(Name)).ToList();

                    foreach(var factoryName in factoryDatas)
                    {

                        FactoryModel mod = new FactoryModel();
                        mod.IdFactory = factoryName.IdFactory;
                        mod.FactoryName = factoryName.FactoryName; 
                        ret.Add(mod);
                    }

                    return ret;
                }
            }
            catch (Exception ex)
            {
                oMessage = common.GetErrorMessage(ServiceName + "GetFactoryByNames ", ex);
                return null;
            }
        }

        public List<FactoryModel> GetFactorys(out string oMessage)
        {
            try
            {
                oMessage = string.Empty;
                List<FactoryModel> rets = new List<FactoryModel>();
                using(IDbConnection conn = common.DbConnection)
                {
                    conn.Open();
                    var factorys = (from a in conn.GetList<TbMFactoryOil>()
                                    select a).ToList();
                    foreach(var factory in factorys)
                    {
                        FactoryModel m = new FactoryModel()
                        {
                            IdFactory = factory.IdFactory,
                            FactoryName = factory.FactoryName,
                            Nib = factory.Nib,
                            Email = factory.Email,
                            Address = factory.Address,
                            Phone = factory.Phone,
                            Pic = factory.Pic,
                            StrStatus = StatusDataConstant.DictStatusData[factory.Status]
                        };

                        rets.Add(m);
                    }
                }
                return rets;
            }
            catch (Exception ex)
            {
                oMessage = common.GetErrorMessage(ServiceName + "GetFactorys", ex);
                return null;
            }
        }

        public SubsidiaryCompanyModel SubsidiaryCompany(int IdSubsidiaryCompany, out string oMessage)
        {
            oMessage = string.Empty;
            try
            {
                using(IDbConnection conn = common.DbConnection)
                {
                    var data = (from a in conn.GetList<TmSubsidiaryCompany>()
                                join b in conn.GetList<TbMFactoryOil>() on a.IdFactory equals b.IdFactory
                                join c in conn.GetList<TmBusinessUnit>() on a.IdBussinessUnit equals c.IdBussinessUnit
                                where a.IdSubsidiaryCompany == IdSubsidiaryCompany
                                select new { a, b, c }).FirstOrDefault();

                    if (data == null)
                    {
                        oMessage = " Data not found";
                        return null;
                    }

                    SubsidiaryCompanyModel mod = new SubsidiaryCompanyModel
                    {
                        IdSubsidiaryCompany = data.a.IdSubsidiaryCompany,
                        Name = data.a.Name,
                        IdBusniessUnit = data.c.IdBussinessUnit,
                        IdFactory = data.b.IdFactory,
                        Nib = data.a.Nib,
                        Phone = data.a.Phone,
                        Email = data.a.Email,
                        Fax = data.a.Fax,
                        PicEmail = data.a.PicEmail,
                        PicPhone = data.a.PicPhone,
                        PicName = data.a.PicName,   
                        Description = data.a.Description,
                        
                    };

                    return mod;
                }
            }
            catch (Exception ex)
            {
                common.GetErrorMessage(ServiceName + "SubsidiaryCompany", ex);
                return null;
            }
        }

        public List<SubsidiaryCompanyModel> SubsidiaryCompanys(out string oMessage)
        {
            List<SubsidiaryCompanyModel> rets = new List<SubsidiaryCompanyModel>();
            oMessage = String.Empty;
            try
            {

                using (IDbConnection conn = common.DbConnection)
                {

                    var datas = (from a in conn.GetList<TmSubsidiaryCompany>()
                                    join b in conn.GetList<TbMFactoryOil>() on a.IdFactory equals b.IdFactory
                                    join c in conn.GetList<TmBusinessUnit>() on a.IdBussinessUnit equals c.IdBussinessUnit
                                    select new { a, b, c }
                                 ).ToList();


                    foreach (var item in datas)
                    {
                        SubsidiaryCompanyModel mod = new SubsidiaryCompanyModel
                        {
                            IdSubsidiaryCompany = item.a.IdSubsidiaryCompany,
                            Name = item.a.Name,
                            IdBusniessUnit = item.c.IdBussinessUnit,
                            FactoryName = item.b.FactoryName,
                            BusinessName = item.c.BussinessName,
                            IdFactory = item.b.IdFactory,
                            Nib = item.a.Nib,
                            Phone = item.a.Phone,
                            Email = item.a.Email,
                            Fax = item.a.Fax,
                            PicEmail = item.a.PicEmail,
                            PicPhone = item.a.PicPhone,
                            PicName = item.a.PicName,
                            Description = item.a.Description,

                        };


                        rets.Add(mod);

                    }
                    return rets;
                }
            }
            catch (Exception ex)
            {
                oMessage =common.GetErrorMessage(ServiceName + "SubsidiaryCompanys", ex);
                return null;
            }
        }
    }
}
