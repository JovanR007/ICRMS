using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FinalCapstone.Models;

namespace FinalCapstone.Controllers
{
    
    public class LoginController : Controller
    {
        //private FinalCapstoneEntities db = new FinalCapstoneEntities();
        // GET: Login
        public ActionResult login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult login(User user)
        {
            LoginModel login = new LoginModel();
            var userLoggedIn = login.GetLogin(user.idnumber.ToString(), user.password.ToString());
            //var userLoggedIn = db.Users.FirstOrDefault(x => x.idnumber == user.idnumber && x.password == user.password);

            if (userLoggedIn != null)
            {
                if (userLoggedIn.role_id > 0)
                {
                    ViewBag.Message = "logged in";
                    ViewBag.triedOnce = "Yes";
                    Session["id_number"] = user.idnumber;

                    if (userLoggedIn.role_id == 1)
                        return RedirectToAction("AdminIndex", "Login", new { id_number = user.idnumber });
                    else if (userLoggedIn.role_id == 2)
                        return RedirectToAction("TeacherIndex", "Login", new { id_number = user.idnumber });
                    else if (userLoggedIn.role_id == 3)
                        return RedirectToAction("StudentIndex", "Login", new { id_number = user.idnumber });
                }
            }
            return View();
        }
       
        public ActionResult AdminIndex(string id_number)
        {
            if (Session["id_number"] == null)
            {
                return RedirectToAction("login", "Login");
            }
            else
            {
                ViewBag.idnumber = Session["id_number"];
                return View();
            }
        }
        public ActionResult TeacherIndex(string id_number)
        {
            if (Session["id_number"] == null)
            {
                return RedirectToAction("login", "Login");
            }
            else
            {
                ViewBag.idnumber = Session["id_number"];
                return View();
            }
        }
        public ActionResult StudentIndex(string id_number)
        {
            if (Session["id_number"] == null)
            {
                return RedirectToAction("login", "Login");
            }
            else
            {
                ViewBag.idnumber = Session["id_number"];
                return View();
            }
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("login", "Login");
        }
    }
}
