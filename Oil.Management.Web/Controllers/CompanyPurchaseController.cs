using Microsoft.AspNetCore.Mvc;

namespace Oil.Management.Web.Controllers
{
    public class CompanyPurchaseController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
