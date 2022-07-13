using System.Windows.Controls;

namespace Radio_Station_Monitor
{
    /// <summary>
    /// Interaction logic for ConnectPage.xaml
    /// </summary>
    public partial class ConnectPage : Page
    {

        string url;
        public string Url { get; set; }

        public ConnectPage()
        {
            InitializeComponent();
        }

        private void ConnectBTN_Click(object sender, System.Windows.RoutedEventArgs e)
        {

            url = StreamURL_Input.Text;

            if (url == "" || url == " ")
            {
                ErrMsg.Text = "A stream url is required";
                return;
            }

            if ( !url.Contains("."))
            {
                ErrMsg.Text = "Please enter a valid url";
                return;
            }

            if (url.Length < 4)
            {
                ErrMsg.Text = "Please enter a valid url";
                return;
            }

            NavigationService.Navigate(new ConnectingPage(url) );

        }
    }
}
