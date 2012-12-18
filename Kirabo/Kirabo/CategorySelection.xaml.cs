using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Phone.Controls;
using System.Windows.Navigation;
using System.Xml.Linq;
using System.Windows.Controls.Primitives;
using System.ComponentModel;
using System.Threading;

namespace Kirabo
{
    public partial class CategorySelection : PhoneApplicationPage
    {
        private Popup popup;
        private BackgroundWorker backroungWorker;
        // Constructor
        public CategorySelection()
        {
            InitializeComponent();
            // Set the data context of the listbox control to the sample data
            DataContext = App.ViewModel;
            this.Loaded += new System.Windows.RoutedEventHandler(CategorySelection_Loaded);

            
            XDocument loadCategoryData = XDocument.Load("Categories.xml");
            var categoryData = from query in loadCategoryData.Descendants("category")
                               select new Category
                                          {
                                              CategoryName = (string)query.Element("Name"),
                                              CategoryImageUri = (string)query.Element("ImageUri")
                                              };
         categoryList.ItemsSource = categoryData;
         
            ShowPopup();
            ApplicationBar.IsVisible = false;

            SupportedOrientations = SupportedPageOrientation.Portrait;

            Application.Current.RootVisual = this;
        }

        private void CategorySelection_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            if (!App.ViewModel.IsDataLoaded)
            {
                App.ViewModel.LoadData();
            }

           
        }
        
        private void CategoryList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            // If selected index is -1 (no selection) do nothing
            if (categoryList.SelectedIndex == -1)
                return;
            //Gifts.
           // Navigate to the new page
           

            NavigationService.Navigate(new Uri("/Gifts.xaml?selectedCategoryImageUri=" + ((Category)(categoryList.SelectedItem)).CategoryImageUri + "&selectedCategory="+((Category)(categoryList.SelectedItem)).CategoryName, UriKind.Relative));

            // Reset selected index to -1 (no selection)
            categoryList.SelectedIndex = -1;
        }

        /**
         * Describes a class to hold the Categories
         */
        public class Category
        {
            string cateogryName;
            string categoryImageUri;

            public string CategoryName
            {
                get { return cateogryName; }
                set { cateogryName = value; }

            }

            public string CategoryImageUri
            {
                get { return categoryImageUri; }
                set { categoryImageUri = value; }

            }

            
        }

        private void ShowPopup()
        {
            this.popup = new Popup();
            this.popup.Child = new MainPage();
            this.popup.IsOpen = true;
            StartLoadingData();
        }
        private void StartLoadingData()
        {
            backroungWorker = new BackgroundWorker();
            backroungWorker.DoWork += new DoWorkEventHandler(backroungWorker_DoWork);
            backroungWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(backroungWorker_RunWorkerCompleted);
            backroungWorker.RunWorkerAsync();
        }

        void backroungWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.Dispatcher.BeginInvoke(() =>
            {
                this.popup.IsOpen = false;

            }
            );
        }

        void backroungWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            // Do some data loading on a background
            // We'll just sleep for the demo
            Thread.Sleep(3000);
        }


        private void MenuItem1_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/AboutPage.xaml", UriKind.Relative));
        }

        
    }
}