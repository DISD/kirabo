using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Collections.ObjectModel;




namespace Kirabo
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public MainViewModel()
        {
            this.CategoryOfGifts = new ObservableCollection<ItemViewModel>();
            this.Gifts = new ObservableCollection<ItemViewModel>();
            
        }

        /// <summary>
        /// A collection for ItemViewModel objects.
        /// </summary>
        
        public ObservableCollection<ItemViewModel>CategoryOfGifts { get; private set; }

        public ObservableCollection<ItemViewModel> Gifts { get; private set; } 

        private string _sampleProperty = "Sample Runtime Property Value";
        /// <summary>
        /// Sample ViewModel property; this property is used in the view to display its value using a Binding
        /// </summary>
        /// <returns></returns>
        public string SampleProperty
        {
            get
            {
                return _sampleProperty;
            }
            set
            {
                if (value != _sampleProperty)
                {
                    _sampleProperty = value;
                    NotifyPropertyChanged("SampleProperty");
                }
            }
        }

        public bool IsDataLoaded
        {
            get;
            private set;
        }

        #region Using Local OData
        /// <summary>
        /// Creates and adds a few ItemViewModel objects into the Items collection.
        /// </summary>
        public void LoadData()
        {
            #region
            // Sample data: Category
            this.CategoryOfGifts.Add(new ItemViewModel() { ItemName = "Love", ItemImage = "../Images/sunflower.jpg", ItemDescription = "Passionate love" });
            this.CategoryOfGifts.Add(new ItemViewModel() { ItemName = "Kids", ItemImage = "../Images/white roses flowers wallpapers (8).png", ItemDescription = "3 to 13" });

            //Sample data:Gifts
            this.Gifts.Add(new ItemViewModel() { ItemName = "Pink rose", ItemImage = "../Images/12_pink_rose.jpg", ItemDescription = "long" });
            this.Gifts.Add(new ItemViewModel() { ItemName = "Violet rose", ItemImage = "../Images/VioletTulip.jpg", ItemDescription = "long" });
            
        #endregion
            this.IsDataLoaded = true;
        }
        #endregion

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (null != handler)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}