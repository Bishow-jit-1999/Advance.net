using NEWS_TASK.Models.Database;
using System.Linq;
using System.Web.Mvc;

namespace NEWS_TASK.Controllers
{
    public class NEWSController : Controller
    {
        // GET: NEWS
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AllBook()
        {
            NEWSDBEntities1 db = new NEWSDBEntities1();
            var data = db.Newsdatas.ToList();
            return View(data);

        }

        [HttpGet]
        public ActionResult Insert()
        {
            return View(new Newsdata());
        }

        [HttpPost]
        public ActionResult Insert(Newsdata A)
        {

            if (ModelState.IsValid)
            {
                NEWSDBEntities1 db = new NEWSDBEntities1();
                db.Newsdatas.Add(A);
                db.SaveChanges();
                return RedirectToAction("AllBOOk");
            }
            return View(A);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {

            if (ModelState.IsValid)
            {
                NEWSDBEntities1 db = new NEWSDBEntities1();
                var newsobj = (from u in db.Newsdatas where u.Id == id select u).FirstOrDefault();
                return RedirectToAction("Edit", newsobj);
            }
            return View();

        }

        public ActionResult Delete(int id)
        {
            if (ModelState.IsValid)
            {
                NEWSDBEntities1 db = new NEWSDBEntities1();
                var obj = (from u in db.Newsdatas where u.Id == id select u).FirstOrDefault();

            }
            return View();

        }
    }
}