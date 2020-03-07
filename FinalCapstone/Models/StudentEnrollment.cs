using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalCapstone.Models
{
    public class StudentEnrollment
    {
        public int subject_id { get; set; }
        public int user_id { get; set; }
   
        public int class_id { get; set; }
        public string subject_name { get; set; }
        public Nullable<long> idnumber { get; set; }
        public Nullable<int> group_no { get; set; }

        public int course_id { get; set; }
        public string courseno { get; set; }
        public Nullable<int> classlist_no { get; set; }

    }
}