using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using WPF_ServerTcp.Commands;
using WPF_ServerTcp.Models;
using WPF_ServerTcp.Views;

namespace WPF_ServerTcp.ViewModels
{
    public class MessageWindowViewModel : BaseViewModel
    {


        private string clientName;

        public string ClientName
        {
            get { return clientName; }
            set { clientName = value; OnPropertyChanged(); }
        }

        private string clientMessage;

        public string ClientMessage
        {
            get { return clientMessage; }
            set { clientMessage = value; OnPropertyChanged(); }
        }


        public RelayCommand SendCommand { get; set; }

        public RelayCommand CloseCommand { get; set; }


        public ClientItem ClientItem { get; set; }

        public BinaryReader BR { get; set; }

        public BinaryWriter BW { get; set; }

        public DispatcherTimer messageReceiverTimer { get; set; }

        public StackPanel MessagePanel { get; set; }
        public async void ReceiveMessage()
        {
            messageReceiverTimer.Stop();
            await Task.Run(() =>
            {
                App.Current.Dispatcher.Invoke((Action)async delegate
                {

                    try
                    {

                        var stream = ClientItem.Client.GetStream();
                        BR = new BinaryReader(stream);
                        App.Current.Dispatcher.Invoke((Action)async delegate
                        {
                            while (true)
                            {
                                var view = new MessageUC();
                                var viewModel = new MessageViewModel();
                                view.DataContext = viewModel;

                                await Task.Run(() =>
                                {
                                    try
                                    {

                                        viewModel.ClientMessage = BR.ReadString();

                                        App.Current.Dispatcher.Invoke((Action)delegate
                                        {
                                            view.HorizontalAlignment = HorizontalAlignment.Left;
                                            MessagePanel.Children.Add(view);
                                        });
                                    }
                                    catch (Exception)
                                    {


                                    }
                                });
                            }
                        });

                    }
                    catch (Exception)
                    {


                    }

                });
            });
        }

        public MessageWindowViewModel()
        {
            messageReceiverTimer = new DispatcherTimer();
            messageReceiverTimer.Interval = TimeSpan.FromSeconds(3);
            messageReceiverTimer.Tick += MessageReceiverTimer_Tick;
            messageReceiverTimer.Start();

            SendCommand = new RelayCommand(c =>
            {
                var stream = ClientItem.Client.GetStream();
                BW = new BinaryWriter(stream);
                BW.Write(ClientMessage);

                var view = new MessageUC();
                var viewModel = new MessageViewModel();
                view.DataContext = viewModel;
                viewModel.ClientMessage = ClientMessage;


                view.HorizontalAlignment = HorizontalAlignment.Right;
                MessagePanel.Children.Add(view);
                ClientMessage = "";
            });
            
        }

        private void MessageReceiverTimer_Tick(object sender, EventArgs e)
        {
            ReceiveMessage();
        }
    }
}
