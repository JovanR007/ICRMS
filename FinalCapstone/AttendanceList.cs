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
    
    public partial class AttendanceList
    {
        public int attendanceList_id { get; set; }
        public int user_id { get; set; }
        public Nullable<int> class_id { get; set; }
        public System.DateTime attendance_date { get; set; }
        public bool status { get; set; }
        public Nullable<int> user_login { get; set; }
        public Nullable<System.DateTime> user_date { get; set; }
    }
}
