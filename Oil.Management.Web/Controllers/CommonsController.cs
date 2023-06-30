using Microsoft.AspNetCore.Mvc;
using Oil.Management.Shared.ViewModels;

namespace Oil.Management.Web.Controllers
{
    public class CommonsController : Controller
    {
        private readonly Commons common = new Commons();


        public ResponseModel<string> GetCurrentDate()
        {
            return new ResponseModel<string>
            {
                IsSuccess = true,
                ReturnMessage = string.Empty,
                Data = common.CurrDate.ToString("dd-MM-yyyy")
            };

        }
    }

    public class Commons
    {


        #region Get Current date
        public DateTime CurrDate;
        #endregion
    }
}
