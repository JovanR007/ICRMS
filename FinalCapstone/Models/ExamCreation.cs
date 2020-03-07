using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalCapstone.Models
{
    public class ExamCreation
    {
        public ExamCreation()
        {

        }
        public Rubric rubric { get; set; }
        public int class_id { get; set; }
        public int examtype_id { get; set; }
       
        public Nullable<int> rubric_id { get; set; }
        public string exam_type { get; set; }
        public Nullable<int> perfect_score { get; set; }
        public int course_id { get; set; }

        public ExamType examtype { get; set; }
    }
}