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
    public class ResultList
    {
        public Nullable <int> X { get; set; }
        public Nullable <int> Y { get; set; }
        public Nullable <int> Result_X { get; set; }
        public Nullable  <int> Result_Y { get; set; }
    }
    public class RubricsController : Controller
    {
        private FinalCapstoneEntities db = new FinalCapstoneEntities();

        // GET: Rubrics
        public ActionResult Index(int? id)
        {
            var rubrics = db.Rubrics.Include(r => r.Course);
            List<Rubric> lstRubric = db.Rubrics.Where(x => x.course_id == id).ToList();
            ViewBag.CourseId = id;
            //List<Rubric> weight = new List<Rubric>();
            //var Result = new List<ResultList>();
            //for (int i = 0; i<weight.Count; i++)
            //{
            //    Result.Add(new ResultList { X = weight[i].weight });
            //}
            //Result.Add(new ResultList { Result_X = weight.Sum(X => X.weight) });

            //ViewBag.test = Result;
            return View(lstRubric);          
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
        public ActionResult Create(int? id)
        {
            // List<Rubric> lstRubric = db.Rubrics.Where(x => x.course_id == id).ToList();
            ViewBag.CourseId = id;
            TempData["cid"] = ViewBag.CourseId;
            return View();
        }

        // POST: Rubrics/Create       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(RubricCreation model)
        {
            model.course_id =(int)TempData["cid"];
            Rubric rub = new Rubric();
            rub.outputs = model.outputs;
            rub.weight = model.weight;
            rub.course_id = model.course_id;
            db.Rubrics.Add(rub);
            db.SaveChanges();

            return RedirectToAction("Index", new { id = model.course_id});         
        }

        // GET: Rubrics/Edit/5
        public ActionResult Edit(int? id, int? courseid)
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
            int? cid = courseid;
            ViewBag.CourseId = cid;
            ViewBag.course_id = new SelectList(db.Courses, "course_id", "courseno", rubric.course_id);
            return View(rubric);
        }

        // POST: Rubrics/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(RubricEdit modelRubric, int? courseid)
        {
            int? cid = courseid;
            if (ModelState.IsValid)
            {
                var rub = db.Rubrics.Find(modelRubric.rubric_id);
                rub.course_id = cid;
                rub.outputs = modelRubric.outputs;
                rub.weight = modelRubric.weight;
                
                db.SaveChanges();
                return RedirectToAction("Index", new { id = cid });
            }
          
            ViewBag.course_id = new SelectList(db.Courses, "course_id", "courseno", modelRubric.course_id);
            return RedirectToAction("Index", new { id = cid });
        }

        // GET: Rubrics/Delete/5
        public ActionResult Delete(int? id , int? courseid)
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
            int? cid = courseid;
            ViewBag.test = cid;
            ViewData["test"] = ViewBag.test;
            return View(rubric);
        }

        // POST: Rubrics/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id, int? courseid )
        {
            int? cid = courseid;
            Rubric rubric = db.Rubrics.Find(id);          
            db.Rubrics.Remove(rubric);
            db.SaveChanges();
          
            return RedirectToAction("Index", new {id =cid});
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
