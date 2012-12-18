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
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using System.Windows.Media.Imaging;
using System.Xml.Linq;
using Microsoft.Phone.Shell;
using Microsoft.Phone.Tasks;

namespace Kirabo
{
    public partial class DetailsPage : PhoneApplicationPage
    {
        // Constructor
        public DetailsPage()
        {
            InitializeComponent();

            
        }

       

        public class Meaning
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
        // When page is navigated to set data context to selected item in list

        private string selectedGiftImageUri = "";
        private string selectedGift = "";
        private string selectedGiftDescription = "";
        private string selectedGiftCategory = "";
        private string selectedGiftCategoryImageUri = "";

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            selectedGiftImageUri = NavigationContext.QueryString["selectedGiftImageUri"];
            selectedGift = NavigationContext.QueryString["selectedGift"];
            selectedGiftDescription = NavigationContext.QueryString["selectedGiftDescription"];
            selectedGiftCategory = NavigationContext.QueryString["selectedGiftCategory"];
            selectedGiftCategoryImageUri = NavigationContext.QueryString["selectedGiftCategoryImageUri"];

            GiftTitle.Text = selectedGift;
            textBlock2.Text = selectedGiftDescription;
            Uri uri = new Uri(selectedGiftImageUri, UriKind.Relative);
            BitmapImage imgSource = new BitmapImage(uri);
            detailsImage.Source = imgSource;
            BannerTextBlock.Text = selectedGiftCategory;

            uri = new Uri(selectedGiftCategoryImageUri, UriKind.Relative);
            imgSource = new BitmapImage(uri);
            CategoryImageBox.Source = imgSource;
           

            XDocument loadGiftData = XDocument.Load("Gifts.xml");
            var giftData = from gift in loadGiftData.Descendants("gift")
                           where gift.Parent.Attribute("name").Value == selectedGift
                           select new Meaning()
                           {
                               GiftName = (string)gift.Element("Name"),
                               GiftImageUri = (string)gift.Element("ImageUri"),
                               GiftDescription = (string)gift.Element("Description")

                           };
        }

        
         
       

        //private void button1_Click_1(object sender, RoutedEventArgs e)
        //{
        //    //MessageBoxResult result = MessageBox.Show("Send gift as SMS?", "Kirabo Infobox", MessageBoxButton.OKCancel);
        //    //if (result == MessageBoxResult.OK)
        //    //{
        //        // sms
        //        SmsComposeTask composeSMS = new SmsComposeTask();
        //        //composeSMS.Body = "Hello i send you a gift for " + BannerTextBlock.Text +", "+ GiftTitle.Text + " meaning " + textBlock2.Text;
        //        composeSMS.Body = "Hello i send you aVirtual Gift of " + GiftTitle.Text + " meaning " + textBlock2.Text;
        //        composeSMS.Show();
        //    //}
        //}

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            //EmailAddressChooserTask selectEmailAddress = new EmailAddressChooserTask();
            //selectEmailAddress.Completed += new EventHandler<EmailResult>(emailAddressChooserTask_Completed);

            //selectEmailAddress.Show();
            //MessageBox.Show(email);

            //MessageBoxResult result = MessageBox.Show("Send gift as Email?", "Kirabo Infobox", MessageBoxButton.OKCancel);
            //if (result == MessageBoxResult.OK)
            //{
            //    EmailComposeTask emailcomposer = new EmailComposeTask();
            //    emailcomposer.To = emailTextBox.Text;
            //    emailcomposer.Body = "Hello i send you a gift for " + BannerTextBlock.Text + ", " + GiftTitle.Text + " meaning " + textBlock2.Text;
            //    emailcomposer.Subject = "Kirabo (Gift): ";
            //    emailcomposer.Show();


            //}

           

        }

        private void detailsImage_ImageFailed(object sender, ExceptionRoutedEventArgs e)
        {

        }

        


        //private string email = "";
        //private void emailAddressChooserTask_Completed(object sender, EmailResult e)
        //{
        //    if(e.TaskResult == TaskResult.OK)
        //    {
        //        email = e.Email;
        //    }
        //}

        private void MenuItem1_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/AboutPage.xaml", UriKind.Relative));
        }

        private void Sms_Click(object sender, EventArgs e)
        {
            SmsComposeTask composeSMS = new SmsComposeTask();
            //composeSMS.Body = "Hello i send you a gift for " + BannerTextBlock.Text +", "+ GiftTitle.Text + " meaning " + textBlock2.Text;
            composeSMS.Body = "Hello i send you aVirtual Gift of " + GiftTitle.Text + " meaning " + textBlock2.Text;
            composeSMS.Show();
        }

        private void BackButton_Click(object sender, EventArgs e)
        {

            //MessageBox.Show("Back Application Bar Is Clicked!!!");
            NavigationService.Navigate(new Uri("/Gifts.xaml?selectedCategory=" + selectedGiftCategory + "&selectedCategoryImageUri=" + selectedGiftCategoryImageUri + "&selectedGift=" + selectedGift +
                "&selectedGiftDescription=" + selectedGiftDescription + "&selectedGiftImageUri=" + selectedGiftImageUri , UriKind.Relative));
        //    Response.Redirect("~/Gifts.xam");selectedGiftCategory
        }
    }
}