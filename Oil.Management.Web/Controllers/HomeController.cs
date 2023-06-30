using Microsoft.AspNetCore.Mvc;
using Oil.Management.Shared.Interfaces;
using Oil.Management.Shared.ViewModels;
using Oil.Management.Shared.ViewModels.ApplMgt;
using Oil.Management.Web.Models;
using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Oil.Management.Web.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUserServices _userServices;

        private readonly int IdUser;
        private readonly string Token = string.Empty;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession _session => _httpContextAccessor.HttpContext.Session;

        public HomeController(ILogger<HomeController> logger, IUserServices userSerives, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _userServices = userSerives;
            _httpContextAccessor = httpContextAccessor;
            if (_session.IsAvailable)
            {
                if (_session.GetInt32("IdUser") != null)
                    IdUser = (int)_session.GetInt32("IdUser");
            }
        }

        public IActionResult Index()
        {
            return View();
        }

        public ResponseModel<List<UserModel>> GetUsers()
        {
            var ret = _userServices.GetUsers(out string oMessage);
            return new ResponseModel<List<UserModel>>
            {
                IsSuccess = ret != null,
                ReturnMessage = ret != null ? "" : "no record",
                Data = ret
            };
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}