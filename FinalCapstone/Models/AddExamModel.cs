using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalCapstone.Models
{
    public class AddExamModel
    {
        public int examlist_id { get; set; }
        public string exam_type { get; set; }
        public string examtype_name { get; set; }
        public int perfect_score { get; set; }
        public int weight { get; set; }
        public string islock { get; set; }
        public int rubric_id { get; set; }
        public int class_id { get; set; }
        public int examtype_id { get; set; }
        public decimal grade { get; set; }
        public int score { get; set; }
    }
}