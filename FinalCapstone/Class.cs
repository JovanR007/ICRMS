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
    
    public partial class Class
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Class()
        {
            this.ClassLists = new HashSet<ClassList>();
            this.ExamLists = new HashSet<ExamList>();
        }
    
        public int class_id { get; set; }
        public Nullable<int> classroom_id { get; set; }
        public Nullable<int> user_id { get; set; }
        public Nullable<int> subject_id { get; set; }
        public Nullable<int> schedule_id { get; set; }
        public Nullable<int> group_no { get; set; }
    
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
