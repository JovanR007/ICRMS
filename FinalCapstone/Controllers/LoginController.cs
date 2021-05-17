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
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.UtcNow.AddHours(-1));
            Response.Cache.SetNoStore();
            return View();
        }

        [HttpPost]
        public ActionResult login(User user)
        {
            LoginModel login = new LoginModel();
            var userLoggedIn = login.GetLogin(user.idnumber.ToString(), user.password.ToString());

            if (userLoggedIn != null)
            {
                if (userLoggedIn.role_id > 0)
                {
                    ViewBag.Message = "logged in";
                    ViewBag.triedOnce = "Yes";
                    Session["id_number"] = user.idnumber;
                    Session["role_id"] = userLoggedIn.role_id;
                    Session["fullname"] = userLoggedIn.l_name + ", " + userLoggedIn.f_name;

                    if (userLoggedIn.role_id == 1)
                        return RedirectToAction("AdminIndex", "Login", new { id_number = user.idnumber });
                    else if (userLoggedIn.role_id == 2)
                        return RedirectToAction("TeacherIndex", "Login", new { id_number = user.idnumber, role = userLoggedIn.role_id, fullname= userLoggedIn.l_name + ", " + userLoggedIn.f_name });
                    else if (userLoggedIn.role_id == 3)
                        //return RedirectToAction("StudentIndex", "Login", new { id_number = user.idnumber });
                        return RedirectToAction("TeacherIndex", "Login", new { id_number = user.idnumber, role = userLoggedIn.role_id, fullname = userLoggedIn.l_name + ", " + userLoggedIn.f_name });
                }

            }
            else
            {
                TempData["message"] = "error";
                
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
        public ActionResult TeacherIndex(string id_number, int role, string fullname)
        {
            if (Session["id_number"] == null)
            {
                return RedirectToAction("login", "Login");
            }
            else
            {
                ViewBag.idnumber = Session["id_number"];
                ViewBag.roleid = Session["role_id"];
                ViewBag.fullname = Session["fullname"];
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
                ViewBag.roleid = Session["role_id"];
                ViewBag.fullname = Session["fullname"];
                return View();
            }
        }

        public ActionResult Logout()
        {
            Session.Clear();
            Session.Abandon();
            Session.RemoveAll();

            this.Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
            this.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            this.Response.Cache.SetNoStore();
            return RedirectToAction("login", "Login");
        }
    }
}
