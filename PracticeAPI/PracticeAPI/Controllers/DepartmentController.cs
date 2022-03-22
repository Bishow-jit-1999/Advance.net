using PracticeAPI.Models;
using PracticeAPI.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PracticeAPI.Controllers
{
    public class DepartmentController : ApiController
    {
        [Route("api/get/department")]
        [HttpGet]
        public HttpResponseMessage GetALL()
        {

            TASKEntities db = new TASKEntities();
            var dept = db.Departments.ToList();
            return Request.CreateResponse(HttpStatusCode.OK, dept);
        }
        [Route("api/add/dept")]
        [HttpPost]
        public HttpResponseMessage Add(Department Data)
        {

            if (ModelState.IsValid)
            {
                TASKEntities db = new TASKEntities();
                db.Departments.Add(Data);
                db.SaveChanges();
            }
            return Request.CreateResponse(HttpStatusCode.OK, "Added");
        }

        [Route("api/edit/department/{id})")]
        [HttpGet]
        public HttpResponseMessage Edit(int id)
        {
            TASKEntities db = new TASKEntities();
            var edept = (from u in db.Departments where u.Id == id select u).FirstOrDefault();
            return Request.CreateResponse(HttpStatusCode.OK, edept);
        }

        [Route("api/delete/department/{id}")]
        [HttpDelete]
        public HttpResponseMessage Delete(int id)
        {

            TASKEntities db = new TASKEntities();
            var deldept= (from u in db.Departments where u.Id == id select u).FirstOrDefault();
            db.Departments.Remove(deldept);
            db.SaveChanges();
            return Request.CreateResponse(HttpStatusCode.OK, deldept);
        }

        [Route("api/dept/details/{id}")]
        [HttpGet]
        public HttpResponseMessage Departmentstu(int id)
        {
            TASKEntities db = new TASKEntities();
            var DATA=(from u in db.Departments join d in db.Students on u.Id equals d.Dept_id where u.Id==id  select u).FirstOrDefault();

           


            return Request.CreateResponse(HttpStatusCode.OK, DATA);
        }
    }
}
