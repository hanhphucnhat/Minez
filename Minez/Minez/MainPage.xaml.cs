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


using Windows.Devices.Gpio;
using Windows.UI.Core;
using System.Threading.Tasks;


// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Minez
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private const int LED_PIN = 6;
        private GpioPin pin;
        private GpioPinValue pinValue;

        private const int ContactorQuat8 = 5;
        private GpioPin Quat8;
        private GpioPinValue Quat8Value;

        private DispatcherTimer timer;
        private DispatcherTimer Tcaplieu;
        private DispatcherTimer Tdung;


        bool Caplieu = false;

        private SolidColorBrush redBrush = new SolidColorBrush(Windows.UI.Colors.Red);
        private SolidColorBrush grayBrush = new SolidColorBrush(Windows.UI.Colors.LightGray);

        private SolidColorBrush denMauDo = new SolidColorBrush(Windows.UI.Color.FromArgb(0x59, 0xFF, 0x04, 0x04));
        private SolidColorBrush denMauDoSang = new SolidColorBrush(Windows.UI.Colors.Red);
        private SolidColorBrush denMauXanh = new SolidColorBrush(Windows.UI.Color.FromArgb(0x3F, 0x00, 0xFF, 0x00));
        private SolidColorBrush denMauXanhSang = new SolidColorBrush(Windows.UI.Colors.Green);

        public MainPage()
        {
            this.InitializeComponent();
            // Start Task that updates time
            Task.Factory.StartNew(async () =>
            {
                while (true)
                {
                    await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Low,
                    () =>
                    {
                        Lbl_Time.Text = DateTime.Now.ToString("hh:mm tt");
                        Lbl_Date.Text = DateTime.Now.ToString("MMMM dd, yyyy");
                    });
                    await Task.Delay(1000);
                }
            });

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(500);
            timer.Tick += Timer_Tick;
            InitGPIO();
            if (pin != null)
            {
                timer.Start();
            }

            Tcaplieu = new DispatcherTimer();
            Tcaplieu.Interval = TimeSpan.FromMilliseconds(1000);
            Tcaplieu.Tick += Tcaplieu_Tick;
            Tcaplieu.Stop();

            Tdung = new DispatcherTimer();
            Tdung.Interval = TimeSpan.FromMilliseconds(3000);
            Tdung.Tick += Tdung_Tick;
            Tdung.Stop();
        }

        private void InitGPIO()
        {
            var gpio = GpioController.GetDefault();

            // Show an error if there is no GPIO controller
            if (gpio == null)
            {
                pin = null;
                //GpioStatus.Text = "There is no GPIO controller on this device.";
                return;
            }

            pin = gpio.OpenPin(LED_PIN);
            pinValue = GpioPinValue.High;
            pin.Write(pinValue);
            pin.SetDriveMode(GpioPinDriveMode.Output);

            Quat8 = gpio.OpenPin(ContactorQuat8);
            Quat8Value = GpioPinValue.Low;
            Quat8.Write(Quat8Value);
            Quat8.SetDriveMode(GpioPinDriveMode.Output);

            //GpioStatus.Text = "GPIO pin initialized correctly.";

        }
        private void ClickMe_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Timer_Tick(object sender, object e)
        {
            if (pinValue == GpioPinValue.High)
            {
                pinValue = GpioPinValue.Low;
                pin.Write(pinValue);
                //B0.Fill = redBrush;
            }
            else
            {
                pinValue = GpioPinValue.High;
                pin.Write(pinValue);
                //B0.Fill = grayBrush;
            }
        }

        private void Tcaplieu_Tick(object sender, object e)
        {
            if (Caplieu)
            {
                Quat8Value = GpioPinValue.Low;
                Quat8.Write(Quat8Value);

                Button_CapLieu_NC.Background = denMauDoSang;
                Button_CapLieu_NO.Background = denMauXanh;
            }
            Tdung.Start();
            Tcaplieu.Stop();
        }

        private void Tdung_Tick(object sender, object e)
        {
            if (Caplieu)
            {
                Quat8Value = GpioPinValue.High;
                Quat8.Write(Quat8Value);

                Button_CapLieu_NC.Background = denMauDo;
                Button_CapLieu_NO.Background = denMauXanhSang;
            }
            Tcaplieu.Start();
            Tdung.Stop();
        }

        private void Button_Quat8_NO_Click(object sender, RoutedEventArgs e)
        {
            //if(Quat8Value == GpioPinValue.Low)
            //{
            //    Quat8Value = GpioPinValue.High;
            //    Quat8.Write(Quat8Value);

            //    Button_Quat8_NC.Background = denMauDo;
            //    Button_Quat8_NO.Background = denMauXanhSang;
            //}
        }

        private void Button_Quat8_NC_Click(object sender, RoutedEventArgs e)
        {
            //if(Quat8Value == GpioPinValue.High)
            //{
            //    Quat8Value = GpioPinValue.Low;
            //    Quat8.Write(Quat8Value);

            //    Button_Quat8_NC.Background = denMauDoSang;
            //    Button_Quat8_NO.Background = denMauXanh;
            //}
        }


        private void Button_GauLo_NC_Click(object sender, RoutedEventArgs e)
        {

            //if (Quat8Value == GpioPinValue.High)
            {
                //Quat8Value = GpioPinValue.Low;
                //Quat8.Write(Quat8Value);

                Button_GauLo_NC.Background = denMauDoSang;
                Button_GauLo_NO.Background = denMauXanh;
            }
        }


        private void Buuton_VitTai_NC_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_BangTai_NC_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_QuatGau_NC_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_CapLieu_NC_Click(object sender, RoutedEventArgs e)
        {
            Quat8Value = GpioPinValue.Low;
            Quat8.Write(Quat8Value);

            Button_CapLieu_NC.Background = denMauDoSang;
            Button_CapLieu_NO.Background = denMauXanh;

            Caplieu = false;
            Tcaplieu.Stop();
        }

        private void Button_QuatLo_NC_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_GauBe_NC_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_QuatBe_NC_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_TuDong_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_BangTay_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Say_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Quat_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_TamDung_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Chay_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_GauLo_NO_Click(object sender, RoutedEventArgs e)
        {

            //if (Quat8Value == GpioPinValue.Low)
            {
                //Quat8Value = GpioPinValue.High;
                //Quat8.Write(Quat8Value);
                Button_GauLo_NC.Background = denMauDo;
                Button_GauLo_NO.Background = denMauXanhSang;
            }
        }

        private void Buuton_VitTai_NO_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_BangTai_NO_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_QuatGau_NO_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_CapLieu_NO_Click(object sender, RoutedEventArgs e)
        {
            Quat8Value = GpioPinValue.High;
            Quat8.Write(Quat8Value);

            Button_CapLieu_NC.Background = denMauDo;
            Button_CapLieu_NO.Background = denMauXanhSang;

            Caplieu = true;

            Tcaplieu.Start();
        }

        private void Button_QuatLo_NO_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_GauBe_NO_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_QuatBe_NO_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_VaoLo_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_RaLo_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Ngung_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Reset_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_None_0_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_None_1_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_None_2_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_None_3_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_None_4_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_None_5_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_None_6_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_None_7_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_None_8_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_None_9_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_None_10_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_None_11_Click(object sender, RoutedEventArgs e)
        {

        }

        private void button_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame_Main.Visibility = Visibility.Visible;
            //Lbl_Time.Visibility = Visibility.Collapsed;
            //Lbl_Date.Visibility = Visibility.Collapsed;

            Pages.Settings Settings = new Pages.Settings();
            Frame_Main.Navigate(Settings.GetType());
        }

        private void Frame_Main_Navigated(object sender, NavigationEventArgs e)
        {

        }

        private void Frame_Main_Navigating(object sender, NavigatingCancelEventArgs e)
        {

        }

        private void button1_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame_Main.Visibility = Visibility.Collapsed;
            //Lbl_Time.Visibility = Visibility.Visible;
            //Lbl_Date.Visibility = Visibility.Visible;
        }

        private void button_Webbrowser_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame_Main.Visibility = Visibility.Visible;
            //Lbl_Time.Visibility = Visibility.Collapsed;
            //Lbl_Date.Visibility = Visibility.Collapsed;

            Pages.Web_Browser Web_Browser = new Pages.Web_Browser();
            Frame_Main.Navigate(Web_Browser.GetType());
        }
    }
}
