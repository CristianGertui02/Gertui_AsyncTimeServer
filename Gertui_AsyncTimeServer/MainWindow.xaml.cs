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
using System.Net;
using System.Net.Sockets;
using System.Windows.Threading;
using Gertui_AsyncServerLib;

namespace Gertui_AsyncTimeServer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        AsyncSocketServer mServer;
        DispatcherTimer timer;
        public MainWindow()
        {
            InitializeComponent();
            mServer = new AsyncSocketServer();
            btn_Disconetti.IsEnabled = false;
        }
        private void btn_Ascolta_Click(object sender, RoutedEventArgs e)
        {
            mServer.StartListening();

            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 10);
            timer.Tick += Timer_Tick;
            timer.Start();
            btn_Ascolta.IsEnabled = false;
            btn_Disconetti.IsEnabled = true;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            mServer.SendToAll(DateTime.Now.ToShortDateString() + " ");
            mServer.SendToAll(DateTime.Now.ToShortTimeString());
        }

        private void btn_Disconetti_Click(object sender, RoutedEventArgs e)
        {
            mServer.CloseConnection();
            btn_Ascolta.IsEnabled = false;

        }

        private void btn_Invia_Click(object sender, RoutedEventArgs e)
        {
            mServer.SendToAll(txt_Messaggio.Text);
            
        }

    }
}

