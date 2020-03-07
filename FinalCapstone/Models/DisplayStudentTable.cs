using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalCapstone.Models
{
    public class DisplayStudentTable
    {
      public User user { get; set; }
        public Class classe { get;set;}
        public ClassList classlist { get; set; }
        public int grade { get; set; }
        public ExamList examlist { get; set; }

        public int classlist_id { get; set; }
        public int examlist_id { get; set; }
        public ExamType examtype { get; set; }
        public ExamScore examscore { get; set; }
        public Rubric rubric { get; set; }

    }
}