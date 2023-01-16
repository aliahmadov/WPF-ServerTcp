using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using WPF_ServerTcp.Models;
using WPF_ServerTcp.Services;

namespace WPF_ServerTcp.ViewModels
{
    public class MainViewModel : BaseViewModel
    {

        private ObservableCollection<ClientItem> tcpClients;

        public ObservableCollection<ClientItem> TcpClients
        {
            get { return tcpClients; }
            set { tcpClients = value; OnPropertyChanged(); }
        }


        private ObservableCollection<ClientItem> offlineTcpClients;

        public ObservableCollection<ClientItem> OfflineTcpClients
        {
            get { return offlineTcpClients; }
            set { offlineTcpClients = value; OnPropertyChanged(); }
        }


        public TcpClient SelectedClient { get; set; }

        public TcpListener TcpListener { get; set; }


        DispatcherTimer acceptDispatcher;

        DispatcherTimer checkDispatcher;

        public BinaryReader BinaryReader { get; set; }

        public BinaryWriter BinaryWriter { get; set; }





        public async void GetClients()
        {
            await Task.Run(async () =>
            {
                var client = await TcpListener.AcceptTcpClientAsync();
                var clientItem = new ClientItem();
                while (true)
                {

                    var stream = client.GetStream();
                    string name;

                    try
                    {
                        BinaryReader = new BinaryReader(stream);
                        name = BinaryReader.ReadString();
                        App.Current.Dispatcher.Invoke((Action)delegate // <--- HERE
                        {
                            clientItem = new ClientItem { Client = client, Name = name };
                            if(OfflineTcpClients.Any(c=>c.Name==clientItem.Name))
                            {
                                var cI = OfflineTcpClients.First(c => c.Name == clientItem.Name);
                                OfflineTcpClients.Remove(cI);

                            }
                            TcpClients.Add(clientItem);
                        });
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"{clientItem.Name} disconnected");
                        break;
                    }

                }

            });
        }
        public MainViewModel()
        {
            //acceptDispatcher = new DispatcherTimer();
            //acceptDispatcher.Interval = TimeSpan.FromSeconds(1);
            //acceptDispatcher.Tick += AcceptDispatcher_Tick;
            //acceptDispatcher.Start();


            checkDispatcher = new DispatcherTimer();
            checkDispatcher.Interval = TimeSpan.FromSeconds(1);
            checkDispatcher.Tick += CheckDispatcher_Tick;
            checkDispatcher.Start();

            OfflineTcpClients = new ObservableCollection<ClientItem>();
            TcpClients = new ObservableCollection<ClientItem>();

            var port = 27001;
            var ip = IPAddress.Parse(IPService.GetLocalIPAddress());
            var endPoint = new IPEndPoint(ip, port);

            TcpListener = new TcpListener(endPoint);
            TcpListener.Start();
            MessageBox.Show($"Listening on {TcpListener.LocalEndpoint}");

            GetClients();

        }



        private void CheckDispatcher_Tick(object sender, EventArgs e)
        {
            GetClients();
            for (int i = 0; i < TcpClients.Count; i++)
            {
                var item = TcpClients[i];
                if (!item.Client.Connected)
                {
                    TcpClients.Remove(item);
                    OfflineTcpClients.Add(item);
                }
                else
                {
                    if (OfflineTcpClients != null)
                        if (OfflineTcpClients.Count != 0)
                            OfflineTcpClients.Remove(item);
                }
            }
        }
    }
}
