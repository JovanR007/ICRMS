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
            ViewBag.user_id = new SelectList(db.Users.Where(g=>g.role_id == 2), "user_id", "f_name" );
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ClassCreation model)
        {
            var chk1 = (from x in db.Subjects where x.course_id == model.course_id select x).FirstOrDefault();
            var chk2 = (from y in db.Subjects where y.course_id == model.course_id select y).FirstOrDefault();

            ViewBag.value = DateTime.Now;

                Classroom classroom = new Classroom();
                classroom.room_number = model.room_number;
                classroom.capacity = 0;
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
            var a = db.Classes.Find(id);
           // Class @class = db.Classes.Find(id);
            var ClassModel = new ClassViewModel {class_id=a.class_id,subject_id = a.subject_id,classroom_id = a.classroom_id, user_id = a.user_id, schedule_id = a.schedule_id,group_no = a.group_no, time_start=a.Schedule.time_start, time_end= a.Schedule.time_end,day=a.Schedule.day,room_number=a.Classroom.room_number};
            if (a == null)
            {
                return HttpNotFound();
            }
            ViewBag.classroom_id = new SelectList(db.Classrooms, "classroom_id", "room_number", a.classroom_id);          
            ViewBag.subject_id = new SelectList(db.Subjects, "subject_id", "subject_name", a.subject_id);
            ViewBag.course_id = new SelectList(db.Courses, "course_id", "courseno", a.Subject.course_id);
            ViewBag.user_id = new SelectList(db.Users, "user_id", "f_name", a.user_id);
            return View(ClassModel);
        }

        // POST: Classes/Edit/5       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ClassViewModel model, int? id)
        {
            if (ModelState.IsValid)
            {
                var a = db.Classes.Find(id);
                var ClassModel = new ClassViewModel { class_id = a.class_id, subject_id = a.subject_id, classroom_id = a.classroom_id, user_id = a.user_id, schedule_id = a.schedule_id, group_no = a.group_no, time_start = a.Schedule.time_start, time_end = a.Schedule.time_end, day = a.Schedule.day, room_number = a.Classroom.room_number };
                var chk1 = (from x in db.Subjects where x.course_id == model.course_id select x).FirstOrDefault();
                
                var cls = db.Classes.Find(model.class_id);              
                cls.group_no = model.group_no;            
                cls.subject_id = chk1.subject_id;
                var classroom = db.Classrooms.Find(ClassModel.classroom_id);
                classroom.classroom_id = (int)ClassModel.classroom_id ;
                classroom.room_number = model.room_number;
                var sched = db.Schedules.Find(ClassModel.schedule_id);
                sched.time_start = model.time_start;
                sched.time_end = model.time_end;
                sched.day = ClassModel.day;
                var user = db.Users.Find(model.user_id);
                user.user_id = (int)model.user_id;
                db.SaveChanges();
                return RedirectToAction("Index");

            }
            ViewBag.classroom_id = new SelectList(db.Classrooms, "classroom_id", "room_number", model.classroom_id);
            ViewBag.schedule_id = new SelectList(db.Schedules, "schedule_id", "day", model.schedule_id);
            ViewBag.subject_id = new SelectList(db.Subjects, "subject_id", "subject_name", model.subject_id);
            ViewBag.course_id = new SelectList(db.Courses, "course_id", "courseno", model.course_id);
            ViewBag.user_id = new SelectList(db.Users, "user_id", "f_name", model.user_id);
            return View(model);
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
