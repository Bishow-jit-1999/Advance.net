using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Demo.Models;

namespace Demo.Controllers
{
    public class CVController : Controller
    {
        // GET: CV
        public ActionResult Index()
        {

            Product p = new Product();
            p.Id = 1234;
            p.Name = "KITKAT";

            return View(p);
        }

        public ActionResult Education()
        {
            return View();
        }

        public ActionResult Projects()
        {
            return View();
        }

        public ActionResult References() {

            return View();
        }

        public ActionResult list()
        {
            List<Product> pro = new List<Product>();
            for (int i=0; i < 100; i++)
            {
                var p = new Product
                {
                    Name = "KitKat" + (i + 1),

                    Id = i + 1
                };

              pro.Add(p);
            }

        
            return View(pro);
        }
              
        
    }
}