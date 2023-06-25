using Microsoft.AspNetCore.Mvc;

namespace Oil.Management.Web.Controllers
{
    public class BaseController : Controller
    {
        //public bool needToDirection = true;
        public override void OnActionExecuting(Microsoft.AspNetCore.Mvc.Filters.ActionExecutingContext filterContext)
        {
            //check Session here

            var sessionUsernameOrEmail = HttpContext.Session.GetInt32("UserIdOrUsernamOrEmail");
            if (sessionUsernameOrEmail == null)
            {
                filterContext.Result =

                RedirectToAction("", "Sign");

                return;

            }
            base.OnActionExecuting(filterContext);
        }
    }
}
