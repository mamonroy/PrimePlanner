using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimePlanner.API.ClassObjects
{
    public class CourseSections
    {
        public string name_code { get; set; }
        public string text { get; set; }
        public string value { get; set; }
        public string title { get; set; }
        public string classType { get; set; }
        public string sectionCode { get; set; }
        public string associatedClass { get; set; }
    }
}
