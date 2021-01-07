using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimePlanner.API.ClassObjects
{
    public class CourseInfo
    {
        public string name { get; set; }
        public string number { get; set; }
        public string deliveryMethod { get; set; }
        public string description { get; set; }
        public string section { get; set; }
        public string units { get; set; }
        public string prerequisites { get; set; }
        public string term { get; set; }
    }
}
