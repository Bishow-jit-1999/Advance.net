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
    //[EnableCors("*","*","*")]
    public class StudentController : ApiController
    {
        [Route("api/get/student")]
        [HttpGet]
        public HttpResponseMessage GetALL()
        {
           
                TASKEntities db = new TASKEntities();
                var stu=db.Students.ToList();
             
            

            return Request.CreateResponse(HttpStatusCode.OK, stu);
        }
        [Route("api/add/student")]
        [HttpPost]
        public HttpResponseMessage Add(Student Data)
        {

            if (ModelState.IsValid)
            {
                TASKEntities db = new TASKEntities();
                db.Students.Add(Data);
                db.SaveChanges();
            }
            return Request.CreateResponse(HttpStatusCode.OK, "ADDED");
        }

        [Route("api/edit/student/{id}")]
        [HttpGet]
        public HttpResponseMessage Edit(int id)
        {

            
                TASKEntities db = new TASKEntities();
                var edata = (from u in db.Students where u.Id == id select u).FirstOrDefault();
            
            return Request.CreateResponse(HttpStatusCode.OK, edata);
        }

        [Route("api/delete/student/{id}")]
        [HttpDelete]
        public HttpResponseMessage Delete(int id)
        {
            TASKEntities db = new TASKEntities();
            var delstu= (from u in db.Students where u.Id == id select u).FirstOrDefault();
            db.Students.Remove(delstu);
            db.SaveChanges();
            return Request.CreateResponse(HttpStatusCode.OK, delstu);
        }
    }
}
