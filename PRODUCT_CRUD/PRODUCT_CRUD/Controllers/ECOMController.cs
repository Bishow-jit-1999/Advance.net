using PRODUCT_CRUD.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PRODUCT_CRUD.Controllers
{
    public class ECOMController : Controller
    {
        // GET: ECOM
        public ActionResult Index()
        {
            ECOMEntities db = new ECOMEntities();
            var data = db.Products.ToList();
            return View(data);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View(new Product());
        }
        [HttpPost]
        public ActionResult Create(Product P)
        {
            if (ModelState.IsValid)
            {
                ECOMEntities db = new ECOMEntities();
                db.Products.Add(P);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(P);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
           
                ECOMEntities db = new ECOMEntities();
                var edata = (from u in db.Products where u.Id == id select u).FirstOrDefault();

                return View(edata);

        }
        [HttpPost]
        public ActionResult Edit(Product edata)
        {
            ECOMEntities db = new ECOMEntities();

            var product = (from p in db.Products where p.Id == edata.Id select p).FirstOrDefault();

            db.Entry(product).CurrentValues.SetValues(edata);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            if (ModelState.IsValid)
            {
                ECOMEntities db = new ECOMEntities();
                var deldata = (from u in db.Products where u.Id == id select u).FirstOrDefault();
                db.Products.Remove(deldata);
                db.SaveChanges();
                return RedirectToAction("Index");

            }
            return View();
        }
    }
}