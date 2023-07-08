using Oil.Management.Shared.ViewModels.Reference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oil.Management.Shared.Interfaces
{
    public interface IApplReferenceService
    {
        string AddTypeUser(int IdUser, TypeUserAddModel data);
        List<TypeUserModel> GetTypeUsers(out string oMessage);

        TypeUserModel GetTypeUser(int IdTypeUser, out string oMessage);

        string EditTypeUser (int IdUser, TypeUserModel data);
    }
}
