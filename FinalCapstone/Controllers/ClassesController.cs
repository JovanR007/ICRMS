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
    public class ClassesController : Controller
    {
        private FinalCapstoneEntities db = new FinalCapstoneEntities();

        // GET: Classes
        public ActionResult Index()
        {
            var classes = db.Classes.Include(a => a.Classroom).Include(a => a.Schedule).Include(a => a.Subject).Include(a => a.User);
            return View(classes.ToList());
        }     

        // GET: Classes/Create
        public ActionResult Create()
        {          
            ViewBag.course_id = new SelectList(db.Courses, "course_id", "courseno");
            ViewBag.user_id = new SelectList(db.Users, "user_id", "f_name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ClassCreation model)
        {
            var chk1 = (from x in db.Subjects where x.course_id == model.course_id select x).FirstOrDefault();
            
           
               

                Classroom classroom = new Classroom();
                classroom.room_number = model.room_number;
                db.Classrooms.Add(classroom);
                db.SaveChanges();
                int latestclassroom = classroom.classroom_id;

                Schedule sched = new Schedule();
                sched.time_start = model.time_start;
                sched.time_end = model.time_end;
                sched.day = model.day;
                db.Schedules.Add(sched);
                db.SaveChanges();
                int latestsched = sched.schedule_id;

                Class cls = new Class();
                cls.schedule_id = latestsched;
                cls.classroom_id = latestclassroom;
                cls.group_no = model.group_no;
                cls.user_id = model.user_id;
                cls.subject_id = chk1.subject_id;
                db.Classes.Add(cls);
                db.SaveChanges();

            ViewBag.course_id = new SelectList(db.Courses, "course_id", "courseno", model.course_id);
            ViewBag.user_id = new SelectList(db.Users, "user_id", "f_name", model.user_id);
            return RedirectToAction("Index","Classes");
        }

        // GET: Classes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Class @class = db.Classes.Find(id);
            if (@class == null)
            {
                return HttpNotFound();
            }
            ViewBag.classroom_id = new SelectList(db.Classrooms, "classroom_id", "room_number", @class.classroom_id);
            ViewBag.schedule_id = new SelectList(db.Schedules, "schedule_id", "day", @class.schedule_id);
            ViewBag.subject_id = new SelectList(db.Subjects, "subject_id", "subject_name", @class.subject_id);
            ViewBag.course_id = new SelectList(db.Subjects, "course_id", "courseno", @class.Subject.course_id);
            ViewBag.user_id = new SelectList(db.Users, "user_id", "f_name", @class.user_id);
            return View(@class);
        }

        // POST: Classes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "class_id,classroom_id,user_id,subject_id,schedule_id,group_no")] Class @class)
        {
            if (ModelState.IsValid)
            {
                db.Entry(@class).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.classroom_id = new SelectList(db.Classrooms, "classroom_id", "room_number", @class.classroom_id);
            ViewBag.schedule_id = new SelectList(db.Schedules, "schedule_id", "day", @class.schedule_id);
            ViewBag.subject_id = new SelectList(db.Subjects, "subject_id", "subject_name", @class.subject_id);
            ViewBag.user_id = new SelectList(db.Users, "user_id", "f_name", @class.user_id);
            return View(@class);
        }

        // GET: Classes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Class @class = db.Classes.Find(id);
            if (@class == null)
            {
                return HttpNotFound();
            }
            return View(@class);
        }

        // POST: Classes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Class @class = db.Classes.Find(id);
            db.Classes.Remove(@class);
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
