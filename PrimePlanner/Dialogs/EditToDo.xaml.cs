using System;
using System.Collections.Generic;
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

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace PrimePlanner.Dialogs
{
    public sealed partial class EditToDo : ContentDialog
    {
        private string ID = null;
        public EditToDo()
        {
            this.InitializeComponent();
            Database.ToDoTasks.InitializeDatabase();
        }
        public void InitializeTextBoxContents(string ID, string title, string task, string courseName, string priorityLevel)
        {
            this.ID = ID;
            Titlebox.Text = title;
            Taskbox.Text = task;
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            Database.ToDoTasks.EditTaskDB(ID,Titlebox.Text, Taskbox.Text, "sample", "sample");
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }
    }
}
