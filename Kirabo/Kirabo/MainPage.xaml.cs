using System;
using System.Windows;
using Microsoft.Phone.Controls;

namespace Kirabo
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();

            // Set the data context of the listbox control to the sample data
            DataContext = App.ViewModel;
            this.Loaded += new RoutedEventHandler(MainPage_Loaded);
            this.progressBar1.IsIndeterminate = true;
        }

       

        // Load data for the ViewModel Items
        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            if (!App.ViewModel.IsDataLoaded)
            {
                App.ViewModel.LoadData();
            }
        }

        private void kiraboStart_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/CategorySelection.xaml", UriKind.Relative));
        }

        //private void image1_ImageFailed(object sender, ExceptionRoutedEventArgs e)
        //{

        //}

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {

        }

        //private void image1_ImageFailed_1(object sender, ExceptionRoutedEventArgs e)
        //{

        //}

        //private void image1_ImageFailed_1(object sender, ExceptionRoutedEventArgs e)
        //{
        //    NavigationService.Navigate(new Uri("/CategorySelection.xaml", UriKind.Relative));
        //}

        //private void button1_Click(object sender, RoutedEventArgs e)
        //{
        //    NavigationService.Navigate(new Uri("/CategorySelection.xaml", UriKind.Relative));
        //}

        
    }
}