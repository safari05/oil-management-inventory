using Microsoft.AspNetCore.Mvc;

namespace Oil.Management.Web.Controllers
{
    public class Company_Stock_OilController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
