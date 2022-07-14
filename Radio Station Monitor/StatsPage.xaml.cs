using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Radio_Station_Monitor
{
    /// <summary>
    /// Interaction logic for StatsPage.xaml
    /// </summary>
    public partial class StatsPage : Page
    {

        ServerStats StreamStats = new ServerStats();
        
        public StatsPage()
        {
            InitializeComponent();
            DispatcherTimer dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(DispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 30);
            dispatcherTimer.Start();

            Status_Text.Text = StreamStats.status;
            Status_Text.Foreground = StreamStats.foreground;
            CurSong_Text.Text = StreamStats.curSong;

        }

        private async void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            StreamStats.updateStats();

            Status_Text.Text = StreamStats.status;
            Status_Text.Foreground = StreamStats.foreground;
            CurSong_Text.Text = StreamStats.curSong;
        }

        private class ServerStats
        {
            public bool failed = false;
            public string status = "Connecting...";
            public Brush foreground = Brushes.Yellow;
            public int curListeners = 0;
            public int peakListeners = 0;
            public int UniqueListeners = 0;
            public int Bitrate = 0;
            public string curSong = "Waiting for song...";


            void setForeground()
            {
                // Set Status
                switch (status)
                {
                    case "Connecting":
                        foreground = Brushes.Yellow;
                        break;
                    case "Offline":
                        foreground = Brushes.Red;
                        break;
                    case "Online":
                        foreground = Brushes.Green;
                        break;
                    default:
                        foreground = Brushes.White;
                        break;
                }

            }

            public async void updateStats()
            {
                return;
            }
        }


    }

}
