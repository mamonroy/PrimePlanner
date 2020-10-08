using Microsoft.Data.Sqlite;
using PrimePlanner.ClassesObjects;
using PrimePlanner.Pages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace PrimePlanner.Dialogs
{
    public sealed partial class ToDoAdd : ContentDialog
    {
        public ToDoAdd()
        {
            this.InitializeComponent();
            Database.ToDoTasks.InitializeDatabase();
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            Database.ToDoTasks.AddData(Titlebox.Text, Taskbox.Text, "Course here", "HARD");
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }


    }
}
