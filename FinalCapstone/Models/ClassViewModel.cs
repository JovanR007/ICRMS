using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalCapstone.Models
{
    public class ClassViewModel
    {
        public int class_id { get; set; }
        public int course_id { get; set; }
        public string room_number{get;set;}
        public Nullable<int> classroom_id { get; set; }
        public Nullable<int> user_id { get; set; }
        public Nullable<int> subject_id { get; set; }
        public Nullable<int> schedule_id { get; set; }
        public Nullable<int> group_no { get; set; }
        public Nullable<int> user_login { get; set; }
        public string time_start { get; set; }
        public string time_end { get; set; }
        public string day { get; set; }
        public Nullable<System.DateTime> user_date { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ClassList> ClassLists { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ExamList> ExamLists { get; set; }
        public virtual Classroom Classroom { get; set; }
        public virtual Schedule Schedule { get; set; }
        public virtual Subject Subject { get; set; }
        public virtual User User { get; set; }
    }
}