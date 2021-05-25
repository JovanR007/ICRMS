using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalCapstone.Models
{
    public class UserEditModel
    {
        public int user_id { get; set; }
        public string f_name { get; set; }
        public string l_name { get; set; }
        public Nullable<long> idnumber { get; set; }
        public Nullable<long> password { get; set; }
        public Nullable<int> role_id { get; set; }
        public string user_type { get; set; }
        public string program1 { get; set; }
    
        public Nullable<int> program_id { get; set; }
        public Nullable<int> batch_id { get; set; }
       
        public Nullable<int> user_login { get; set; }
        public Nullable<System.DateTime> user_date{get; set; }
    }
}