using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalCapstone.Models
{
    public class TeacherClass
    {
        public User user { get; set; }
        public Schedule sched { get; set; }
        public Classroom room { get; set; }
        public Subject subj { get; set; }
        public Class cls { get; set; }

        public Course course { get; set; }
    }
}