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

            //Load the gift items
            XDocument loadGiftData = XDocument.Load("Gifts.xml");
            var giftData = from gift in loadGiftData.Descendants("gift")
                           where gift.Parent.Attribute("name").Value == selectedCategory
                           select new Gift()
                           {
                               GiftName = (string)gift.Element("Name"),
                               GiftImageUri = (string)gift.Element("ImageUri"),
                               GitDescription = (string)gift.Element("Description")
                               
                           };

            if(giftData.Any() == false)
            {   
                Gift emptyGift = new Gift();
                emptyGift.GiftName = "No "+selectedCategory+" gifts found!";
                emptyGift.GiftImageUri = "../Images/VioletTulip.jpg";

                giftData = new[] {emptyGift};
            }

            giftListBox.ItemsSource = giftData;

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