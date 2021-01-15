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
            listOfCourses.ItemsSource = courseSections;

            //if (courseSections.Any())
            //    courseDescription.Text = API.Course.getCourseDescription(queryCourse.Text, course.FirstOrDefault().sectionNumber);
            //else
            //    courseDescription.Text = "This course is either not offered this season or course code is invalid";
        }
    }
}
