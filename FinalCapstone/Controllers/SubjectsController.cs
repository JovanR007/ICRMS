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
    public class SubjectsController : Controller
    {
        private FinalCapstoneEntities db = new FinalCapstoneEntities();

        // GET: Subjects
        public ActionResult Index()
        {
            var subjects = db.Subjects.Include(s => s.Course);
            return View(subjects.ToList());
        }

        // GET: Subjects/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Subject subject = db.Subjects.Find(id);
            if (subject == null)
            {
                return HttpNotFound();
            }
            return View(subject);
        }

        // GET: Subjects/Create
        public ActionResult Create()
        {
            ViewBag.course_id = new SelectList(db.Courses, "course_id", "courseno");
            return View();
        }

        // POST: Subjects/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SubjectCreation model)
        {
            Course course = new Course();
            course.courseno = model.courseno;
            course.coursetitle = model.coursetitle;
            course.coursedescription = model.coursedescription;
            db.Courses.Add(course);
            db.SaveChanges();
            int newcourseid = course.course_id;

            Subject subj = new Subject();
            subj.course_id = newcourseid;
            db.Subjects.Add(subj);
            db.SaveChanges();


           
            return RedirectToAction("Index");
        }

        // GET: Subjects/Edit/5
        public ActionResult Edit(int? id, int courseid)
        {

            var cid = courseid;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
          //  Subject subject = db.Subjects.Find(id);
            var subject = db.Subjects.Find(id);
            var viewModel = new SubjectViewModel { subject_id = subject.subject_id,course_id = subject.course_id, courseno = subject.Course.courseno , coursetitle = subject.Course.coursetitle,coursedescription = subject.Course.coursedescription };
            if (viewModel == null)
            {
                return HttpNotFound();
            }
            ViewBag.course_id = new SelectList(db.Courses, "course_id", "courseno", subject.Course.courseno);
            
            return View(viewModel);
        }

        // POST: Subjects/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "subject_id,course_id")] SubjectViewModel subject, int courseid)
        //{

        //    if (ModelState.IsValid)
        //    {
        //        var sject = db.Subjects.Find(subject.subject_id);
        //        sject.course_id = subject.course_id;


        //       var scourse =  db.Courses.Find(subject.course_id);
        //        scourse.courseno = subject.courseno;
        //        scourse.coursetitle = subject.coursetitle;
        //        scourse.coursedescription = subject.coursedescription;

        //        db.SaveChanges();

        //        return RedirectToAction("Index");
        //    }



        //    return View(subject);
        //}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SubjectViewModel subject)
        {

            if (ModelState.IsValid)
            {
                var sject = db.Subjects.Find(subject.subject_id);
                subject.course_id = sject.course_id;


                var scourse = db.Courses.Find(subject.course_id);
                scourse.courseno = subject.courseno;
                scourse.coursetitle = subject.coursetitle;
                scourse.coursedescription = subject.coursedescription;

                db.SaveChanges();

                return RedirectToAction("Index");
            }



            return View(subject);
        }






        // GET: Subjects/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Subject subject = db.Subjects.Find(id);
            if (subject == null)
            {
                return HttpNotFound();
            }
            return View(subject);
        }

        // POST: Subjects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Subject subject = db.Subjects.Find(id);
            db.Subjects.Remove(subject);
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
