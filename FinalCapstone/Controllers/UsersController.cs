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
    public class UsersController : Controller
    {
        private FinalCapstoneEntities db = new FinalCapstoneEntities();

        // GET: Users
        public ActionResult Index(string sortOrder, string searchString)
        {
            var userz = from s in db.Users
                        select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                userz = userz.Where(s => s.f_name.Contains(searchString)
                                       || s.l_name.Contains(searchString));
                                       
                                     
            }


            //var users = db.Users.Include(u => u.Batch).Include(u => u.ClassLists).Include(u => u.Program).Include(u => u.Role).ToList();
            return View(userz);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            ViewBag.batch_id = new SelectList(db.Batches, "batch_id", "batch1");          
            ViewBag.program_id = new SelectList(db.Programs, "program_id", "program1");
            ViewBag.role_id = new SelectList(db.Roles, "role_id", "user_type");
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "user_id,f_name,l_name,idnumber,password,role_id,program_id,batch_id")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.batch_id = new SelectList(db.Batches, "batch_id", "batch1", user.batch_id);         
            ViewBag.program_id = new SelectList(db.Programs, "program_id", "program1", user.program_id);
            ViewBag.role_id = new SelectList(db.Roles, "role_id", "user_type", user.role_id);
            return View(user);
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            ViewBag.batch_id = new SelectList(db.Batches, "batch_id", "batch1", user.batch_id);           
            ViewBag.program_id = new SelectList(db.Programs, "program_id", "program1", user.program_id);
            ViewBag.role_id = new SelectList(db.Roles, "role_id", "user_type", user.role_id);
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "user_id,f_name,l_name,idnumber,password,role_id,program_id,batch_id")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.batch_id = new SelectList(db.Batches, "batch_id", "batch1", user.batch_id);
            ViewBag.program_id = new SelectList(db.Programs, "program_id", "program1", user.program_id);
            ViewBag.role_id = new SelectList(db.Roles, "role_id", "user_type", user.role_id);
            return View(user);
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
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
