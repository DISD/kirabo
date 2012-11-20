using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using System.Windows.Navigation;
using System.Xml.Linq;

namespace Kirabo
{
    public partial class CategorySelection : PhoneApplicationPage
    {
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

    }
}