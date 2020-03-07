using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml;
using Dapper;

namespace FinalCapstone.Models
{
    public class TeacherClassesModel
    {
        IDbConnection _db = new SqlConnection(ConfigurationManager.ConnectionStrings["Capstone"].ConnectionString);
        
        public TeacherClasses_Component GetClassesByUser(int? idno)
        {
            TeacherClasses_Component cls = new TeacherClasses_Component();
            string sql = @" SELECT COURSE.course_id as [courseid]
                            , CLASS.class_id as [classid]
                             , COURSE.courseno
                             , COURSE.coursetitle 
                             , ROOM.room_number as [roomnumber]
                             , FORMAT(SCHED.time_start,'hh:mm tt') as [starttime]
                             , FORMAT(SCHED.time_end,'hh:mm tt') as [endtime]
                             , SCHED.day
                             , CLASS.group_no as [groupno]
                             FROM dbo.Class CLASS
                             INNER JOIN dbo.[User] LOGINUSER ON CLASS.user_id = LOGINUSER.user_id
                             INNER JOIN dbo.[Subject] SUBJ ON CLASS.subject_id = SUBJ.subject_id
                             INNER JOIN dbo.[Course] COURSE ON COURSE.course_id = SUBJ.course_id
                             INNER JOIN dbo.Schedule SCHED ON CLASS.schedule_id = SCHED.schedule_id
                             INNER JOIN dbo.Classroom ROOM ON CLASS.classroom_id = ROOM.classroom_id  
                             WHERE LOGINUSER.idnumber = @idno";
            var result = _db.Query<TeacherClasses_Model>(sql, new { idno }).ToList();
            cls._data = result;
            return cls;
        }

