using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DOAN3.Areas.Admin.Models;
using DOAN3.Models.DataAccess;
using DOAN3.Models.Entities;

namespace DOAN3.Areas.Admin.Controllers
{
    public class BaseController : Controller
    {
        // GET: Admin/Base
        protected override void OnActionExecuting(ActionExecutingContext filtercontext)
        {
            var usec = (TaiKhoan)Session["User_Session"];
            if (usec == null)
            {
                filtercontext.Result = new RedirectToRouteResult
                (new System.Web.Routing.RouteValueDictionary
                (new { Controller = "LoginAdmin", action = "Dangnhap"/*, Area = "Administrator"*/ }));
            }
            base.OnActionExecuting(filtercontext);
        }
    }
}