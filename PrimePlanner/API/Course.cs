using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PrimePlanner.API.ClassObjects;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;

namespace PrimePlanner.API
{

    public class Course
    {
        //private const string URL = "http://www.sfu.ca/bin/wcm/course-outlines?2021/spring/cmpt/300/d100"; // Example API URL
        private const string baseURL = "http://www.sfu.ca/bin/wcm/course-outlines?2021/spring/";


        public static ObservableCollection<CourseSections> GetCourseSections(string courseName)
        {
            ObservableCollection<CourseSections> courseSections = new ObservableCollection<CourseSections>();

            string URL = null;
            // If query for search contains white spaces, seperate them
            if (courseName.Contains(" "))
            {
                var firstSpaceIndex = courseName.IndexOf(" ");
                var firstString = courseName.Substring(0, firstSpaceIndex);
                var secondString = courseName.Substring(firstSpaceIndex + 1);
                URL = baseURL + firstString + "/" + secondString;
            }
            else
            {
                return courseSections;
            }

            // Return Empty CourseOutline if API is unsuccessful
            if (!API.isGETSuccess(URL))
                return courseSections;

            string response = API.GETApiString(URL);

            JArray jsonArr = JArray.Parse(response);

            foreach (JObject obj in jsonArr)
            {

                string jsonString = obj.ToString();
                CourseSections courseInfo = JsonConvert.DeserializeObject<CourseSections>(jsonString);
                courseInfo.name_code = courseName.ToUpper();
                courseSections.Add(courseInfo);
            }


            return courseSections;

            //if (token is JObject)
            //{
            //    CourseSections courseInfo = JsonConvert.DeserializeObject<CourseSections>(response);
            //    courseInfo.name_code = courseName.ToUpper();
            //    courseSections.Add(courseInfo);
            //}

            //else if (token is JArray)
            //{
            //    JArray jsonArr = JArray.Parse(response);

            //    foreach (JObject obj in jsonArr)
            //    {

            //        string jsonString = obj.ToString();
            //        CourseSections courseInfo = JsonConvert.DeserializeObject<CourseSections>(jsonString);
            //        courseInfo.name_code = courseName.ToUpper();
            //        courseSections.Add(courseInfo);
            //    }
            //}
        }
    }
}