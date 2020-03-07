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
        // GET: TeacherClass
        public ActionResult Index(int? id)
        {
            TeacherClassesModel classes = new TeacherClassesModel();
            var x = classes.GetClassesByUser(id);
            #region //Entity
            //List<User> users = db.Users.ToList();
            //List<Class> classes = db.Classes.ToList();
            //List<Subject> subjects = db.Subjects.ToList();
            //List<Schedule> schedules = db.Schedules.ToList();
            //List<Classroom> classrooms = db.Classrooms.ToList();
            //ViewData["TeacherClass"] = from cls in classes
            //                           join use in users on cls.user_id equals use.user_id
            //                           join subj in subjects on cls.subject_id equals subj.subject_id
            //                           join sched in schedules on cls.schedule_id equals sched.schedule_id
            //                           join room in classrooms on cls.classroom_id equals room.classroom_id

            //                           where id == use.idnumber
            //                           select new TeacherClass
            //                           {
            //                               user = use,
            //                               subj = subj,
            //                               room = room,
            //                               cls = cls,
            //                               sched = sched
            //                           };
            #endregion
            ViewBag.test = id;
            return View(x);
        }

        public ActionResult ClassDetails(int? classid, string coursetitle, int? userlogin)
        {
            //var chk = (from x in db.Classes where x.class_id == id2 select x).FirstOrDefault();
            //var chk1 = (from y in db.ClassLists where y.class_id == id2 select y).FirstOrDefault();
            ViewData["classid"] = classid;
            ViewBag.classid = ViewData["classid"];
            ViewData["coursetitle"] = coursetitle;
            ViewData["userlogin"] = userlogin;
            #region //Entity
            //ViewData["id2"] = id2;
            //ViewBag.id2 = ViewData["id2"];
            //TempData["idholder"] = ViewBag.id2;
            //List<ExamType> examtype = db.ExamTypes.ToList();
            //List<ClassList> classlist = db.ClassLists.ToList();
            //List<Class> classe = db.Classes.ToList();
            //List<User> user = db.Users.ToList();
            //List<ExamList> examlist = db.ExamLists.ToList();
            //List<ExamScore> examscore = db.ExamScores.ToList();
            //List<Rubric> rubric = db.Rubrics.ToList();
            ////  List<ExamList> examlist = db.ExamLists.ToList();
            ////ViewData["studtable"] = from exlist in examlist
            ////                        join ex in examtype on exlist.examtype_id equals ex.examtype_id into table1
            ////                        from ex in table1.DefaultIfEmpty()
            ////                        join slist in classlist on exlist.classlist_id equals slist.classlist_id into table2
            ////                        from slist in table2.DefaultIfEmpty()
            //ViewData["studtable"] = from slist in classlist
            //                        join cls in classe on slist.class_id equals cls.class_id into table1
            //                        from cls in table1.DefaultIfEmpty()
            //                        join use in user on slist.user_id equals use.user_id into table2
            //                        from use in table2.DefaultIfEmpty()
            //                        where id2 == slist.class_id
            //                        select new DisplayStudentTable
            //                        {
            //                            // examtype = ex,
            //                            classlist = slist,
            //                            // examlist = exlist,
            //                            user = use,
            //                            classe = cls,
            //                        };
            //ViewData["grade"] = from exlist in examlist
            //                    join extype in examtype on exlist.examtype_id equals extype.examtype_id into table1
            //                    from extype in table1.DefaultIfEmpty()
            //                    where id2 == exlist.class_id
            //                    select new DisplayStudentTable
            //                    {
            //                        examlist = exlist,
            //                        examtype = extype
            //                    };
            //ViewData["grade1"] = from exscore in examscore
            //                     join extype in examtype on exscore.examtype_id equals extype.examtype_id into table1
            //                     from extype in table1.DefaultIfEmpty()
            //                     join rub in rubric on exscore.ExamType.rubric_id equals rub.rubric_id into table2
            //                     from rub in table2.DefaultIfEmpty()
            //                     select new DisplayStudentTable
            //                     {
            //                         examtype = extype,
            //                         examscore = exscore,
            //                         rubric = rub
            //                     };
            //ArrayList myList = new ArrayList(11);
            //foreach (var item in ViewData["grade"] as IEnumerable<DisplayStudentTable>)
            //{
            //    myList.Add(item.examlist.ExamType.examtype_id);
            //}
            //ViewData["examtypeid"] = myList;
            #endregion
            TeacherClassesModel classes = new TeacherClassesModel();
            var x = classes.GetClassDetailsPivot(classid);
            //return Json(x, JsonRequestBehavior.AllowGet);
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

        //public ActionResult DeleteClassList(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    ClassList classList = db.ClassLists.Find(id);
        //    if (classList == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(classList);
        //}

        //public ActionResult AddExam(int? id)
        //{
        //    ViewBag.id2 = TempData["idholder"];
        //    ViewData["rub_id"] = new SelectList(db.Rubrics, "rubric_id", "outputs").ToList();
        //    TempData.Keep();
        //    var chk1 = (from x in db.Rubrics where x.course_id == id select x).FirstOrDefault();

        //    ViewData["id"] = chk1.rubric_id;
        //    ViewBag.id = ViewData["id"];
        //    List<Rubric> rubrics = db.Rubrics.ToList();
        //    ViewData["rubrictable"] = from c in rubrics
        //                              where id == c.course_id
        //                              select new ExamCreation
        //                              {
        //                                  rubric = c
        //                              };
        //    return View(ViewData);
        //}

        //public JsonResult AddExamType(List<ExamType> examtypes, int? id)
        //{
        //    int id2 = (int)TempData["idholder"];

        //    if (examtypes == null)
        //    {
        //        examtypes = new List<ExamType>();

        //    }
        //    ExamType et = new ExamType();
        //    //Loop and insert records.
        //    foreach (ExamType examtype in examtypes)
        //    {

        //        et = examtype;

        //    }
        //    db.ExamTypes.Add(et);
        //    db.SaveChanges();
        //    id = 1;
        //    int newexamtype = et.examtype_id;
        //    var chk1 = (from x in db.ClassLists where x.class_id == id2 select x).FirstOrDefault();
        //    ExamList el = new ExamList();
        //    el.examtype_id = newexamtype;
        //    el.class_id = chk1.class_id;
        //    db.ExamLists.Add(el);
        //    ExamScore es = new ExamScore();

        //    var chk2 = (from x in db.ClassLists where x.class_id == id2 select x).ToList();
        //    foreach (var x in chk2)
        //    {
        //        es.user_id = x.user_id;
        //        es.examtype_id = newexamtype;
        //        es.grade = 0;
        //        db.ExamScores.Add(es);
        //        db.SaveChanges();
        //    }
        //    int insertedRecords = db.SaveChanges();
        //    return Json(insertedRecords, JsonRequestBehavior.AllowGet);
        //}

        //public JsonResult CalculateGrade(List<ExamScore> examscores)
        //{
        //    //if(examscores == null)
        //    //{
        //    //    examscores = new List<ExamScore>();
        //    //}
        //    //ExamScore es = new ExamScore();
        //    //foreach (ExamScore examscore in examscores)
        //    //{

        //    //    es = examscore;

        //    //}
        //    //db.ExamScores.Add(es);
        //    //db.SaveChanges();


        //    int insertedRecords = db.SaveChanges();
        //    return Json(insertedRecords, JsonRequestBehavior.AllowGet);
        //}

        //public JsonResult UpdateGrade(List<ExamScore> examscores)
        //{
        //    //Loop and insert records.
        //    foreach (ExamScore exam in examscores)
        //    {
        //        bool bExists = db.ExamScores.Any(x => x.user_id == exam.user_id && x.examtype_id == exam.examtype_id && x.grade == exam.grade);
        //        if (!bExists)
        //        {
        //            db.ExamScores.Add(exam);
        //        }         
        //        else
        //        {
        //            //Update
        //        }          
        //    }
        //    int insertedRecords = db.SaveChanges();
        //    return Json(insertedRecords);
        //}

        #region Class  List Details
        public ActionResult ClassListDetails(int? classid, string coursetitle, int? userlogin)
        {
            ViewData["classid"] = classid;
            ViewData["coursetitle"] = coursetitle;
            ViewData["userlogin"] = userlogin;
            TeacherClassesModel classes = new TeacherClassesModel();
            var x = classes.GetClassListDetails(classid);
            return View(x);
        }

        public JsonResult DeleteClassList(int? classlist_id)
        {
            var x = new TeacherClassesModel().deleteClassList(classlist_id);
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
            ViewData["classid"] = classid;
            ViewData["coursetitle"] = coursetitle;
            ViewData["userlogin"] = userlogin;
            var x = new TeacherClassesModel().GetExamList(classid);
            return View(x);
        }

        public JsonResult AddExamType(int? examtypeid)
        {
            var x = new TeacherClassesModel().GetExamTypes(examtypeid);
            return Json(x, JsonRequestBehavior.AllowGet);
        }

        public JsonResult InsertExamList(int? classid, int? examtypeid, bool islock, string userlogin)
        {
            var x = new TeacherClassesModel().InsertDataExamList(classid, examtypeid, islock, userlogin);
            return Json(x, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteExamList(int? examlist_id)
        {
            var x = new TeacherClassesModel().DeleteExamList(examlist_id);
            return Json(x, JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
}