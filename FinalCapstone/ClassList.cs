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
    
    public partial class ClassList
    {
        public int classlist_id { get; set; }
        public int user_id { get; set; }
        public Nullable<int> class_id { get; set; }
        public Nullable<int> classlist_no { get; set; }
    
        public virtual Class Class { get; set; }
        public virtual User User { get; set; }
    }
}
