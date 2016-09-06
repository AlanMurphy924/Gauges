using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace GaugesTest
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private Timer timer;

        public MainPage()
        {
            this.InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            therm.Temperature += 0.1;
            //therm2.Temperature += 0.1;
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            therm.Temperature -= 0.1;
            //therm2.Temperature -= 0.1;
        }

        private async void TimerExpired(object state)
        {
            await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                if (litres.DeliveredLitres < litres.PresetLitres)
                {
                    litres.DeliveredLitres += 1;
                }
                else
                {
                    timer.Change(Timeout.Infinite, Timeout.Infinite);
                }
            });
        }

        private void btnPump_Click(object sender, RoutedEventArgs e)
        {
            timer = new Timer(TimerExpired, null, 0, 100);
        }
    }
}
