using Newtonsoft.Json.Linq;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;

namespace PrimePlanner.API
{
    public class API
    {
        //private const string URL = "http://www.sfu.ca/bin/wcm/course-outlines?2021/spring/cmpt/300/d100"; // Example API URL


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

        public static string GETApiString(string URL)
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

    }
}


