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
    public class ClassListsController : Controller
    {
        private FinalCapstoneEntities db = new FinalCapstoneEntities();

        // GET: ClassLists
        public ActionResult Index()
        {
            var classLists = db.ClassLists.Include(c => c.User);
            return View(classLists.ToList());
        }

        // GET: ClassLists/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClassList classList = db.ClassLists.Find(id);
            if (classList == null)
            {
                return HttpNotFound();
            }
            return View(classList);
        }

        // GET: ClassLists/Create
        public ActionResult Create()
        {
            ViewBag.classlist_id = new SelectList(db.Users, "user_id", "f_name");
            return View();
        }

        // POST: ClassLists/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "classlist_id,user_id")] ClassList classList)
        {
            if (ModelState.IsValid)
            {
                db.ClassLists.Add(classList);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.classlist_id = new SelectList(db.Users, "user_id", "f_name", classList.classlist_id);
            return View(classList);
        }

        // GET: ClassLists/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClassList classList = db.ClassLists.Find(id);
            //classList.group_no = classList.group_no;
            Course courses = db.Courses.Find(id);
            if (classList == null)
            {
                return HttpNotFound();
            }

            ViewBag.classlist_id = classList.classlist_id;
            ViewBag.user_id = new SelectList(db.Users, "user_id", "f_name", classList.user_id);
            ViewBag.course_id = new SelectList(db.Courses, "course_id", "courseno", classList.Class.Subject.Course.course_id);
            return View(classList);
        }

        // POST: ClassLists/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "user_id,class_id,classlist_id,group_no")] ClassList classList)
        {
            if (ModelState.IsValid)
            {
                db.Entry(classList).State = EntityState.Modified;

                //var dbclass = db.Classes.Find(classList.class_id);
                //dbclass.group_no = classList.group_no;

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.classlist_id = new SelectList(db.Users, "user_id", "f_name", classList.classlist_id);
            ViewBag.classlist_id = new SelectList(db.Users, "user_id", "f_name");
            return View(classList);
        }

        // GET: ClassLists/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClassList classList = db.ClassLists.Find(id);
            if (classList == null)
            {
                return HttpNotFound();
            }
            return View(classList);
        }

        // POST: ClassLists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ClassList classList = db.ClassLists.Find(id);
            db.ClassLists.Remove(classList);
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

        public ActionResult StudentEnrollment()
        {
            ViewBag.user_id = new SelectList(db.Users, "user_id", "idnumber");
            ViewBag.subject_id = new SelectList(db.Subjects, "subject_id", "subject_name");
            ViewBag.course_id = new SelectList(db.Courses, "course_id", "courseno");
            return View();
        }
        [HttpPost]
        public ActionResult StudentEnrollment(StudentEnrollment model)
        {
            List <Class> classes = db.Classes.ToList();
            var chk = (from y in db.Subjects where y.course_id == model.course_id select y).FirstOrDefault(); 
          
            var chk1 = (from x in db.Classes where x.subject_id == chk.subject_id && x.group_no == model.group_no select x).FirstOrDefault();
            if (chk1 != null)
            {
                

                ClassList clist = new ClassList();
                clist.user_id = model.user_id;
                clist.class_id = chk1.class_id;
                clist.classlist_id = chk1.class_id;
                db.ClassLists.Add(clist);
                db.SaveChanges();



            }




            ViewBag.user_id = new SelectList(db.Users, "user_id", "idnumber", model.user_id);
            ViewBag.subject_id = new SelectList(db.Subjects, "subject_id", "subject_name", model.subject_id);
            ViewBag.course_id = new SelectList(db.Courses, "course_id", "courseno", model.course_id);
            return RedirectToAction("Index","ClassLists");
        }
    }
}
