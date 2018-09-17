using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestTask.Models;

namespace TestTask.Controllers
{
    public class DataController : Controller
    {
        // GET: Data
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult DatailInformation()
        {
            Tender tender= new Tender();
            int id = Convert.ToInt32(RouteData.Values["id"]);
            ViewBag.Message = RouteData.Values["id"];
           
            using (Models.TenderContext db = new TenderContext())
            {
                var tenders = from t in db.Tenders
                              where t.Id == id
                              select t;

                foreach (Tender t in tenders)
                    tender = t;
            }
            return View("Index", tender);
        }
    }
}