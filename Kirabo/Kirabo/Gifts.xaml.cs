using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
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

            public string GiftDescription
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
            var giftData = retrieveGiftData(selectedCategory, "");

            if(giftData.Any() == false)
            {   
                Gift emptyGift = new Gift();
                emptyGift.GiftName = "No "+selectedCategory+" gifts found!";
                emptyGift.GiftImageUri = "../Images/VioletTulip.jpg";

                giftData = new[] {emptyGift};
            }

            giftListBox.ItemsSource = giftData;

        }

        private IEnumerable<Gift> retrieveGiftData(string selectedCategoryItem, string searchItem)
        {
            XDocument loadGiftData = XDocument.Load("Gifts.xml");
            IEnumerable<Gift> giftData = null;
            if(searchItem.Equals(""))
            {
                giftData = from gift in loadGiftData.Descendants("gift")
                               where gift.Parent.Attribute("name").Value == selectedCategoryItem
                               select new Gift()
                               {
                                   GiftName = convertFirstElementToUpperCase((string)gift.Element("Name")),
                                   GiftImageUri = convertFirstElementToUpperCase((string)gift.Element("ImageUri")),
                                   GiftDescription = convertFirstElementToUpperCase((string)gift.Element("Description"))
                               };
            }else
            {
                giftData = from gift in loadGiftData.Descendants("gift")
                           where gift.Parent.Attribute("name").Value == selectedCategoryItem && gift.Element("Name").Value.Contains(searchItem.ToLower())
                           select new Gift()
                           {
                               GiftName = convertFirstElementToUpperCase((string)gift.Element("Name")),
                               GiftImageUri = convertFirstElementToUpperCase((string)gift.Element("ImageUri")),
                               GiftDescription = convertFirstElementToUpperCase((string)gift.Element("Description"))
                           };
            }

          
            return giftData;
        }

        private string convertFirstElementToUpperCase(string text)
        {

            return char.ToUpper(text[0]) + text.Substring(1);
        }

        private void MainListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           
            if (giftListBox.SelectedIndex == -1)
                return;

         
            NavigationService.Navigate(new Uri("/DetailsPage.xaml?selectedGiftImageUri=" + ((Gift)(giftListBox.SelectedItem)).GiftImageUri + "&selectedGift=" + ((Gift)(giftListBox.SelectedItem)).GiftName + "&selectedGiftDescription=" + ((Gift)(giftListBox.SelectedItem)).GiftDescription
                + "&selectedGiftCategory=" + selectedCategory + "&selectedGiftCategoryImageUri=" + selectedCategoryImageUri, UriKind.Relative));

           
           giftListBox.SelectedIndex = -1;
        }


        public static string SelectedCategoryItem { get; set; }

       private void button1_Click_2(object sender, RoutedEventArgs e)
        {
            var giftData = retrieveGiftData(selectedCategory, searchTextBox.Text);

            if (giftData.Any() == false)
            {
                Gift emptyGift = new Gift();
                emptyGift.GiftName = "No " + searchTextBox.Text + " gifts found!";
                emptyGift.GiftImageUri = "../Images/VioletTulip.jpg";

                giftData = new[] { emptyGift };
            }

            giftListBox.ItemsSource = giftData;
        }

       private void searchTextBox_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
       {
           var giftData = retrieveGiftData(selectedCategory, searchTextBox.Text);
           if (e.Key == Key.Enter)
           {
               Gift emptyGift = new Gift();
               emptyGift.GiftName = "No " + searchTextBox.Text + " gifts found!";
               emptyGift.GiftImageUri = "../Images/VioletTulip.jpg";

               giftData = new[] {emptyGift};
           }
           giftListBox.ItemsSource = giftData;
       }

        

        

    }
}