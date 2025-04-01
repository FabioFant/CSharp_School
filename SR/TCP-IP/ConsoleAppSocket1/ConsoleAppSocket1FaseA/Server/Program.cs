using System;
using System.Net;
using System.Net.Sockets;
using System.Text;


namespace Server
{
    public class SocketListener
    {
        public static void Main(string[] args)
        {
            StartServer();
            return;
        }

        public static void StartServer()
        {
            // Stabilire l'IP per la connesione (localhost --> 127.0.0.1 o ::1)
            IPHostEntry host = Dns.GetHostEntry("localhost");

            // Prende il primo indirizzo dalla lista di IP restituiti
            IPAddress ipAddress = host.AddressList[0];

            // Definisce il punto di accesso con la porta, quindi
            // L'endpoit (IP:Porta)
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 11000);
            try
            {
                // Genera una socket
                // Le primite da passare sono: Il tipo di indirizzo (v4 o v6); Il tipo di comunicazione (stream significa connesso); Il tipo di protocollo.
                Socket listener = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

                // Associa il socket all'endpoint
                listener.Bind(localEndPoint);

                // Il socket passa da chiuso in ascolto, 10 sono le richieste max eseguibili
                listener.Listen(10);

                Console.WriteLine("Waiting for a connection...");

                // Una volta atteso lo stabilimento di una connessione, si crea un nuovo socket attivo
                Socket handler = listener.Accept();

                // Dati che saranno ricevuti
                string data = "";

                // Buffer che conterrà di volta in volta 1 byte ricevuti
                byte[] bytes = null;

                // Ciclo di ricezione: riceve finchè non si raggiunge "<EOF>" all'interno del messaggio del client
                while (true)
                {
                    // Alloca 1024 byte di buffer
                    bytes = new byte[1024];

                    // Riceve i byte dal socket, "bytesRec" indica il numero di byte arrivati
                    int bytesRec = handler.Receive(bytes);

                    // Converte 1 byte ricevuti in stringa ASCII e li concatena a "data"
                    data += Encoding.ASCII.GetString(bytes, 0, bytesRec);

                    // Se la stringa accumulata contiene "<EOF>", usciamo dal ciclo
                    if (data.IndexOf("<EOF>") > -1)
                    {
                        break;
                    }
                }

                // Stampa a video il testo ricevuto (compreso "<EOF>")
                Console.WriteLine("Text received : {0}", data);

                // Prepara la risposta (in questo caso, rimanda indietro la stessa stringa ricevuta)
                byte[] msg = Encoding.ASCII.GetBytes(data);

                // Invia i byte di risposta al client
                handler.Send(msg);

                // Chiude la connessione in modo ordinato (niente più letture ne scritture)
                handler.Shutdown(SocketShutdown.Both);
                handler.Close();
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
