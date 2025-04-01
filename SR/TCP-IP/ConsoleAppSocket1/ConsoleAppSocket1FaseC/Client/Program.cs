using System;
using System.Net;
using System.Net.Sockets;
using System.Text;


namespace Client
{
    public class SocketClient
    {
        public static void Main(string[] args)
        {
            StartClient();
            return;
        }

        public static void StartClient()
        {
            byte[] bytes = new byte[1024];

            try
            {
                // Ricaviamo un IP da utilizzare per la connessione (127.0.0.1)
                IPHostEntry host = Dns.GetHostEntry("localhost");
                IPAddress ipAddress = IPAddress.Parse("10.73.0.24");

                IPEndPoint remoteEP = new IPEndPoint(ipAddress, 11000); // Porta alla quale collegarsi
                Socket sender = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp); // Genera un socket

                try
                {
                    // Connessione alla porta per richidere un servizio
                    sender.Connect(remoteEP);

                    Console.WriteLine("Socket connected to {0}", sender.RemoteEndPoint.ToString());

                    // Il messaggio da inviare e il numero di byte inviati e ricevuti
                    byte[] msg = Encoding.ASCII.GetBytes("Antonio sei un Surgo! <EOF>");
                    int bytesSent = sender.Send(msg);
                    int bytesRec = sender.Receive(msg);

                    Console.WriteLine("Echoed test = {0}", Encoding.ASCII.GetString(bytes, 0, bytesRec));

                    // Chiusura corretta del collegamento
                    sender.Shutdown(SocketShutdown.Both);
                    sender.Close();
                }

                // Gestione delle eccezioni
                catch (ArgumentNullException ane)
                {
                    Console.WriteLine("ArgumentNullException : {0}", ane.ToString());
                }
                catch (SocketException se)
                {
                    Console.WriteLine("SocketException : {0}", se.ToString());
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            Console.WriteLine("\n Press any key to continue...");
            Console.ReadKey();
        }
    }
}
