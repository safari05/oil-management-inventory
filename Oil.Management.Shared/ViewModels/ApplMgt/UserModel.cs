using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oil.Management.Shared.ViewModels.ApplMgt
{
    public class UserModel : UserAddModel
    {
        public int IdUser { get; set; }
        public string LastLogin { get; set; }
        public string FullName { get; set; }
        public string StrStatus { get; set; }
    }

    public class UserAddModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string FileImage { get; set; }
    }
}
