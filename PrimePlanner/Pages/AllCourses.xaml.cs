using Microsoft.Data.Sqlite;
using PrimePlanner.ClassesObjects;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace PrimePlanner.Pages
{

    public sealed partial class AllCourses : Page
    {
        public static ObservableCollection<Course> allCourses = new ObservableCollection<Course>();
        public static Dictionary<string, double> gradeSystem = new Dictionary<string, double>()
        {
            { "A+", 4.33 },
            { "A", 4.00 },
            { "A-", 3.67 },
            { "B+", 3.33 },
            { "B", 3 },
            { "B-", 2.67 },
            { "C+", 2.33 },
            { "C", 2.00 },
            { "C-", 1.67 },
            { "D", 1.00 },
            { "F", 0 }
        };

        public AllCourses()
        {
            this.InitializeComponent();
            Database.AllCourses.InitializeDatabase();
            allCourses = Database.AllCourses.RetrieveFromDB();
            DisplayCourses();
            DisplaySummaryBoard();
        }

        public void DisplayCourses()
        {
            listOfCourses.ItemsSource = allCourses;
        }

        public void DisplaySummaryBoard()
        {
            CGPA.Text = calculateCGPA();
            Units.Text = calculateUnits();
            Status.Text = checkAcademicStatus();
        }

        public string calculateCGPA()
        {
            double GradePoint = 0;
            int cumulativeCredits = 0;
            foreach (Course courses in allCourses)
            {
                GradePoint += gradeSystem[courses.Grade] * Convert.ToInt32(courses.Credits);
                cumulativeCredits += Convert.ToInt32(courses.Credits);
            }
            return Convert.ToString(Math.Round(GradePoint / cumulativeCredits, 4));
        }

        public string calculateUnits()
        {
            int cumulativeCredits = 0;
            foreach (Course courses in allCourses)
            {
                cumulativeCredits += Convert.ToInt32(courses.Credits);
            }
            return Convert.ToString(cumulativeCredits);
        }

        public string checkAcademicStatus()
        {
            return "Good Academic Status";
        }

        private async void Add_Course_Button_Click(object sender, RoutedEventArgs e)
        {
            Dialogs.AddCourse addCourse = new Dialogs.AddCourse();
            ContentDialogResult result = await addCourse.ShowAsync();

            if(result == ContentDialogResult.Primary)
            {
                allCourses = Database.AllCourses.RetrieveFromDB();
                DisplaySummaryBoard();
            }
            
        }

        private void listOfCourses_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        private async void AppBarButton_Delete_Click(object sender, RoutedEventArgs e)
        {
            listOfCourses.SelectedItem = ((FrameworkElement)sender).DataContext;
            Course course = (Course)listOfCourses.SelectedItem;

            Dialogs.DeleteConfirmation deleteConf = new Dialogs.DeleteConfirmation();
            ContentDialogResult result = await deleteConf.ShowAsync();
            if (result == ContentDialogResult.Primary)
            {
                Database.AllCourses.DeleteCourseDB(course.ID.ToString());
                allCourses = Database.AllCourses.RetrieveFromDB();
                DisplaySummaryBoard();
            }
        }

        private async void AppBarButton_Edit_Click(object sender, RoutedEventArgs e)
        {
            listOfCourses.SelectedItem = ((FrameworkElement)sender).DataContext;
            Course course = (Course)listOfCourses.SelectedItem;

            Dialogs.EditCourse editCourse = new Dialogs.EditCourse();
            editCourse.InitializeTextBoxContents(course.ID.ToString(), course.CourseName, course.Grade, course.Credits);
            ContentDialogResult result = await editCourse.ShowAsync();

            if (result == ContentDialogResult.Primary)
            {
                allCourses = Database.AllCourses.RetrieveFromDB();
                DisplaySummaryBoard();
            }
        }

    }
}
