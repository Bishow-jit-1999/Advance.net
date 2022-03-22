using AutoMapper;
using Pharmacy.Auth;
using Pharmacy.Models.Database;
using Pharmacy.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Pharmacy.Controllers
{
    [AdminAccess]
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            PharmacyDBEntities db = new PharmacyDBEntities();
            ViewBag.orders = db.Orders.ToList().Count();
            ViewBag.suppliers = db.Suppliers.ToList().Count();
            ViewBag.medicines = db.Medicines.ToList().Count();
            ViewBag.customers = (from c in db.Users
                                 where c.role.Equals("Customer")
                                 select c).ToList().Count();
            return View();
        }


        //Medicines

        [HttpGet]
        public ActionResult MedicineAdd()
        {
            PharmacyDBEntities db = new PharmacyDBEntities();
            
            var sup = db.Suppliers.ToList();
            ViewBag.suppliers = sup;
            var cat = db.Categories.ToList();
            ViewBag.category = cat;
            return View(new Medicine());
        }
        [HttpPost]
        public ActionResult MedicineAdd(Medicine medicine)
        {
            PharmacyDBEntities db = new PharmacyDBEntities();
            if (ModelState.IsValid)
            {
                //var isStocked = (from m in db.Medicines
                //                 where m.name.Equals(medicine.name) &&
                //                 m.generic.Equals(medicine.generic)
                //                 select m).FirstOrDefault();
                //if (isStocked != null)
                //{
                //    medicine.id = isStocked.id;
                //    medicine.quantity += isStocked.quantity;
                //    db.Entry(isStocked).CurrentValues.SetValues(medicine);
                //    db.SaveChanges();
                //    return View("MedicineList");
                //}

                //var config = new MapperConfiguration(
                //        cfg =>
                //        {
                //            cfg.CreateMap<Medicine, MedicineModel>();
                //        }
                //    );
                //Mapper mapper = new Mapper(config);
                //var data = mapper.Map<MedicineModel>(medicine);

                //db.Medicines.Add(data);

                db.Medicines.Add(medicine);
                db.SaveChanges();
                return RedirectToAction("MedicineList");
            }
            var sup = db.Suppliers.ToList();
            ViewBag.suppliers = sup;
            var cat = db.Categories.ToList();
            ViewBag.category = cat;
            return View(medicine);
        }

        public ActionResult MedicineList()
        {
            PharmacyDBEntities db = new PharmacyDBEntities();
            //var medicines = db.Medicines.ToList();
            var medicineList = (from m in db.Medicines
                            group m by new { m.name, m.generic } into mg
                            select new { 
                                    //mg.id,
                                    mg.Key.name, 
                                    mg.Key.generic,
                                    //mg.Key.rate,
                                    //mg.supplier_id,
                                    //mg.Key.expired_date,
                                    //mg.Key.category_id,
                                    quantity = mg.Sum(stocksum => stocksum.quantity),
                                    rate = mg.Max(r=>r.rate),
                                    otherItems = mg
                                }).ToList();
            List<Medicine> medicines = new List<Medicine>();
            foreach(var m in medicineList)
            {
                Medicine medicine = new Medicine();
                medicine.name = m.name;
                medicine.rate = m.rate;
                medicine.quantity = m.quantity;
                medicine.generic = m.generic;
                
                foreach(var other in m.otherItems)
                {
                    medicine.id = other.id;
                    medicine.expired_date = other.expired_date;
                    medicine.category_id = other.category_id;
                    medicine.supplier_id = other.supplier_id;
                }
                medicines.Add(medicine);

                ViewData[m.name + m.generic] = (from md in db.Medicines
                                  where md.name.Equals(m.name) && md.generic.Equals(m.generic)
                                  select md).ToList();

            }
            return View(medicines);
        }
        public ActionResult MedicineDelete(int id)
        {
            PharmacyDBEntities db = new PharmacyDBEntities();
            var medicine = (from m in db.Medicines
                           where m.id.Equals(id)
                           select m).FirstOrDefault();
            db.Medicines.Remove(medicine);
            db.SaveChanges();

            return RedirectToAction("MedicineList");
        }
        [HttpGet]
        public ActionResult MedicineEdit(int id)
        {
            PharmacyDBEntities db = new PharmacyDBEntities();
            var medicine = (from m in db.Medicines
                           where m.id.Equals(id)
                           select m).FirstOrDefault();
            var sup = db.Suppliers.ToList();
            ViewBag.suppliers = sup;
            var cat = db.Categories.ToList();
            ViewBag.category = cat;
            return View(medicine);
        }
        [HttpPost]
        public ActionResult MedicineEdit(Medicine UpdateMedicine)
        {
            PharmacyDBEntities db = new PharmacyDBEntities();
            var medicine = (from m in db.Medicines
                            where m.id.Equals(UpdateMedicine.id)
                            select m).FirstOrDefault();
            db.Entry(medicine).CurrentValues.SetValues(UpdateMedicine);
            db.SaveChanges();
            return RedirectToAction("MedicineList");
        }


        //Suppliers 
        public ActionResult SupplierList()
        {
            PharmacyDBEntities db = new PharmacyDBEntities();
            var suppliers = db.Suppliers.ToList();
            return View(suppliers);
        }


        //Customers
        public ActionResult CustomerList()
        {
            PharmacyDBEntities db = new PharmacyDBEntities();
            var customers = (from c in db.Users
                             where c.role.Equals("Customer")
                             select c);
            return View(customers);
        }
    }
}