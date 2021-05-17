using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Mvc;
using System.Net;
using FinalCapstone.Models;
using Dapper;

namespace FinalCapstone.Controllers
{
    public class TeacherClassesController : Controller
    {
        IDbConnection _db = new SqlConnection(ConfigurationManager.ConnectionStrings["Capstone"].ConnectionString);

        private FinalCapstoneEntities db = new FinalCapstoneEntities();
        public TeacherClassesController()
        {

        }

        // GET: TeacherClass
        public ActionResult Index(int? id, int? role)
        {
            ViewBag.test = id;
            ViewBag.idnumber = id;
            ViewBag.roleid = role;
            TempData["userlogin"] = id;
            TempData["roleid"] = role;
           ViewBag.fullname = Session["fullname"];
            TeacherClassesModel classes = new TeacherClassesModel();
            var x = classes.GetClassesByUser(id, role);
            return View(x);
        }
        

        public ActionResult ClassDetails(int? classid, string coursetitle, int? userlogin, int? role)
        {

            ViewBag.idnumber = TempData["userlogin"];
            TempData.Keep("userlogin");
            ViewBag.fullname = Session["fullname"];
            ViewData["classid"] = classid;
            ViewBag.classid = ViewData["classid"];
            ViewData["coursetitle"] = coursetitle;
            ViewData["userlogin"] = userlogin;
            ViewBag.roleid = role;
            #region //Entity
            #endregion
            TeacherClassesModel classes = new TeacherClassesModel();
            
            var x = classes.GetClassDetailsPivot(classid, userlogin, role);
            
            return View(x);
        }

        public JsonResult GetStudentAttendance(int? idnumber, int? classid)
        {
            var x = new TeacherClassesModel().GetStudentAttendanceDetails(idnumber, classid);
            return Json(x, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetFinalGradeByUser(int? idnumber, int? classid)
        {
            var x = new TeacherClassesModel().GetFinalGradeByUserDetails(idnumber, classid);
            return Json(x, JsonRequestBehavior.AllowGet);
        }
        public JsonResult InsertGrade(int? class_id, int? examtype_id, int? user_id, string grade, int? userlogin)
        {
            var x = new TeacherClassesModel().InsertGradeData(class_id, examtype_id, user_id, grade, userlogin);
            return Json(x, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetExamTypeByUser(int? idnumber, int? classid)
        {
            var x = new TeacherClassesModel().GetExamTypeByUserDetails(idnumber, classid);
            return Json(x, JsonRequestBehavior.AllowGet);
        }

         #region Class  List Details
        public ActionResult ClassListDetails(int? classid, string coursetitle, int? userlogin)
        {
            
            ViewData["classid"] = classid;
            ViewData["coursetitle"] = coursetitle;
            ViewData["userlogin"] = userlogin;
            ViewBag.fullname = Session["fullname"];
            ViewBag.roleid = TempData["roleid"];
            ViewBag.idnumber = ViewData["userlogin"];
            TeacherClassesModel classes = new TeacherClassesModel();
            var x = classes.GetClassListDetails(classid);
            return View(x);
        }

        public JsonResult DeleteClassList(int? user_id, int? classlist_id)
        {
            var x = new TeacherClassesModel().deleteClassList(user_id, classlist_id);
            return Json(x, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetUserList(int? userid)
        {
            var x = new TeacherClassesModel().GetUserDetails(userid);
            return Json(x, JsonRequestBehavior.AllowGet);
        }

        public JsonResult AddStudent(int? class_id, int? user_id, string userlogin)
        {
            var x = new TeacherClassesModel().InsertClassList(class_id, user_id, userlogin);
            return Json(x, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Exam Type Details
        public ActionResult ExamListDetails(int? classid, string coursetitle, int? userlogin)
        {
            ViewBag.idnumber = TempData["userlogin"];
            ViewBag.fullname = Session["fullname"];
            ViewData["classid"] = classid;
            ViewData["coursetitle"] = coursetitle;
            ViewData["userlogin"] = userlogin;

            ViewBag.roleid = TempData["roleid"];
            ViewBag.idnumber = ViewData["userlogin"];
            var x = new TeacherClassesModel().GetExamList(classid);
            return View(x);
        }

        public JsonResult AddExamType(int? classid, int? examtypeid)
        {
            var x = new TeacherClassesModel().GetExamTypes(classid, examtypeid);
            return Json(x, JsonRequestBehavior.AllowGet);
        }

        public JsonResult InsertExamList(int? classid, int? examtypeid, int? perfectscore,  bool islock, string userlogin)
        {
            var x = new TeacherClassesModel().InsertDataExamList(classid, examtypeid, perfectscore, islock, userlogin);
            return Json(x, JsonRequestBehavior.AllowGet);
        }

  
        public JsonResult DeleteExamList(int? examlist_id, int? classid, int? examtype_id)
        {
            var x = new TeacherClassesModel().DeleteExamList(examlist_id, classid, examtype_id);
            return Json(x, JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
}