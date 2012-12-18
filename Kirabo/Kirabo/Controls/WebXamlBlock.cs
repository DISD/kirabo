using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Kirabo.Controls
{
    public class WebXamlBlock : ContentControl
    {
        #region public Uri XamlUri
        /// <summary>
        /// Gets or sets the XAML Uri.
        /// </summary>
        public Uri XamlUri
        {
            get { return GetValue(XamlUriProperty) as Uri; }
            set { SetValue(XamlUriProperty, value); }
        }
 
        /// <summary>
        /// Identifies the XamlUri dependency property.
        /// </summary>
        public static readonly DependencyProperty XamlUriProperty =
            DependencyProperty.Register(
                "XamlUri",
                typeof(Uri),
                typeof(WebXamlBlock),
                new PropertyMetadata(null, OnXamlUriPropertyChanged));
 
        /// <summary>
        /// XamlUriProperty property changed handler.
        /// </summary>
        /// <param name="d">WebXamlBlock that changed its XamlUri.</param>
        /// <param name="e">Event arguments.</param>
        private static void OnXamlUriPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            WebXamlBlock source = d as WebXamlBlock;
            source.TryDownloading();
        }
        #endregion public Uri XamlUri
 
        #region public object FallbackContent
        /// <summary>
        /// Gets or sets the content to fallback to if the request fails.
        /// </summary>
        public object FallbackContent
        {
            get { return GetValue(FallbackContentProperty) as object; }
            set { SetValue(FallbackContentProperty, value); }
        }
 
        /// <summary>
        /// Identifies the FallbackContent dependency property.
        /// </summary>
        public static readonly DependencyProperty FallbackContentProperty =
            DependencyProperty.Register(
                "FallbackContent",
                typeof(object),
                typeof(WebXamlBlock),
                new PropertyMetadata(null));
        #endregion public object FallbackContent
 
        public WebXamlBlock()
        {
        }
 
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
 
            TryDownloading();
        }
 
        private bool _haveTried;
 
        private void TryDownloading()
        {
            if (_haveTried)
            {
                return;
            }
            _haveTried = true;
            if (XamlUri != null)
            {
                var wc = new WebClient();
                wc.DownloadStringCompleted += OnDownloadStringCompleted;
                wc.DownloadStringAsync(XamlUri);
            }
        }
 
        private void OnError()
        {
            Dispatcher.BeginInvoke(() =>
                {
                    var b = new Binding("FallbackContent") {Source = this};
                    SetBinding(ContentProperty, b);
                });
        }
 
        private void OnDownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            if (e.Error != null || e.Cancelled)
            {
                OnError();
            }
            else
            {
                string xaml = e.Result;
                Dispatcher.BeginInvoke(() =>
                    {
                        try
                        {
                            var o = XamlReader.Load(xaml);
                            if (o != null)
                            {
                                Content = o;
                            }
                        }
                        catch
                        {
                            OnError();
                        }
                    });
            }
        }
    }

}
