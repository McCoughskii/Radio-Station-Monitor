﻿using System.Windows;

namespace Radio_Station_Monitor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            _NavigationFrame.Navigate(new ConnectPage());
        }
    }
}
