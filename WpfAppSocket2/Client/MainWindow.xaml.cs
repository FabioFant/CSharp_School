using System.Net;
using System.Net.Sockets;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;
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
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Socket sender;
        Socket listener;

        public MainWindow()
        {
            InitializeComponent();
            tbox_eccezione.Text = "";
        }

        private async void btn_start_Click(object obj, RoutedEventArgs e)
        {
            await Task.Run(() => {
                try
                {
                    Dispatcher.Invoke(new Action(() => { btn_start.IsEnabled = false; }));

                    IPHostEntry host = Dns.GetHostEntry("localhost");
                    IPAddress ipAddress = host.AddressList[0];

                    IPEndPoint remoteEP = new IPEndPoint(ipAddress, 11000);
                    sender = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

                    try
                    {
                        Dispatcher.Invoke(new Action(() => { tbox_eccezione.Text = "Connessione al server..."; }));
                        sender.Connect(remoteEP);
                        Dispatcher.Invoke(new Action(() => {
                            btn_invia.IsEnabled = true;
                            tbox_eccezione.Text = "Connessione stabilita.";
                        }));

                        while (true)
                        {
                            byte[] bytes = new byte[1024];
                            int bytesRec = sender.Receive(bytes);
                            string data = Encoding.ASCII.GetString(bytes, 0, bytesRec);
                            Dispatcher.Invoke(new Action(() => { tbox_risposta.Text = data; }));

                            if (data.IndexOf("<EOF>") > -1)
                            {
                                break;
                            }
                        }

                        sender.Shutdown(SocketShutdown.Both);
                        sender.Close();

                        Dispatcher.Invoke(new Action(() =>
                        {
                            btn_start.IsEnabled = true;
                            btn_invia.IsEnabled = false;
                            tbox_eccezione.Text = "Connessione terminata.";
                        }));
                    }
                    catch(ArgumentNullException ane)
                    {
                        Dispatcher.Invoke(new Action(() => { 
                            tbox_eccezione.Text = "ArgumentNullException : " + ane.ToString();
                            btn_start.IsEnabled = true;
                            btn_invia.IsEnabled = false;
                        }));
                    }
                    catch(SocketException se)
                    {
                        Dispatcher.Invoke(new Action(() => { 
                            tbox_eccezione.Text = "SocketException : {0}" + se.ToString();
                            btn_start.IsEnabled = true;
                            btn_invia.IsEnabled = false;
                        }));
                    }
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
                int bytesSent = sender.Send(msg);
            }
            catch(Exception ex)
            {
                tbox_eccezione.Text = ex.Message;
            }
        }
    }
}