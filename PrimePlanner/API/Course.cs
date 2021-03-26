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
using PrimePlanner.API.ClassObjects.CourseOutlineMembers;

namespace PrimePlanner.API
{

    public class Course
    {
        //private const string URL = "http://www.sfu.ca/bin/wcm/course-outlines?2021/spring/cmpt/300/d100"; // Example API URL
        private const string baseURL = "http://www.sfu.ca/bin/wcm/course-outlines?2021/spring/";

        public static string GetURL(string courseName)
        {
            var firstSpaceIndex = courseName.IndexOf(" ");
            var firstString = courseName.Substring(0, firstSpaceIndex);
            var secondString = courseName.Substring(firstSpaceIndex + 1);
            string URL = baseURL + firstString + "/" + secondString;

            return URL;
        }

        public static CourseOutline setCourseOutlineObj(string url)
        {
            string response = API.GETApiString(url);
            JObject jsonResponse = JObject.Parse(response);
            
            CourseOutline courseOutline = new CourseOutline();

            // Info
            courseOutline.info = new Info
            {
                educationalGoals = (string)jsonResponse["info"]["educationalGoals"],
                notes = (string)jsonResponse["info"]["notes"],
                deliveryMethod = (string)jsonResponse["info"]["deliveryMethod"],
                description = (string)jsonResponse["info"]["description"],
                section = (string)jsonResponse["info"]["section"],
                units = (string)jsonResponse["info"]["units"],
                title = (string)jsonResponse["info"]["title"],
                type = (string)jsonResponse["info"]["type"],
                classNumber = (string)jsonResponse["info"]["classNumber"],
                departmentalUgradNotes = (string)jsonResponse["info"]["departmentalUgradNotes"],
                prerequisites = (string)jsonResponse["info"]["prerequisites"],
                number = (string)jsonResponse["info"]["number"],
                registrarNotes = (string)jsonResponse["info"]["registrarNotes"],
                outlinePath = (string)jsonResponse["info"]["outlinePath"],
                term = (string)jsonResponse["info"]["term"],
                gradingNotes = (string)jsonResponse["info"]["gradingNotes"],
                corequisites = (string)jsonResponse["info"]["corequisites"],
                dept = (string)jsonResponse["info"]["dept"],
                degreeLevel = (string)jsonResponse["info"]["degreeLevel"],
                specialTopic = (string)jsonResponse["info"]["specialTopic"],
                courseDetails = (string)jsonResponse["info"]["courseDetails"],
                name = (string)jsonResponse["info"]["name"],
                designation = (string)jsonResponse["info"]["designation"]
            };
            
            // Instructor
            JArray jsonArrInstructor = JArray.Parse(jsonResponse["instructor"].ToString());
            foreach (JObject obj in jsonArrInstructor)
            {
                courseOutline.instructor.Add(new Instructor
                {
                    firstName = (string)obj["firstName"],
                    lastName = (string)obj["lastName"],
                    email = (string)obj["email"]
                });
            }

            // Course Schedule
            JArray jsonArrCourseSchedule = JArray.Parse(jsonResponse["courseSchedule"].ToString());
            foreach (JObject obj in jsonArrCourseSchedule)
            {
                courseOutline.courseSchedule.Add(new courseSchedule
                {
                    startTime = (string)obj["startTime"],
                    endTime = (string)obj["endTime"],
                    days = (string)obj["days"]
                });
            }

            return courseOutline;
        }

        public static string GetCourseDescription(string courseName, string sectionNumber)
        {
            string URL = GetURL(courseName) + "/" + sectionNumber;
            string response = API.GETApiString(URL);
            JObject jsonResponse = JObject.Parse(response);

            return (string)jsonResponse["info"]["description"];
        }
          
        public static ObservableCollection<CourseSections> GetCourseSections(string courseName)
        {
            ObservableCollection<CourseSections> courseSections = new ObservableCollection<CourseSections>();

            string URL = null;
            // If query for search contains white spaces, seperate them
            if (courseName.Contains(" "))
                URL = GetURL(courseName);
            else
                return courseSections;

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

        }
    }
}