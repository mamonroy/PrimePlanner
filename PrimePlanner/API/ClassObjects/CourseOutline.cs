using Newtonsoft.Json.Linq;
using PrimePlanner.API.ClassObjects.CourseOutlineMembers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;


namespace PrimePlanner.API
{
    public class CourseOutline
    {
       
        public Info info { get; set; }
        public List<Instructor> instructor = new List<Instructor>();
        public List<courseSchedule> courseSchedule = new List<courseSchedule>();
        public List<requiredText> requiredText = new List<requiredText>();

    }
}