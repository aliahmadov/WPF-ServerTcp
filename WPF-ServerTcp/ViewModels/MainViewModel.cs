using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using WPF_ServerTcp.Services;

namespace WPF_ServerTcp.ViewModels
{
    public class MainViewModel : BaseViewModel
    {

        private ObservableCollection<TcpClient> tcpClients;

        public ObservableCollection<TcpClient> TcpClients
        {
            get { return tcpClients; }
            set { tcpClients = value; OnPropertyChanged(); }
        }


        public TcpClient SelectedClient { get; set; }

        public TcpListener TcpListener { get; set; }


        DispatcherTimer acceptDispatcher;
        public async void AcceptClients()
        {
            await Task.Run(() =>
            {
                var client = TcpListener.AcceptTcpClient();

                App.Current.Dispatcher.Invoke((Action)delegate // <--- HERE
                {
                    TcpClients.Add(client);
                });
             
            });
        }
        public MainViewModel()
        {
            acceptDispatcher = new DispatcherTimer();
            acceptDispatcher.Interval = TimeSpan.FromSeconds(1);
            acceptDispatcher.Tick += AcceptDispatcher_Tick;
            acceptDispatcher.Start();


            TcpClients = new ObservableCollection<TcpClient>();

            var port = 27001;
            var ip = IPAddress.Parse(IPService.GetLocalIPAddress());
            var endPoint = new IPEndPoint(ip, port);

            TcpListener = new TcpListener(endPoint);
            TcpListener.Start();
            MessageBox.Show($"Listening on {TcpListener.LocalEndpoint}");


        }

        private void AcceptDispatcher_Tick(object sender, EventArgs e)
        {
            AcceptClients();
        }
    }
}
