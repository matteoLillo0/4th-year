// Matteo Angiolillo 4°H 2025-03-08 Client TCP

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
using System.Net; // da aggiungere manualmente
using System.Threading; // da aggiungere manualmente
using System.Net.Sockets;
using System.CodeDom;
using System.IO; 

namespace WpfClient
{
    /// <summary>
    /// Logica di interazione per MainWindow.xaml
    /// </summary>
    /// 
    public partial class MainWindow : Window
    {
        private Socket senderSocket;

        public MainWindow()
        {
            InitializeComponent();
            ConnectToServer(); // connette con l'interfaccia 
        }

        private void ConnectToServer()
        {
            Thread clientThread = new Thread(() => // thread come nel server per non bloccare esecuzione
            {
                try
                {  
                    // dice indirizzo ip e porta del server
                    IPAddress ipAddress = IPAddress.Parse("127.0.0.1");
                    IPEndPoint remoteEP = new IPEndPoint(ipAddress, 11000);

                    // Crea la socket
                    senderSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    senderSocket.Connect(remoteEP);

                    // aggiorna ui con connessione avvenuta
                    Dispatcher.Invoke(() => txtMessaggiRicevuti.AppendText("Connesso al server.\n"));

                    while (true)
                    {
                        byte [] buffer = new byte[1024];
                        int bytesRec = senderSocket.Receive(buffer); // riceve i dati dal server

                        if (bytesRec == 0) break; // se il server chiude la connessione

                        string response = Encoding.ASCII.GetString(buffer, 0, bytesRec);
                        Dispatcher.Invoke(() => txtMessaggiRicevuti.AppendText("Server: " + response + "\n"));
                    }
                }
                catch (Exception ex)
                {
                    Dispatcher.Invoke(() => MessageBox.Show("Errore client: " + ex.Message));
                }
                finally{
                    // chiude la connessione in caso di errore 
                    senderSocket?.Shutdown(SocketShutdown.Both);
                    senderSocket?.Close();
                }
            });
            clientThread.IsBackground = true; // IMPORTANTE perchè viene terminato quando il thread main finisce 
            clientThread.Start();
        }

        private void BtnSend_Click(object sender, RoutedEventArgs e)
        {
            if (senderSocket!= null && senderSocket.Connected) // controlla la connessione al server
            {   
                string message = txtMessaggioInviare.Text;
                byte[] msgBytes = Encoding.ASCII.GetBytes(message);
                senderSocket.Send(msgBytes); // invia il messaggio

                txtMessaggiRicevuti.AppendText("(you)Client: " + txtMessaggioInviare.Text + "\n");
                txtMessaggioInviare.Clear();

            }
            else
            {
                MessageBox.Show("Non connesso al server"); // avvisa se il server non è connesso
            }
        }
    }
}
