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
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.IO;
using System.Xml;

namespace Radio_Station_Monitor
{
    /// <summary>
    /// Interaction logic for StatsPage.xaml
    /// </summary>
    public partial class StatsPage : Page
    {

        ServerStats StreamStats = new ServerStats();
        static readonly HttpClient client = new HttpClient();

        public StatsPage()
        {
            InitializeComponent();

            client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0");

            DispatcherTimer dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(DispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 10);
            dispatcherTimer.Start();

            DispatcherTimer_Tick(null, null);
        }

        private async void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            // pull data from server.

            try
            {
                HttpResponseMessage response = await client.GetAsync(Secrets.ServerURL);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();

                // parse XML
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(responseBody);

                XmlNode root = xmlDoc.DocumentElement;

                // display all the data.

                Cur_Listeners.Text = root.SelectSingleNode("CURRENTLISTENERS").InnerText;
                Peak_Listeners.Text = root.SelectSingleNode("PEAKLISTENERS").InnerText;
                Genre.Text = root.SelectSingleNode("SERVERGENRE").InnerText;
                StreamHits.Text = root.SelectSingleNode("STREAMHITS").InnerText;
                CurSong_Text.Text = root.SelectSingleNode("SONGTITLE").InnerText;

                switch (root.SelectSingleNode("STREAMSTATUS").InnerText)
                {
                    case "1":
                        Status_Text.Text = "Online";
                        Status_Text.Foreground = Brushes.Green;
                        break;
                    case "0":
                        Status_Text.Text = "Offline";
                        Status_Text.Foreground = Brushes.Red;
                        break;
                    default:
                        Status_Text.Text = "Unknown";
                        Status_Text.Foreground = Brushes.Black;
                        break;
                }

                // convert seconds to minutes and seconds.
                int seconds = Convert.ToInt32(root.SelectSingleNode("AVERAGETIME").InnerText);
                int minutes = seconds / 60;
                int seconds_left = seconds % 60;

                Avg_ListenTime.Text = minutes.ToString() + "m " + seconds_left.ToString() + "s";

            }
            catch (HttpRequestException err)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", err.Message);
            }

            
        }

        private class ServerStats
        {
            public bool failed = false;
            public string status = "Connecting...";
            public Brush foreground = Brushes.Yellow;
            public string curListeners = "0";
            public string peakListeners = "0";
            public string UniqueListeners = "0";
            public string Bitrate = "0";
            public string curSong = "Waiting for song...";


            void SetForeground()
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
        }


    }

}
