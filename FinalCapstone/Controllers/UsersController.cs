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
    public class UsersController : Controller
    {
        private FinalCapstoneEntities db = new FinalCapstoneEntities();
        public class RetJson
        {
            public int status;
            public string message;
        }
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
        [HttpPost]
        public JsonResult Create([Bind(Include = "user_id,f_name,l_name,idnumber,password,role_id,program_id,batch_id")] User user)
        {
            RetJson a = new RetJson();


            if (ModelState.IsValid)
            {
                var chk1 = (from x in db.Users where x.idnumber == user.idnumber select x).FirstOrDefault();
               
                if (chk1 == null)
                {
                    db.Users.Add(user);
                    db.SaveChanges();
                    a.status = 0;
                    a.message = "success";

                }

                else
                {
                    a.status = 1;
                    a.message = "ID Number already exists";
                    
                }

                
            }

            ViewBag.batch_id = new SelectList(db.Batches, "batch_id", "batch1", user.batch_id);         
            ViewBag.program_id = new SelectList(db.Programs, "program_id", "program1", user.program_id);
            ViewBag.role_id = new SelectList(db.Roles, "role_id", "user_type", user.role_id);
            return Json(a,JsonRequestBehavior.AllowGet);
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            var a = db.Users.Find(id);
            var UserModel = new UserEditModel { idnumber = user.idnumber, f_name = user.f_name, l_name = a.l_name, password = a.password, role_id = a.role_id, batch_id = a.batch_id, program_id = a.program_id};
            if (user == null)
            {
                return HttpNotFound();
            }
            ViewBag.batch_id = new SelectList(db.Batches, "batch_id", "batch1", UserModel.batch_id);           
            ViewBag.program_id = new SelectList(db.Programs, "program_id", "program1", UserModel.program_id);
            ViewBag.role_id = new SelectList(db.Roles, "role_id", "user_type", UserModel.role_id);
            return View(UserModel);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UserEditModel UserModel, int? id )
        {
           
            if (ModelState.IsValid)
            {
                var a = db.Users.Find(id);
                a.f_name = UserModel.f_name;
                a.l_name = UserModel.l_name;
                a.password = UserModel.password;
                a.role_id = UserModel.role_id;
                a.program_id = UserModel.program_id;
                a.batch_id = UserModel.batch_id;
                a.idnumber = a.idnumber;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.batch_id = new SelectList(db.Batches, "batch_id", "batch1", UserModel.batch_id);
            ViewBag.program_id = new SelectList(db.Programs, "program_id", "program1", UserModel.program_id);
            ViewBag.role_id = new SelectList(db.Roles, "role_id", "user_type", UserModel.role_id);
            return RedirectToAction("Index");
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
