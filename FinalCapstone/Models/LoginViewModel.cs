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
    public class LoginModel
    {
        IDbConnection _db = new SqlConnection(ConfigurationManager.ConnectionStrings["Capstone"].ConnectionString);
        
        public Login_Model GetLogin(string idno, string pass)
        {
            string sql = @"SELECT f_name
                            ,l_name
                            ,idnumber
                            ,[password]
                            ,role_id
                            ,program_id
                            ,batch_id
                            ,email 
                            FROM dbo.[User]
                            WHERE [idnumber] = @idno
                                AND [password] = @pass";
            var result = _db.Query<Login_Model>(sql, new { idno, pass }).FirstOrDefault();
            return result;
        }
    }

    public class Login_Model
    {
        public string f_name { get; set; }
        public string l_name { get; set; }
        public string idnumber { get; set; }
        public string password { get; set; }
        public int role_id { get; set; }
        public int program_id { get; set; }
        public int batch_id { get; set; }
        public string email { get; set; }
    }

    
}
