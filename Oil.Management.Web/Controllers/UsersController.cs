using Microsoft.AspNetCore.Mvc;
using Oil.Management.Shared.Interfaces;
using Oil.Management.Shared.ViewModels;
using Oil.Management.Shared.ViewModels.ApplMgt;

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

        public UsersController(IHttpContextAccessor httpContextAccessor, IUserServices userService,  IApplTaskService applTaskService)
        {
            _userService = userService;
            _applTaskService = applTaskService;
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
    }
}
