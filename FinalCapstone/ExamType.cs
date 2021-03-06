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
    
    public partial class ExamType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ExamType()
        {
            this.ExamLists = new HashSet<ExamList>();
            this.ExamScores = new HashSet<ExamScore>();
        }
    
        public int examtype_id { get; set; }
        public Nullable<int> rubric_id { get; set; }
        public string examtype_name { get; set; }
        public Nullable<int> user_login { get; set; }
        public Nullable<System.DateTime> user_date { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ExamList> ExamLists { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ExamScore> ExamScores { get; set; }
        public virtual Rubric Rubric { get; set; }
    }
}
