using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FinalCapstone;

namespace FinalCapstone.Controllers
{
    public class RubricsController : Controller
    {
        private FinalCapstoneEntities db = new FinalCapstoneEntities();

        // GET: Rubrics
        public ActionResult Index()
        {
            var rubrics = db.Rubrics.Include(r => r.Course);
            return View(rubrics.ToList());
        }

        // GET: Rubrics/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rubric rubric = db.Rubrics.Find(id);
            if (rubric == null)
            {
                return HttpNotFound();
            }
            return View(rubric);
        }

        // GET: Rubrics/Create
        public ActionResult Create()
        {
          
            return View();
        }

        // POST: Rubrics/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public JsonResult Create(List<Rubric> rubrics)
        {
            //Check for NULL.
            if (rubrics == null)
            {
                rubrics = new List<Rubric>();
                Rubric rb = new Rubric();
                rb.outputs = "1";
                rb.weight = 1;
                rb.course_id = 6;
                db.Rubrics.Add(rb);
            }
            //Loop and insert records.
            foreach (Rubric rubric in rubrics)
            {
               
                db.Rubrics.Add(rubric);


            }
            int insertedRecords = db.SaveChanges();



           
            return Json(insertedRecords);
        }

        // GET: Rubrics/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rubric rubric = db.Rubrics.Find(id);
            if (rubric == null)
            {
                return HttpNotFound();
            }
            ViewBag.course_id = new SelectList(db.Courses, "course_id", "courseno", rubric.course_id);
            return View(rubric);
        }

        // POST: Rubrics/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "rubric_id,outputs,weight,course_id")] Rubric rubric)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rubric).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.course_id = new SelectList(db.Courses, "course_id", "courseno", rubric.course_id);
            return View(rubric);
        }

        // GET: Rubrics/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rubric rubric = db.Rubrics.Find(id);
            if (rubric == null)
            {
                return HttpNotFound();
            }
            return View(rubric);
        }

        // POST: Rubrics/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Rubric rubric = db.Rubrics.Find(id);
            db.Rubrics.Remove(rubric);
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