        public ClassDetails_Component GetClassDetailsPivot(int? classid)
        {
            try
            {
                string sql = string.Format(
                             @" DECLARE @COLS       NVARCHAR(MAX)=''
                                DECLARE @QUERY      NVARCHAR(MAX)=''

                                SELECT @COLS = @COLS + QUOTENAME(EXAMLIST) + ',' 
                                FROM (
		                                SELECT CAST(ExamType.examtype_id AS VARCHAR) + '|' + examtype_name + '|' + CAST(perfect_score AS VARCHAR) + '|' + CAST(Rubric.[weight] AS VARCHAR) + '|' + CAST(Rubric.rubric_id AS VARCHAR) AS [EXAMLIST]
		                                FROM ExamList 
		                                INNER JOIN ExamType ON  ExamType.examtype_id = ExamList.examtype_id
                                        INNER JOIN Rubric ON Rubric.rubric_id = ExamType.rubric_id 
		                                WHERE ExamList.class_id = {0}
		                                UNION ALL
		                                SELECT '999998' + '|' + 'Total' + '| ' + '| ' + '| ' AS [EXAMLIST]
		                                UNION ALL
		                                SELECT '999999' + '|' + 'Final Grade' + '| '  + '| ' + '| ' AS [EXAMLIST]
		                                ) AS tmp

                                SELECT @COLS = SUBSTRING(@COLS, 0, LEN(@COLS))

                                SET @QUERY = 
                                'SELECT * FROM 
                                ( ' + 
	                                CAST('SELECT TMP.* FROM
	                                (
	                                SELECT [User].user_id, idnumber as [ID Number], l_name as [Last Name], f_name as [First Name], CAST(grade AS INT) as grade, CAST(ExamType.examtype_id AS VARCHAR) + ''|'' + examtype_name + ''|'' + CAST(perfect_score AS VARCHAR) + ''|'' + CAST(Rubric.[weight] AS VARCHAR) +''|''+ CAST(Rubric.rubric_id AS VARCHAR)   AS [EXAMLIST]
	                                FROM ClassList 
	                                INNER JOIN [User] ON [User].user_id = ClassList.user_id 
	                                INNER JOIN ExamScore ON ExamScore.user_id = [User].user_id
	                                INNER JOIN ExamType ON ExamType.examtype_id = ExamScore.examtype_id
	                                INNER JOIN Rubric ON Rubric.rubric_id = ExamType.rubric_id 
	                                WHERE CLASSLIST.class_id = {0}
	                                UNION ALL
	                                SELECT [User].user_id, idnumber as [ID Number], l_name as [Last Name], f_name as [First Name], CAST(totalgrade  AS INT), ''999998'' + ''|'' + ''TOTAL'' + ''| '' + ''| ''  AS [EXAMLIST]
	                                FROM ClassList
	                                INNER JOIN ExamFinalScore ON  ExamFinalScore.user_id = ClassList.user_id
		                                AND ExamFinalScore.class_id = ClassList.class_id
	                                INNER JOIN [User] ON [User].user_id = ExamFinalScore.user_id 
	                                WHERE ClassList.class_id = {0}
	                                UNION ALL
	                                SELECT [User].user_id, idnumber as [ID Number], l_name as [Last Name], f_name as [First Name], CAST(finalgrade  AS INT), ''999999'' + ''|'' + ''FINAL GRADE'' + ''| '' + ''| ''  AS [EXAMLIST]
	                                FROM ClassList
	                                INNER JOIN ExamFinalScore ON  ExamFinalScore.user_id = ClassList.user_id
		                                AND ExamFinalScore.class_id = ClassList.class_id
	                                INNER JOIN [User] ON [User].user_id = ExamFinalScore.user_id 
	                                WHERE ClassList.class_id = {0}
	                                ' AS  NVARCHAR(MAX)) +
	                                CAST(') TMP		 ) src 	PIVOT ( SUM(grade) FOR [EXAMLIST] IN (' + @COLS + ')) piv
	                                ORDER BY [Last Name], [First Name]' AS  NVARCHAR(MAX))
	
                                EXEC(@QUERY)", classid);
                var x = _db.Query<object>(sql).ToList();
                return x.Count != 0 ? new ClassDetails_Component { respcode = 1, message = "success", _data = x } : new ClassDetails_Component { respcode = 0, message = "No record(s) found." };
            }
            catch (Exception ex)
            {
                return new ClassDetails_Component { respcode = 0, message = ex.Message };
            }
        }

        public getClassListResponse GetClassListDetails(int? classid)
        {
            try
            {
                string sql = string.Format(@"SELECT [ClassList].classlist_id
                                                , [User].user_id 
                                                , idnumber 
                                                , l_name 
                                                , f_name 
                                             FROM [ClassList]
                                             INNER JOIN [User] on [User].user_id = ClassList.user_id
                                                 AND [User].role_id = 3
                                             WHERE class_id = @classid
                                             ORDER BY l_name, f_name

                                            SELECT[User].user_id
                                                , idnumber
                                                , l_name
                                                , f_name
                                             FROM [User]
                                             LEFT JOIN[ClassList] ON[User].user_id = ClassList.user_id
                                                AND ClassList.class_id = @classid
                                             WHERE [User].role_id = 3
                                                   AND ClassList.user_id IS NULL
                                             ORDER BY l_name, f_name");

                var x = _db.QueryMultiple(sql, new { classid });
                var clsList = x.Read<ClassList_Model>().ToList();
                var usrList = x.Read<ClassList_Model>().ToList();
                return new getClassListResponse
                {
                    respcode = 1,
                    message = "success",
                    _data = clsList,
                    _user = usrList
                };
                
            }
            catch (Exception ex)
            {
                return new getClassListResponse { respcode = 0, message = ex.Message };
            }
        }

        public Responses InsertGradeData(int? classid, int? examtypeid, int? userid, string grade, int? userlogin)
        {
            try
            {
                string sql = string.Format(@"UPDATE [ExamScore] SET grade = @grade
                                            , user_login = @userlogin
                                            , user_date = GETDATE()
                                             WHERE user_id = @userid
                                                AND examtype_id = @examtypeid
                                                AND class_id = @classid");

                
                var x = _db.Execute(sql, new { classid, examtypeid, userid, grade, userlogin });
                return new Responses { respcode = 1, message = "success" };

            }
            catch (Exception ex)
            {
                return new Responses { respcode = 0, message = ex.Message };
            }
        }

        public getClassListResponse GetUserDetails(int? userid)
        {
            try
            {
                string sql = string.Format(@"SELECT idnumber
                                                , l_name
                                                , f_name
                                             FROM[User]
                                             WHERE [User].user_id = @userid
                                            ");

                var x = _db.Query<ClassList_Model>(sql, new { userid }).ToList();
                return x.Count != 0 ? new getClassListResponse { respcode = 1, message = "success", _data = x } : new getClassListResponse { respcode = 0, message = "No record(s) found." };

            }
            catch (Exception ex)
            {
                return new getClassListResponse { respcode = 0, message = ex.Message };
            }
        }

        public Responses InsertClassList(int? class_id, int? user_id, string userlogin)
        {
            try
            {
                string sql = string.Format(@"INSERT INTO [ClassList] (user_id, class_id, user_login, user_date)
                                            VALUES(@user_id, @class_id, @userlogin, getdate()) ");
                var x = _db.Execute(sql, new { user_id, class_id, userlogin });
                return new Responses { respcode = 1, message = "success" };

            }
            catch (Exception ex)
            {
                return new Responses { respcode = 0, message = ex.Message };
            }
        }

        public Responses deleteClassList(int? classlist_id)
        {
            try
            {
                string sql = string.Format(@" DELETE FROM [ClassList]
                                              WHERE [ClassList].classlist_id = @classlist_id");
                var x = _db.Execute(sql, new { classlist_id });

                return new Responses { respcode = 1, message = "success" };
            }
            catch (Exception ex)
            {
                return new Responses { respcode = 0, message = ex.Message };
            }
        }

        public getAttendanceResponse GetStudentAttendanceDetails(int? idnumber, int? classid)
        {
            try
            {
                string sql = @" SELECT CONVERT(DATE, attendance_date) AS [Date]
                                , CASE WHEN status = 1 THEN 'Yes' ELSE 'No' END AS [Status]
                                FROM [AttendanceList]
                                INNER JOIN [User] ON [User].user_id = attendanceList.user_id
                                    AND [User].user_id = @idnumber
                                WHERE class_id = @classid
                                ORDER BY 1";
                var x = _db.Query<Attendance_Model>(sql, new { idnumber, classid }).ToList();
                return x.Count != 0 ? new getAttendanceResponse { respcode = 1, message = "success", _res = x } : new getAttendanceResponse { respcode = 0, message = "No record(s) found." };
            }
            catch (Exception ex)
            {
                return new getAttendanceResponse { respcode = 0, message = ex.Message };
            }
        }

        public getExamTypeResponse GetExamTypeByUserDetails(int? idnumber, int? classid)
        {
            try
            {
                string sql = @" SELECT [ExamType].examtype_id
                                ,examtype_name		
                                FROM [ExamType]					
                                INNER JOIN [ExamScore] on [ExamType].examtype_id = [ExamScore].examtype_id	
	                                AND class_id = @classid
	                                AND [ExamScore].user_id = @idnumber	";
                var x = _db.Query<ExamList_Model>(sql, new { idnumber, classid }).ToList();
                return x.Count != 0 ? new getExamTypeResponse { respcode = 1, message = "success", _data = x } : new getExamTypeResponse { respcode = 0, message = "No record(s) found." };
            }
            catch (Exception ex)
            {
                return new getExamTypeResponse { respcode = 0, message = ex.Message };
            }
        }

        public getExamTypeResponse GetFinalGradeByUserDetails(int? idnumber, int? classid)
        {
            try
            {
                string sql = @"SELECT [examtype_name]
	                                , score
	                                , perfect_score
	                                , [weight]
	                                , CAST(((score*1.000)/(perfect_score*1.000))*([weight]*1.000) as decimal(6,2)) as grade
                                FROM
                                (SELECT examtype.rubric_id
	                                , outputs as [examtype_name]
	                                , SUM(grade) as score
	                                , SUM(perfect_score) as perfect_score
	                                , MAX(weight) as [weight]
                                FROM [ExamScore]
                                INNER JOIN examlist on examlist.examtype_id = examscore.examtype_id	
                                    AND examlist.class_id=examscore.class_id
                                INNER JOIN examtype on ExamType.examtype_id = examscore.examtype_id
                                INNER JOIN rubric on rubric.rubric_id = examtype.rubric_id
                                WHERE user_id= @idnumber
	                                AND examscore.class_id = @classid
                                GROUP BY examtype.rubric_id,outputs) TEMP
";
                var x = _db.Query<ExamList_Model>(sql, new { idnumber, classid }).ToList();
                return x.Count != 0 ? new getExamTypeResponse { respcode = 1, message = "success", _data = x } : new getExamTypeResponse { respcode = 0, message = "No record(s) found." };
            }
            catch (Exception ex)
            {
                return new getExamTypeResponse { respcode = 0, message = ex.Message };
            }
        }

        public getExamListResponse GetExamList(int? classid)
        {
            try
            {
                string sql = string.Format(@"SELECT examlist_id
                                                ,class_id
                                                ,[ExamType].examtype_id
                                                ,exam_type
                                                ,examtype_name
                                                ,perfect_score
                                                ,[weight] 
                                                ,CASE WHEN ISNULL(lock,0) = 1 THEN 'Yes' ELSE 'No' END AS [islock]
                                                ,[Rubric].rubric_id  
                                             FROM [ExamList]
                                             INNER JOIN [ExamType] 
                                             ON [ExamList].examtype_id = [ExamType].examtype_id
                                             INNER JOIN [Rubric] ON [ExamType].rubric_id = [Rubric].rubric_id
                                             WHERE class_id = @classid

                                            SELECT [ExamType].examtype_id
                                                ,exam_type
                                                ,examtype_name
                                                ,perfect_score
                                                ,[weight] 
                                                ,[Rubric].rubric_id  
                                             FROM [ExamType] 
                                             LEFT JOIN [ExamList]
                                             ON [ExamList].examtype_id = [ExamType].examtype_id
                                                AND [ExamList].class_id = @classid
                                             INNER JOIN [Rubric] ON [ExamType].rubric_id = [Rubric].rubric_id
                                             WHERE [ExamList].examtype_id IS NULL
                                             ");

                var x = _db.QueryMultiple(sql, new { classid });
                var examList = x.Read<ExamList_Model>().ToList();
                var examType = x.Read<ExamList_Model>().ToList();
                return new getExamListResponse
                {
                    respcode = 1,
                    message = "success",
                    _data = examList,
                    _examtype = examType
                };
            }
            catch (Exception ex)
            {
                return new getExamListResponse { respcode = 0, message = ex.Message };
            }
        }

        public getExamTypeResponse GetExamTypes(int? examtypeid)
        {
            string sql = @" SELECT examtype_id
                                  ,exam_type
                                  ,examtype_name
                                  ,perfect_score
                                  ,[weight] 
                                  ,[Rubric].rubric_id  
                              FROM [ExamType] 
                              INNER JOIN [Rubric] ON [ExamType].rubric_id = [Rubric].rubric_id
                              WHERE examtype_id = @examtypeid
                            ";
            var x = _db.Query<ExamList_Model>(sql, new { examtypeid }).ToList();
            return new getExamTypeResponse
            {
                respcode = 1,
                message = "success",
                _data = x,
            };
        }

        public Responses InsertDataExamList(int? classid, int? examtypeid, bool islock, string userlogin)
        {
            try
            {
                string sql = string.Format(@"INSERT INTO [ExamList] (class_id, examtype_id, lock, user_login, user_date)
                                            VALUES(@classid, @examtypeid, @islock, @userlogin, getdate()) ");

                string sql2 = @"INSERT INTO [ExamScore] (user_id, grade, examtype_id, class_id, user_login, user_date)
                                SELECT user_id = [ClassList].user_id
                                       ,grade = 0
                                       ,examtype_id = @examtypeid
                                       ,class_id = @classid
                                       ,user_login = @userlogin
                                       ,user_date = GETDATE()
                                FROM [ClassList]
                                WHERE class_id =  @classid
                                ";
                var x = _db.Execute(sql, new { classid, examtypeid, islock, userlogin });
                var y = _db.Execute(sql2, new { classid, examtypeid, userlogin });
                return new Responses { respcode = 1, message = "success" };

            }
            catch (Exception ex)
            {
                return new Responses { respcode = 0, message = ex.Message };
            }
        }
        public Responses DeleteExamList(int? examlist_id)
        {
            try
            {
                string sql = string.Format(@" DELETE FROM [ExamList]
                                              WHERE examlist_id = @examlist_id");
                var x = _db.Execute(sql, new { examlist_id });

                return new Responses { respcode = 1, message = "success" };
            }
            catch (Exception ex)
            {
                return new Responses { respcode = 0, message = ex.Message };
            }
        }
    }

    public class TeacherClasses_Component : Responses
    {
        public List<TeacherClasses_Model> _data { get; set; }
    }

    public class ClassDetails_Component : Responses
    {
        public List<object> _data { get; set; }
    }

    public class getAttendanceResponse : Responses
    {
        public List<Attendance_Model> _res { get; set; }
    }

    public class getClassListResponse : Responses
    {
        public List<ClassList_Model> _data { get; set; }

        public List<ClassList_Model> _user { get; set; }
    }

    public class getExamListResponse : Responses
    {
        public List<ExamList_Model> _data { get; set; }

        public List<ExamList_Model> _examtype { get; set; }
    }

    public class getExamTypeResponse : Responses
    {
        public List<ExamList_Model> _data { get; set; }
    }

    public class Responses
    {
        public int respcode { get; set; }
        public string message { get; set; }
    }


    public class Attendance_Model
    {
        public string Date { get; set; }
        public string Status { get; set; }
    }

    public class ClassList_Model
    {
        public int classlist_id { get; set; }
        public int user_id { get; set; }
        public string idnumber { get; set; }
        public string l_name { get; set; }
        public string f_name { get; set; }
    }

    public class ExamList_Model
    {
        public int examlist_id { get; set; }
        public string exam_type { get; set; }
        public string examtype_name { get; set; }
        public int perfect_score { get; set; }
        public int weight { get; set; }
        public string islock { get; set; }
        public int rubric_id { get; set; }
        public int class_id { get; set; }
        public int examtype_id { get; set; }
        public decimal grade { get; set; }
        public int score { get; set; }
    }
    public class TeacherClasses_Model
    {
        public string courseid { get; set; }
        public string classid { get; set; }
        public string courseno { get; set; }
        public string coursetitle { get; set; }
        public string roomnumber { get; set; }
        public string starttime { get; set; }
        public string endtime { get; set; }
        public string day { get; set; }
        public int groupno { get; set; }
    }

    
}
