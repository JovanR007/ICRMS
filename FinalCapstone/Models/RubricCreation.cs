using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalCapstone.Models
{
    public class RubricCreation
    {
        public int rubric_id { get; set; }
        public string outputs { get; set; }
        public Nullable<int> weight { get; set; }
        public Nullable<int> course_id { get; set; }
        public Nullable<int> user_login { get; set; }
        public Nullable<System.DateTime> user_date { get; set; }

        public virtual Course Course { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ExamType> ExamTypes { get; set; }
    }
}