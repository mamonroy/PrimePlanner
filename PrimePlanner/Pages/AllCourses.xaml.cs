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

        public AllCourses()
        {
            this.InitializeComponent();
            Database.AllCourses.InitializeDatabase();
            allCourses = Database.AllCourses.RetrieveFromDB();
            DisplayCourses();
        }

        public void DisplayCourses()
        {
            listOfCourses.ItemsSource = allCourses;
        }

        private async void Add_Course_Button_Click(object sender, RoutedEventArgs e)
        {
            Dialogs.AddCourse addCourse = new Dialogs.AddCourse();
            ContentDialogResult result = await addCourse.ShowAsync();

            if(result == ContentDialogResult.Primary)
            {
                allCourses = Database.AllCourses.RetrieveFromDB();
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
            }
        }
    }
}
