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

using System.Threading.Tasks;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Minez.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Settings : Page
    {
        public Settings()
        {
            this.InitializeComponent();
          Library.Core.Device  Thietbi = new Library.Core.Device();

            Thietbi.Id = 1;
            Thietbi.ImagePath = "";
            Thietbi.Name = "Thiet bi 1";
            Thietbi.Pin = (Library.Core.Device.PinsEnum) 10;
            Thietbi.I2C_Slave_Address = 0x40;

            Task.Factory.StartNew(() =>
            {
                Thietbi.TurnOn();
            }).Wait(1000);
        }
        
    }
}
