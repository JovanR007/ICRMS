//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FinalCapstone
{
    using System;
    using System.Collections.Generic;
    
    public partial class Rubric
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Rubric()
        {
            this.ExamTypes = new HashSet<ExamType>();
        }
    
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
