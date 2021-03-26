using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using PrimePlanner.API.ClassObjects;
using PrimePlanner.API;

namespace PrimePlanner.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class FindCourses : Page
    {
        public FindCourses()
        {
            this.InitializeComponent();
        }

        private void Search_Button_Click(object sender, RoutedEventArgs e)
        {
            ObservableCollection<CourseSections> courseSections = API.Course.GetCourseSections(queryCourse.Text);
            ObservableCollection<CourseOutline> courseOutlines = new ObservableCollection<CourseOutline>();
            listOfSections.ItemsSource = courseSections;

            if (courseSections.Any())
            {
                foreach (CourseSections courseSection in courseSections)
                {
                    string URL = API.Course.GetURL(queryCourse.Text + "/" + courseSection.value);
                    CourseOutline courseOutlineObj = API.Course.setCourseOutlineObj(URL);
                    courseOutlines.Add(courseOutlineObj);
                }

                courseDescription.Text = courseOutlines.First().info.description + " Prerequisites: " + courseOutlines.First().info.prerequisites;
            }
            else
            {
                courseDescription.Text = "This course is either not offered this season or course code is invalid";
            }
        }

        private void listOfCourses_ItemClick(object sender, ItemClickEventArgs e)
        {
            CourseSections course = (CourseSections)e.ClickedItem;
            string URL = API.Course.GetURL(queryCourse.Text + "/" + course.value);
            CourseOutline courseOutlineObj = API.Course.setCourseOutlineObj(URL);
            this.Frame.Navigate(typeof(CourseInfo), courseOutlineObj);

        }
    }
}
