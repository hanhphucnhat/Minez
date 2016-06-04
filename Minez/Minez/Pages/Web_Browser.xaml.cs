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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Minez.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Web_Browser : Page
    {
        public Web_Browser()
        {
            this.InitializeComponent();

        }

        private void DoWebNavigate()
        {
            if (Web_Address.Text.Length > 0)
            {
                webView.Navigate(new Uri(Web_Address.Text));
            }
        }

        private void Go_Hackster_Click(object sender, RoutedEventArgs e)
        {
            Web_Address.Text = "https://www.hackster.io/windowsiot";
            DoWebNavigate();
        }

        private void Go_WOD_Click(object sender, RoutedEventArgs e)
        {
            Web_Address.Text = "https://www.google.com.vn";
            DoWebNavigate();
        }

        private void Go_Web_Click(object sender, RoutedEventArgs e)
        {
            DoWebNavigate();
        }
    }
}
