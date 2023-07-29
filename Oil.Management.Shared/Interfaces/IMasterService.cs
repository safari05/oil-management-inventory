using Oil.Management.Shared.ViewModels.Master;
using Oil.Management.Shared.ViewModels.Reference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oil.Management.Shared.Interfaces
{
    public interface IMasterService
    {
        List<FactoryModel> GetFactorys(out string oMessage);
        List<FactoryModel> GetFactoryByNames(string Name, out string oMessage);
        FactoryModel GetFactory(int IdFactory, out string oMessage);
        string AddFactory(int IdUser, FactoryAddModel Data);
        string EditFactory(int IdUser, FactoryModel Data);

        string AddBussinessUnit(int IdUser, BusinessUnitAddModel data);
        List<BusinessUnitModel> GetBusinessUnits(out string oMessage);

        List<BusinessUnitModel> GetBusinessUnitByNames(string busniessName,out string oMessage);
        BusinessUnitModel GetBusinessUnit(int IdBusinessModel, out string oMessage);

        string EditBusiness (int IdUser, BusinessUnitModel Data);


        List<SubsidiaryCompanyModel> SubsidiaryCompanys (out string oMessage);

        SubsidiaryCompanyModel SubsidiaryCompany(int IdSubsidiaryCompany, out string oMessage);

        string AddSubsidiaryCompany(int IdUser, SubsidiaryCompanyAdd Data);

        string EditSubsidiaryCompany(int IdUser, SubsidiaryCompanyAdd Data);


        List<CustomerModel> GetCustomers(out string oMessage);
        CustomerModel GetCustomer(out string oMessage);

        string CustomerCreate (int IdUser, CustomerAddModel Data);
        string CustomerEdit(int IdUser, CustomerModel Data);


    }
}
