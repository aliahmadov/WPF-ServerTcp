using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WPF_ServerTcp.Commands;
using WPF_ServerTcp.Views;

namespace WPF_ServerTcp.ViewModels
{
    public class MessageWindowViewModel:BaseViewModel
    {


        private string clientName;

        public string ClientName
        {
            get { return clientName; }
            set { clientName = value;OnPropertyChanged(); }
        }

        public RelayCommand SendCommand { get; set; }

        public RelayCommand CloseCommand { get; set; }

        public MessageWindow MyMessageWindow { get; set; }



        public MessageWindowViewModel()
        {
            MyMessageWindow = new MessageWindow();
            CloseCommand = new RelayCommand(c =>
            {
                MyMessageWindow.Close();
            });
        }


    }
}
