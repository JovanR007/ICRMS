using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
namespace FinalCapstone.Models
{
    public class SubjectCreation
    {
        public int subject_id { get; set; }

        public int course_id { get; set; }
        public string courseno { get; set; }
        public string coursetitle { get; set; }
        public string coursedescription { get; set; }


    }
}