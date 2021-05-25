using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FinalCapstone;
using FinalCapstone.Models;

namespace FinalCapstone.Controllers
{
    public class ProgramsController : Controller
    {
        private FinalCapstoneEntities db = new FinalCapstoneEntities();
        public class RetJson
        {
            public int status;
            public string message;
        }
        // GET: Programs
        public ActionResult Index()
        {
            return View(db.Programs.ToList());
        }

        
        // GET: Programs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Programs/Create        
        [HttpPost]      
        public JsonResult Create([Bind(Include = "program_id,program1,program_title,program_description")] Program program)
        {
            RetJson a = new RetJson();

           
            if (ModelState.IsValid)
            {
                var chk1 = (from x in db.Programs where x.program1 == program.program1 select x).FirstOrDefault();

                if (chk1 == null)
                {
                    db.Programs.Add(program);
                    db.SaveChanges();
                    a.status = 0;
                    a.message = "success";

                }
                else
                {
                    a.status = 1;
                    a.message = "Program already exists";

                }
               
            }

            return Json(a,JsonRequestBehavior.AllowGet); ;
        }

        // GET: Programs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Program program = db.Programs.Find(id);
            if (program == null)
            {
                return HttpNotFound();
            }
            return View(program);
        }

        // POST: Programs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "program_id,program1,program_title,program_description")] Program program)
        {
            if (ModelState.IsValid)
            {
                db.Entry(program).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(program);
        }

        // GET: Programs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Program program = db.Programs.Find(id);
            if (program == null)
            {
                return HttpNotFound();
            }
            return View(program);
        }

        // POST: Programs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Program program = db.Programs.Find(id);
            db.Programs.Remove(program);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
