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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace PrimePlanner.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    /// 
    public sealed partial class Tasks : Page
    {
        public static ObservableCollection<ToDo> ToDoLists = new ObservableCollection<ToDo>();

        public Tasks()
        {
            this.InitializeComponent();
            Database.ToDoTasks.InitializeDatabase();
            ToDoLists = Database.ToDoTasks.RetrieveFromDB();
            DisplayToDoTasks();
        }

        public void DisplayToDoTasks()
        {
            ToDoItems.ItemsSource = ToDoLists;
        }

        private async void Button_AddToDo_Click(object sender, RoutedEventArgs e)
        {
            Dialogs.ToDoAdd toAddDialog = new Dialogs.ToDoAdd();
            ContentDialogResult result = await toAddDialog.ShowAsync();
            if(result == ContentDialogResult.Primary)
            {
                ToDoLists = Database.ToDoTasks.RetrieveFromDB();
            }
        }

        private void Button_AddInProgress_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_AddCompleted_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_AddOverdue_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ToDoItem_ItemClick(object sender, ItemClickEventArgs e)
        {
            ToDoCommandBar.Visibility = Visibility.Visible;
        }

        private async void AppBarButton_Delete_Click(object sender, RoutedEventArgs e)
        {
            ToDo toDoItem = (ToDo)ToDoItems.SelectedItem;
            ToDoCommandBar.Visibility = Visibility.Collapsed;
            Dialogs.DeleteConfirmation deleteConf = new Dialogs.DeleteConfirmation();
            ContentDialogResult result = await deleteConf.ShowAsync();
            if (result == ContentDialogResult.Primary)
            {
                Database.ToDoTasks.DeleteTaskDB(toDoItem.ID.ToString());
                ToDoLists = Database.ToDoTasks.RetrieveFromDB();
            }
            
        }

        private async void AppBarButton_Edit_Click(object sender, RoutedEventArgs e)
        {
            ToDo toDoItem = (ToDo)ToDoItems.SelectedItem;
            ToDoCommandBar.Visibility = Visibility.Collapsed;
            Dialogs.EditToDo editDialog = new Dialogs.EditToDo();

            editDialog.InitializeTextBoxContents(toDoItem.ID.ToString(), toDoItem.Title, toDoItem.Task, "sample", "sample");

            ContentDialogResult result = await editDialog.ShowAsync();
            if (result == ContentDialogResult.Primary)
            {
                ToDoLists = Database.ToDoTasks.RetrieveFromDB();
            }
        }
    }
}
