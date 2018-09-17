using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TestTask.Models;
using PagedList.Mvc;
using PagedList;

namespace TestTask.Controllers
{
    public class HomeController : Controller
    {

        [HttpGet]
        public async Task<ActionResult> Index(int page = 1)
        {
            List<Tender> tenders = new List<Tender>();
      
            using (TenderContext db = new TenderContext())
                {
                //db.Tenders.Add(new Tender()
                //{
                //    Id = 1,
                //    Subject = "Item 1",
                //    Price = 10,
                //    Currency = "Доллар",
                //    Date = DateTime.Now,
                //    Start = new DateTime(2017, 10, 30),
                //    End = new DateTime(2018, 12, 30),
                //    Category = "Химическая продукция",
                //    Kind = "Открытые торги",
                //    Organizer = "Энергоатом"
                //});

                //db.Tenders.Add(new Tender()
                //{
                //    Id = 2,
                //    Subject = "Item 2",
                //    Price = 5,
                //    Currency = "Евро",
                //    Date = DateTime.Now,
                //    Start = new DateTime(2018, 10, 30),
                //    End = new DateTime(2018, 12, 30),
                //    Category = "Химическая продукция",
                //    Kind = "Открытые торги",
                //    Organizer = "Энергоатом"
                //});

                //db.Tenders.Add(new Tender()
                //{
                //    Id = 3,
                //    Subject = "Item 3",
                //    Price = 50,
                //    Currency = "Гривна",
                //    Date = DateTime.Now,
                //    Start = new DateTime(2018, 10, 30),
                //    End = new DateTime(2018, 12, 30),
                //    Category = "Химическая продукция",
                //    Kind = "Открытые торги",
                //    Organizer = "Энергоатом"
                //});

                //db.Tenders.Add(new Tender()
                //{
                //    Id = 1,
                //    Subject = "Item 4",
                //    Price = 10,
                //    Currency = "Доллар",
                //    Date = DateTime.Now,
                //    Start = new DateTime(2018, 10, 30),
                //    End = new DateTime(2018, 12, 30),
                //    Category = "Печатная продукция",
                //    Kind = "Открытые торги",
                //    Organizer = "Киевский метрополитен"
                //});

                //db.Tenders.Add(new Tender()
                //{
                //    Id = 2,
                //    Subject = "Item 5",
                //    Price = 5,
                //    Currency = "Евро",
                //    Date = DateTime.Now,
                //    Start = new DateTime(2018, 10, 30),
                //    End = new DateTime(2018, 12, 30),
                //    Category = "Химическая продукция",
                //    Kind = "Открытые торги",
                //    Organizer = "Энергоатом"
                //});

                //db.Tenders.Add(new Tender()
                //{
                //    Id = 3,
                //    Subject = "Item 6",
                //    Price = 50,
                //    Currency = "Гривна",
                //    Date = DateTime.Now,
                //    Start = new DateTime(2018, 10, 30),
                //    End = new DateTime(2018, 12, 30),
                //    Category = "Печатная продукция",
                //    Kind = "Закрытые торги",
                //    Organizer = "Укрэнерго"
                //});

                //db.SaveChanges();


                foreach (Tender t in db.Tenders)
                    {
                        tenders.Add(t);
                    }

                    ViewBag.sortOrder = tenders;

                
            }
            int pageSize = 3;
            IEnumerable<Tender> tendersPerPages = tenders.Skip((page - 1) * pageSize).Take(pageSize);
            PageInfo pageInfo = new PageInfo { PageNumber = page, PageSize = pageSize, TotalItems = tenders.Count };
            IndexViewModel ivm = new IndexViewModel { PageInfo = pageInfo, Tenders = tendersPerPages };
            
            return View(ivm);
        }

        [HttpPost]
        public ActionResult _TenderView(int? page, string organizer, string kind, string category, string search, string sort, DateTime? dateFrom, DateTime? dateTo )
        {  
            TenderContext db = new TenderContext();
            var tenders = db.Tenders.ToList();

            tenders = tenders.Where(item =>
              ((string.IsNullOrEmpty(organizer) || organizer == "Все") || item.Organizer.Equals(organizer))
           && ((string.IsNullOrEmpty(kind) || kind == "Все") || item.Kind.Equals(kind))
           && ((string.IsNullOrEmpty(category) || category == "Все") || item.Category.Equals(category))
           ).ToList();

            if (!string.IsNullOrEmpty(search))
            {
                var tendersSub = tenders.Where(a =>a.Subject!=null && a.Subject.Contains(search)).ToList();
                var tendersDes = tenders.Where(a => a.Desciption != null && a.Desciption.Contains(search)).ToList();
                tenders = tendersSub;
                tenders.AddRange(tendersDes);
            }

            if(dateFrom!=null)  tenders = tenders.Where(item => item.Date.Date >= dateFrom).ToList();
            if (dateTo != null) tenders = tenders.Where(item => item.Date.Date <= dateTo).ToList();


            //Фильтры:  Date, Category, Price
            if (!string.IsNullOrEmpty(sort))
            {
                if (sort == "Date")
                    tenders = tenders.OrderBy(a => a.Date).ToList();

                if (sort == "Price")
                    tenders = tenders.OrderBy(a => a.Price).ToList();

                if (sort == "Category")
                    tenders = tenders.OrderBy(a => a.Category).ToList();
            }
           

            int pageSize = tenders.Count;
            int current =  page??1;
            IEnumerable<Tender> tendersPerPages = tenders.Skip(( 1- 1) * pageSize).Take(pageSize);
            PageInfo pageInfo = new PageInfo { PageNumber = 1, PageSize = pageSize, TotalItems = tenders.Count };
            IndexViewModel ivm = new IndexViewModel { PageInfo = pageInfo, Tenders = tendersPerPages };
            ViewBag.sortOrder = tenders;
            return PartialView( ivm);
 
        }

}
}