using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using DOAN3.Models.Entities;
using DOAN3.Bussiness;

namespace DOAN3.Controllers
{
    public class HomeController : Controller
    {
        public QuanLySanPhamBus db = new QuanLySanPhamBus();
        public ActionResult Index(int? page)
        {
            int pagenuber = page ?? 1;
            //var ds = db.LaySPNenmua();
            var ds = db.LaySPBanChay(10, "2019");
            return View(ds.ToPagedList(pagenuber, 8));
            
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult FAQS()
        {
            return View();
        }
        public ActionResult TERMs()
        {
            return View();
        }
        public ActionResult Policies()
        {
            return View();
        }
        public ActionResult sitemap()
        {
            return View();
        }
        public ActionResult BlogPost()
        {
            return View();
        }
        public ActionResult BlogView()
        {
            return View();
        }
    }
}