using Microsoft.AspNetCore.Mvc;
using Oil.Management.Shared.Interfaces;
using Oil.Management.Shared.ViewModels;
using Oil.Management.Shared.ViewModels.ApplMgt;
using Oil.Management.Shared.ViewModels.Master;
using Oil.Management.Shared.ViewModels.Reference;

namespace Oil.Management.Web.Controllers
{
    public class UsersController : BaseController
    {
        private readonly IUserServices _userServices;

        private readonly int IdUser;
        private readonly string Token = string.Empty;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession _session => _httpContextAccessor.HttpContext.Session;
        private readonly IUserServices _userService;
        private readonly IApplTaskService _applTaskService;
        private readonly IApplReferenceService _applReferenceService;
        private readonly IMasterService _masterService;


        public UsersController(IHttpContextAccessor httpContextAccessor, IUserServices userService,  IApplTaskService applTaskService,
            IApplReferenceService applReferenceService, IMasterService masterService)
        {
            _userService = userService;
            _applTaskService = applTaskService;
            _applReferenceService = applReferenceService;
            _masterService = masterService;
            _httpContextAccessor = httpContextAccessor;
            if (_session.IsAvailable)
            {
                if (_session.GetInt32("IdUser") != null)
                    IdUser = (int)_session.GetInt32("IdUser");
            }
        }

        public ResponseModel<string> Logout()
        {
            HttpContext.Session.Clear();
            return new ResponseModel<string>
            {
                IsSuccess = true,
                ReturnMessage = null,
                Data = null
            };
        }

       
        public ResponseModel<List<ApplTaskModel>> GetMenuss(int IdAppl)
        {
            List<ApplTaskModel> ret = _applTaskService.GetMenus(IdAppl, 5, out string oMessage);
            return new ResponseModel<List<ApplTaskModel>>
            {
                IsSuccess = (string.IsNullOrEmpty(oMessage)) ? true : false,
                ReturnMessage = oMessage,
                Data = ret
            };
        }

        public ResponseModel<string> ChangePassword(string UsernameOrEmail, string OldPassword, string NewPassword1, string NewPassword2)
        {
            string ret = _userService.ChangePassword(UsernameOrEmail, OldPassword, NewPassword1, NewPassword2);
            return new ResponseModel<string>
            {
                IsSuccess = (string.IsNullOrEmpty(ret)) ? true : false,
                ReturnMessage = ret,
                Data = null
            };
        }

        public ResponseModel<UserModel> GetUserUpdateProfile()
        {
            UserModel ret = _userService.GetUser(IdUser, out string oMessage);
            return new ResponseModel<UserModel>
            {
                IsSuccess = (string.IsNullOrEmpty(oMessage)) ? true : false,
                ReturnMessage = oMessage,
                Data = ret
            };
        }

        public ResponseModel<List<RoleModel>> GetRoles()
        {
            List<RoleModel> ret = _userService.GetRoles(out string oMessage);
            return new ResponseModel<List<RoleModel>>
            {
                IsSuccess = (string.IsNullOrEmpty(oMessage)) ? true : false,
                ReturnMessage = oMessage,
                Data = ret
            };
        }

        public ResponseModel<List<TypeUserModel>> GetTypeUser()
        {
            List<TypeUserModel> ret = _applReferenceService.GetTypeUsers(out string oMessage);
            return new ResponseModel<List<TypeUserModel>>
            {
                IsSuccess = (string.IsNullOrEmpty(oMessage)) ? true : false,
                ReturnMessage = oMessage,
                Data = ret
            };
        }

        public ResponseModel<List<SubsidiaryCompanyModel>> GetSubsidiarys()
        {
            List<SubsidiaryCompanyModel> ret = _masterService.SubsidiaryCompanys(out string oMessage);
            return new ResponseModel<List<SubsidiaryCompanyModel>>
            {
                IsSuccess = (string.IsNullOrEmpty(oMessage)) ? true : false,
                ReturnMessage = oMessage,
                Data = ret
            };
        }
    }
}
