using Pharmacy.Models;
using Pharmacy.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Pharmacy.Controllers
{
    public class AuthController : Controller
    {
        // GET: Auth
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            if (User.Identity.IsAuthenticated) { return RedirectToAction("Index", "Home"); }
            return View(new Login());
        }
        [HttpPost]
        public ActionResult Login(Login login)
        {
            if (ModelState.IsValid)
            {
                PharmacyDBEntities db = new PharmacyDBEntities();
                var data = (from u in db.Users
                            where u.password.Equals(login.password) &&
                            u.email.Equals(login.email)
                            select u).FirstOrDefault();
                if (data != null)
                {
                    FormsAuthentication.SetAuthCookie(data.email, false);
                    Session["userEmail"] = data.email;
                    Session["userRole"] = data.role;
                    return RedirectToAction("Index", "Customer");
                }
                TempData["incorrectPass"] = "Invalid Credential";
                return RedirectToAction("Login");
            }
            return View(login);
        }
        [HttpGet]
        public ActionResult Register()
        {
            return View(new User());
        }
        [HttpPost]
        public ActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                PharmacyDBEntities db = new PharmacyDBEntities();
                user.created_at = DateTime.Now;
                user.updated_at = DateTime.Now;
                user.role = "Customer";

                db.Users.Add(user);
                db.SaveChanges();

                

                return RedirectToAction("Login");
            }
            return View(user);
        }

        public ActionResult Signout()
        {
            FormsAuthentication.SignOut();
            Session.Clear();
            return RedirectToAction("Login");
        }
    }
}