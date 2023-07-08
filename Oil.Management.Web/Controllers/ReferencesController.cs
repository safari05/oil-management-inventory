using Microsoft.AspNetCore.Mvc;
using Oil.Management.Shared.Interfaces;
using Oil.Management.Shared.ViewModels;
using Oil.Management.Shared.ViewModels.Reference;

namespace Oil.Management.Web.Controllers
{
    public class ReferencesController : BaseController
    {
        private readonly IApplReferenceService _iapplReferenceService;
        private readonly int IdUser;
        private readonly string Token = string.Empty;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession _session => _httpContextAccessor.HttpContext.Session;

        public ReferencesController(IApplReferenceService applReferenceService)
        {
            _iapplReferenceService = applReferenceService;
            _httpContextAccessor = _httpContextAccessor;
            if (_session.IsAvailable)
            {
                if (_session.GetInt32("IdUser") != null)
                    IdUser = (int)_session.GetInt32("IdUser");
            }
        }


        [HttpGet("GetTypeUsers", Name = "GetTypeUsers")]
        public ResponseModel<List<TypeUserModel>> GetTypeUsers (out string oMessage)
        {
            var ret = _iapplReferenceService.GetTypeUsers (out oMessage);
           
            return new ResponseModel<List<TypeUserModel>>
            {
                IsSuccess = (string.IsNullOrEmpty(oMessage)) ? true : false,
                Data = ret,
                ReturnMessage = oMessage
            };
            
        }

        [HttpGet(Name ="GetTypeUser")]
        public ResponseModel<TypeUserModel> GetTypeUser (int IdTypeUser)
        {
            var ret = _iapplReferenceService.GetTypeUser(IdTypeUser, out string oMessage);

            return new ResponseModel<TypeUserModel>
            {
                IsSuccess = (string.IsNullOrEmpty(oMessage)) ? true : false,
                Data = ret,
                ReturnMessage = oMessage
            };

        }

        [HttpPost(Name = "AddTypeUser")]
        public ResponseModel<string> AddTypeUser([FromBody] TypeUserAddModel Data)
        {
            var ret = _iapplReferenceService.AddTypeUser(IdUser, Data, );
            return new ResponseModel<string>
            {
                IsSuccess = (string.IsNullOrEmpty(ret)) ? true : false,
                Data = null,
                ReturnMessage = ret
            };
        }

        [HttpPost(Name = "EditTypeUser")]
        public ResponseModel<string> EditTypeUser(TypeUserModel Data)
        {
            var ret = _iapplReferenceService.EditTypeUser(IdUser, Data);
            return new ResponseModel<string>
            {
                IsSuccess = (string.IsNullOrEmpty(ret)) ? true : false,
                Data = null,
                ReturnMessage = ret
            };
        }
    }
}
