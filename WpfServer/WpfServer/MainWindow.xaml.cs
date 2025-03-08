// Matteo Angiolillo 4°H 2025-03-08 Server TCP

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
using System.Net; // da aggiungere
using System.Threading; // da aggiungere
using System.Net.Sockets;
using System.CodeDom; // da aggiungere

namespace WpfServer
{
    /// <summary>
    /// Logica di interazione per MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Socket listener; // socket che ascolta in ingresso
        private Socket handler; // socket che gestisce le comunicazioni con il client
        public MainWindow()
        {
            InitializeComponent();
            StartServer(); // avvia il server con l'interfaccia
        }
        private void StartServer()
        {
            Thread serverThread = new Thread(() => // thread per non bloccare l'interfaccia e quindi paralelliza server e interfaccia
            {
                try
                {
                    IPHostEntry host = Dns.GetHostEntry("127.0.0.1"); // ottiene l'ip del server (localhost)
                    IPAddress ipAddress = host.AddressList[0];
                    IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 11000);

                    // Crea il socket per le connesioni tcp
                    listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    listener.Bind(localEndPoint);  // primitiva bind che associa il socket alla porta 
                    listener.Listen(10); // inizia ad ascoltare le connessioni

                    Dispatcher.Invoke(() => txtMessaggiRicevuti.AppendText("Server in ascolto...\n")); // aggiorna l'ui

                    // accetta una conessione dal client
                    handler = listener.Accept();
                    Dispatcher.Invoke(() => txtMessaggiRicevuti.AppendText("Client connesso.\n")); 

                    while (true) 
                    {
                        byte[] buffer = new byte[1024];
                        int bytesRec = handler.Receive(buffer); // primitiva Receive che riceve i dati dal client
                        if (bytesRec == 0) break;

                        string data = Encoding.ASCII.GetString(buffer, 0, bytesRec); 
                        Dispatcher.Invoke(() => txtMessaggiRicevuti.AppendText("Client: " + data + "\n")); // aggiorna l'ui con i messaggi del client

                        // Risponde al client confermando che ha ricevuto
                        byte[] response = Encoding.ASCII.GetBytes("Ricevuto: " + data); 
                        handler.Send(response); // primitiva send invia la risposta
                    }

                }
                catch (Exception e)
                {
                    Dispatcher.Invoke(() => MessageBox.Show("Errore server: " + e.Message));
                }
                finally
                {   // chiude il socket se ci sono errori
                    handler?.Shutdown(SocketShutdown.Both); // nullable
                    handler?.Close();
                    listener?.Close();
                }
            });            

            serverThread.IsBackground = true; // IMPORTANTE perchè viene terminato quando il thread main finisce 
            serverThread.Start();
        }

        private void BtnSend_Click(object sender, RoutedEventArgs e)
        {
            if (handler != null && handler.Connected) // verifica il client sia connesso
            {
                string message = txtMessaggioInviare.Text; // legge il messaggio da inviare
                byte[] msgBytes = Encoding.ASCII.GetBytes(message); // lo converte in byte
                handler.Send(msgBytes); // primitiva send e lo manda

                txtMessaggiRicevuti.AppendText("(you)Server: " + message + "\n");  // lo aggiunge alla casella dei messaggi ricevuti
                txtMessaggioInviare.Clear(); // pulisce la casella di input

            }
            else
            {
                MessageBox.Show("nessun client connesso"); // messaggio di errore 

            }
        }
    }
}
