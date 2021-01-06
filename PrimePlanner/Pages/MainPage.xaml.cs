using PrimePlanner.Pages;
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

namespace PrimePlanner
{

    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void mainNavigationPane_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            if (args.IsSettingsInvoked)
            {
                //contentFrame.Navigate(typeof(Views.SettingsPage));
            }
            else
            {
                if (args.InvokedItem is string ItemContent)
                {
                    switch (ItemContent)
                    {
                        case "Home":
                            //mainFrame.Navigate(typeof(Home));
                            break;

                        case "All Courses":
                            mainFrame.Navigate(typeof(AllCourses));
                            break;

                        case "Find Courses":
                            //contentFrame.Navigate(typeof(Views.CreateClaimsTicket));
                            break;

                        case "Reminders":
                            //contentFrame.Navigate(typeof(Views.PrintPage));
                            break;

                        case "Tasks":
                            mainFrame.Navigate(typeof(Tasks));
                            break;

                        case "Calendar":
                            //contentFrame.Navigate(typeof(Views.PrintPage));
                            break;

                        case "Help":
                            //contentFrame.Navigate(typeof(Views.PrintPage));
                            break;

                        case "About":
                            //contentFrame.Navigate(typeof(Views.PrintPage));
                            break;
                    }
                }
            }
        }
    }
}
