using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalCapstone.Models
{
    public class ClassCreation
    {
        public int subject_id { get; set; }
        public int schedule_id { get; set; }
        public int user_id { get; set; }
        public int classroom_id { get; set; }
        public string subject_name { get; set; }

        public int course_id { get; set; }
        public string courseno { get; set; }
        public string room_number { get; set; }     
        public Nullable<System.DateTime> time_start { get; set; }
        public Nullable<System.DateTime> time_end { get; set; }
        public string day { get; set; }
        public Nullable<int> group_no { get; set; }
        public string f_name { get; set; }
    }
}