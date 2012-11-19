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
using System.Xml.Linq;
using Microsoft.Phone.Controls;
using System.Windows.Navigation;
using System.Windows.Media.Imaging;

namespace Kirabo
{
    public partial class Gifts : PhoneApplicationPage
    {
        public Gifts()
        {
            InitializeComponent();
           
            DataContext = App.ViewModel;
            this.Loaded += new System.Windows.RoutedEventHandler(GiftSelections_Loaded);

            XDocument loadGiftData = XDocument.Load("Gifts.xml");
            var giftData = from query in loadGiftData.Descendants("category").Descendants("gift")
                          where  query.
                               //let xAttribute = query.Attribute("name")
                           //where xAttribute != null && xAttribute.Value == "Love"
                      
                               select new Gift()
                               {
                                   GiftName = (string)query.Element("Name"),
                                   GiftImageUri = (string)query.Element("ImageUri"),
                                   GitDescription = (string)query.Element("Description")
                               };
            
            giftListBox.ItemsSource = giftData;

            
        }

        public class Gift
        {
            private string giftName;
            private string giftImageUri;
            private string giftDescription;

            public string GiftName
            {
                get { return giftName; }
                set { giftName = value; }

            }

            public string GiftImageUri
            {
                get { return giftImageUri; }
                set { giftImageUri = value; }

            }

            public string GitDescription
            {
                get { return giftDescription; }
                set { giftDescription = value; }

            }
        }

        private void GiftSelections_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            if (!App.ViewModel.IsDataLoaded)
            {
                App.ViewModel.LoadData();
            }
        }

        private string selectedCategoryImageUri = "";
        private string selectedCategory = "";
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            selectedCategoryImageUri = NavigationContext.QueryString["selectedCategoryImageUri"];
            selectedCategory = NavigationContext.QueryString["selectedCategory"];

            BannerTextBlock.Text = selectedCategory;
            Uri uri = new Uri(selectedCategoryImageUri, UriKind.Relative);
            BitmapImage imgSource = new BitmapImage(uri);
            CategoryImageBox.Source = imgSource;
        }

        private void MainListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // If selected index is -1 (no selection) do nothing
            if (giftListBox.SelectedIndex == -1)
                return;

            // Navigate to the new page
            NavigationService.Navigate(new Uri("/DetailsPage.xaml?selectedItem=" + giftListBox.SelectedIndex, UriKind.Relative));

            // Reset selected index to -1 (no selection)
            giftListBox.SelectedIndex = -1;
        }


        public static string SelectedCategoryItem { get; set; }

    }
}