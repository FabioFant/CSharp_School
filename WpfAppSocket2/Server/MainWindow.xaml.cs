using System.Net;
using System.Net.Sockets;

using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Server
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Socket handler;
        Socket listener;

        public MainWindow()
        {
            InitializeComponent();
            tbox_eccezione.Text = "";
        }

        private async void btn_start_Click(object obj, RoutedEventArgs e)
        {
            await Task.Run(() =>
            {
                Dispatcher.Invoke(new Action(() => { btn_start.IsEnabled = false; }));

                IPHostEntry host = Dns.GetHostEntry("localhost");
                IPAddress ipAddress = host.AddressList[0];
                IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 11000);

                try
                {
                    Socket listener = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                    listener.Bind(localEndPoint);
                    listener.Listen(10);

                    Dispatcher.Invoke(new Action(() => { tbox_eccezione.Text = "In attesa della connessione..."; }));
                    handler = listener.Accept();
                    Dispatcher.Invoke(new Action(() => {
                        btn_invia.IsEnabled = true;
                        tbox_eccezione.Text = "Connessione stabilita.";
                    }));

                    while (true)
                    {
                        byte[] bytes = new byte[1024];
                        int bytesRec = handler.Receive(bytes);
                        string data = Encoding.ASCII.GetString(bytes, 0, bytesRec);
                        Dispatcher.Invoke(new Action(() => { tbox_risposta.Text = data; }));

                        if (data.IndexOf("<EOF>") > -1)
                        {
                            break;
                        }
                    }

                    handler.Shutdown(SocketShutdown.Both);
                    handler.Close();

                    Dispatcher.Invoke(new Action(() =>
                    {
                        btn_start.IsEnabled = true;
                        btn_invia.IsEnabled = false;
                        tbox_eccezione.Text = "Connessione terminata.";
                    }));
                    
                }
                catch (Exception ex)
                {
                    Dispatcher.Invoke(new Action(() => { 
                        tbox_eccezione.Text = ex.Message;
                        btn_start.IsEnabled = true;
                        btn_invia.IsEnabled = false;   
                    }));
                }
            });
        }

        private void btn_invia_Click(object obj, RoutedEventArgs e)
        {
            byte[] bytes = new byte[1024];

            try
            {
                byte[] msg = Encoding.ASCII.GetBytes(tbox_messaggio.Text);
                int bytesSent = handler.Send(msg);
            }
            catch (Exception ex)
            {
                tbox_eccezione.Text = ex.Message;
            }
        }
    }
}