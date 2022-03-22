using Pharmacy.Auth;
using Pharmacy.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Pharmacy.Controllers
{
    [CustomerAccess]
    public class CustomerController : Controller
    {
        // GET: Customer
        public ActionResult Index()
        {
            PharmacyDBEntities db = new PharmacyDBEntities();
            var cat = db.Categories.ToList();
            foreach(var c in cat)
            {
                ViewData[c.name] = (from m in db.Medicines 
                                    where m.category_id == c.id
                                    select m);
            }
            return View(cat);
        }
    }
}