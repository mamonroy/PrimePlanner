using Newtonsoft.Json.Linq;
using PrimePlanner.API.ClassObjects.CourseOutlineMembers;
using System;
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
        courseSchedule courseSchedule { get; set; }
        Info info { get; set; }
        Instructor instructor { get; set; }
        requiredText requiredText { get; set; }

    }
}