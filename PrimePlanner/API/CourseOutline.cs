using Newtonsoft.Json.Linq;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;

namespace PrimePlanner.API
{

    public class CourseOutline
    {
        //private const string URL = "http://www.sfu.ca/bin/wcm/course-outlines?2021/spring/cmpt/300/d100"; // Example API URL

        private const string baseURL = "http://www.sfu.ca/bin/wcm/course-outlines?2021/spring/";

        public static string GETApi(string URL)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL);
            request.Method = "GET";
            request.ContentType = "application/json";

            WebResponse webResponse = request.GetResponse();
            Stream webStream = webResponse.GetResponseStream();
            StreamReader responseReader = new StreamReader(webStream);
            string response = responseReader.ReadToEnd();

            return response;
        }

        public static Boolean isGETSuccess(string URL)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL);
            request.Method = "GET";
            request.ContentType = "application/json";

            try { WebResponse webResponse = request.GetResponse(); }
            catch (Exception e)
            {
                Debug.WriteLine("-----------------");
                Debug.WriteLine(e.Message);
                return false;
            }

            return true;
        }

        public static string getCourseDescription(string parameters, string sectionNumber)
        {
            var firstSpaceIndex = parameters.IndexOf(" ");
            var firstString = parameters.Substring(0, firstSpaceIndex);
            var secondString = parameters.Substring(firstSpaceIndex + 1);
            string URL = baseURL + firstString + "/" + secondString + "/" + sectionNumber;
            Debug.WriteLine(URL);
            string response = GETApi(URL);

            JObject jsonObj = JObject.Parse(response);
            Debug.WriteLine(jsonObj);
            return (string)jsonObj["info"]["description"];
        }

        public static ObservableCollection<API.ClassObjects.CourseTitle> GetAvailableCourses(string parameters)
        {
            ObservableCollection<API.ClassObjects.CourseTitle> course = new ObservableCollection<API.ClassObjects.CourseTitle>();
            string URL = null;

            // If query for search contains white spaces, seperate them
            if(parameters.Contains(" "))
            {
                var firstSpaceIndex = parameters.IndexOf(" ");
                var firstString = parameters.Substring(0, firstSpaceIndex);
                var secondString = parameters.Substring(firstSpaceIndex + 1);
                URL = baseURL + firstString + "/" + secondString;
            }

            else
            {
                URL = baseURL + parameters;
            }

            // Returns an empty Collection if API is not working
            if (!isGETSuccess(URL))
                return course;

            string response = GETApi(URL);
            var token = JToken.Parse(response);

            if (token is JObject)
            {
                JObject jsonObj = JObject.Parse(response);
                course.Add(new API.ClassObjects.CourseTitle
                {
                    name_code = parameters.ToUpper(),
                    title = (string)jsonObj["title"],
                    sectionNumber = (string)jsonObj["text"],
                });
            }
            else if (token is JArray)
            {
                JArray jsonArr = JArray.Parse(response);
                Debug.WriteLine(jsonArr);
                //Debug.WriteLine(jsonArr);
                foreach (JObject obj in jsonArr)
                {
                    course.Add(new API.ClassObjects.CourseTitle
                    {
                        name_code = parameters.ToUpper(),
                        title = (string)obj["title"],
                        sectionNumber = (string)obj["text"],
                    });
                }
            }

            return course;
        }
    }
}