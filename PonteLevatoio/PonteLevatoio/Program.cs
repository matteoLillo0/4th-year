/*
 * Matteo Angiolillo 4°H 2025-01-20 -> 2025-01-27 -> 2025-02-17
 * App per simulare un ponte levatoio con l'utilizzo di thread per creare il passaggio di macchine su di esso
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static System.Console;

namespace Ponte_Levatoio
{
    class Program
    {
        #region Costanti
        // costanti

        const int MAX_AUTO_SUL_PONTE = 4;  // n max auto che passano sul ponte
        const int CONSOLE_LENGTH = 115; // lunghezza max console
        const int CONSOLE_HEIGHT = 29; // altezza 

        #endregion

        #region Variabili

        // variabili

        static SemaphoreSlim _sem = new SemaphoreSlim(MAX_AUTO_SUL_PONTE); // semaforo per auto sul ponte
        static List<Thread> Waiting_Threads = new List<Thread>(); // coda auto 
        static Thread[] Active_Threads = new Thread[MAX_AUTO_SUL_PONTE]; // thread attivi
        static int auto_counter = 0; // Contatore auto
        static bool statoPonte = false; // false = giù, true = alzato
        static Object _lock = new Object(); // oggetto lock per precedenze
        static Random rnd = new Random(); // oggetto random
        static int checkLimAuto = 0; // controllo numero auto per non crashare
        static int[] pos = new int[MAX_AUTO_SUL_PONTE];

        #endregion

        #region StampaMappa()

        static void StampaMappa() // stampa la mappa
        {
            lock (_lock) // lock console
            {

                ForegroundColor = ConsoleColor.Cyan;
                WriteLine("                                           ║░░░░░░░░░░░░░░░░░░░░░░░░░░░║                                           ");
                WriteLine("                                           ║░░░░░░░░░░░░░░░░░░░░░░░░░░░║                                           ");
                WriteLine("                                           ║░░░░░░░░░░░░░░░░░░░░░░░░░░░║                                           ");
                WriteLine("                                           ║░░░░░░░░░░░░░░░░░░░░░░░░░░░║                                           ");
                WriteLine("                                           ║░░░░░░░░░░░░░░░░░░░░░░░░░░░║                                           ");
                WriteLine("                                           ║░░░░░░░░░░░░░░░░░░░░░░░░░░░║                                           ");
                WriteLine("                                           ║░░░░░░░░░░░░░░░░░░░░░░░░░░░║                                           ");
                WriteLine("                                           ║░░░░░░░░░░░░░░░░░░░░░░░░░░░║                                           ");
                WriteLine("                                           ║░░░░░░░░░░░░░░░░░░░░░░░░░░░║                                           ");
                WriteLine("                                           ║░░░░░░░░░░░░░░░░░░░░░░░░░░░║                                           ");
                WriteLine("                                           ║░░░░░░░░░░░░░░░░░░░░░░░░░░░║                                           ");
                WriteLine("                                           ║░░░░░░░░░░░░░░░░░░░░░░░░░░░║                                           ");
                WriteLine("                                           ║░░░░░░░░░░░░░░░░░░░░░░░░░░░║                                           ");
                WriteLine("                                           ║░░░░░░░░░░░░░░░░░░░░░░░░░░░║                                           ");
                WriteLine("                                           ║░░░░░░░░░░░░░░░░░░░░░░░░░░░║                                           ");
                WriteLine("                                           ║░░░░░░░░░░░░░░░░░░░░░░░░░░░║                                           ");
                WriteLine("                                           ║░░░░░░░░░░░░░░░░░░░░░░░░░░░║                                           ");
                WriteLine("                                           ║░░░░░░░░░░░░░░░░░░░░░░░░░░░║                                           ");
                WriteLine("                                           ║░░░░░░░░░░░░░░░░░░░░░░░░░░░║                                           ");
                WriteLine("                                           ║░░░░░░░░░░░░░░░░░░░░░░░░░░░║                                           ");
                WriteLine("                                           ║░░░░░░░░░░░░░░░░░░░░░░░░░░░║                                           ");
                WriteLine("                                           ║░░░░░░░░░░░░░░░░░░░░░░░░░░░║                                           ");
                WriteLine("                                           ║░░░░░░░░░░░░░░░░░░░░░░░░░░░║                                           ");
                WriteLine("                                           ║░░░░░░░░░░░░░░░░░░░░░░░░░░░║                                           ");
                WriteLine("                                           ║░░░░░░░░░░░░░░░░░░░░░░░░░░░║                                           ");
                WriteLine("                                           ║░░░░░░░░░░░░░░░░░░░░░░░░░░░║                                           ");
                WriteLine("                                           ║░░░░░░░░░░░░░░░░░░░░░░░░░░░║                                           ");
                WriteLine("                                           ║░░░░░░░░░░░░░░░░░░░░░░░░░░░║                                           ");
                WriteLine("                                           ║░░░░░░░░░░░░░░░░░░░░░░░░░░░║                                           ");
                WriteLine("                                           ║░░░░░░░░░░░░░░░░░░░░░░░░░░░║                                           ");
                ForegroundColor = ConsoleColor.White;
            }

        }
        #endregion

        #region StampaPonteAlzato()

        static void StampaPonteAlzato()
        {
            statoPonte = true;

            lock (_lock) // lock console
            {               

                SetCursorPosition(0, 12);
                Write("═════════════════════════════════════════╗                               ╔═════════════════════════════════════════");
                SetCursorPosition(41, 13);
                Write("║                               ║");
                SetCursorPosition(41, 14);
                Write("║                               ║");
                SetCursorPosition(41, 15);
                Write("║                               ║");
                SetCursorPosition(41, 16);
                Write("║                               ║");
                SetCursorPosition(41, 17);
                Write("║                               ║");
                SetCursorPosition(0, 18);
                Write("═════════════════════════════════════════╝                               ╚═════════════════════════════════════════");

                ForegroundColor = ConsoleColor.Cyan;
                for (int i = 0; i < 7; i++) // per stampare la parte alzata
                {
                    SetCursorPosition(43, 12 + i);
                    Write("║░░░░░░░░░░░░░░░░░░░░░░░░░░░║");
                }
                ForegroundColor = ConsoleColor.White; // ritorna al colore normale
            }
        }

        #endregion

        #region StampaPonteGiu()

        static void StampaPonteChiuso()
        {

            if (!statoPonte) return; // per non sovrascrivere le auto

            statoPonte = false; // bool per check passaggio auto


            lock (_lock) // lock console
            {
                SetCursorPosition(0, 12);
                Write("═══════════════════════════════════════════════════════════════════════════════════════════════════════════════════");
                SetCursorPosition(0, 13);
                Write("                                                                          ");
                SetCursorPosition(0, 14);
                Write("                                                                          ");
                SetCursorPosition(0, 15);
                Write("                                                                          ");
                SetCursorPosition(0, 16);
                Write("                                                                          ");
                SetCursorPosition(0, 17);
                Write("                                                                          ");
                SetCursorPosition(0, 18);
                Write("═══════════════════════════════════════════════════════════════════════════════════════════════════════════════════");
            }
        }

        #endregion

        #region AddAuto() 

        static void AddAuto() // aggiunge un thread auto
        {
            Thread new_thread = new Thread(Auto);
            new_thread.Name = $"Auto {auto_counter++}";
            Waiting_Threads.Add(new_thread); // in caso lo aggiunge alla lista dei thread che aspettano
            UpdateThreadList();
            new_thread.Start();
        }

        #endregion

        #region UpdateThreadList()

        static void UpdateThreadList()
        {
            lock (_lock) // lock console
            {
                for (int i = 0; i < Waiting_Threads.Count; i++) // per la lunghezza della lista dei thread che aspettano
                {
                    SetCursorPosition(5, i);
                    Write(Waiting_Threads[i].Name); // scrive il nome dei thread che aspettano
                }
                SetCursorPosition(5, Waiting_Threads.Count); // uno sotto l'altro in riga
                Write("            "); // spaziati
            }
        }

        #endregion

        #region StampaComandi

        static void StampaComandi()
        {
            lock (_lock)
            {
                SetCursorPosition(85, 1);
                ForegroundColor = ConsoleColor.Green;
                Write("Matteo Angiolillo 4°H 2025-02-17");
                ForegroundColor = ConsoleColor.White;                
                SetCursorPosition(85, 2);
                Write("Comandi:");                
                SetCursorPosition(85, 3);
                Write("<A> Aggiungi macchina");                
                SetCursorPosition(85, 4);
                Write("<O> Apri ponte");                
                SetCursorPosition(85, 5);
                Write("<C> Chiudi ponte");

            }
        }

        #endregion

        #region Auto()

        /// <summary>
        ///  Per gestire ogni auto         
        /// </summary>

        static void Auto()
        {
            
            _sem.Wait(); // aspetta
            Thread.Sleep(50); // anche i thread

            Waiting_Threads.RemoveAt(Waiting_Threads.IndexOf(Thread.CurrentThread));
            UpdateThreadList(); // aggiorna la lista dei thread in attesa


            int x = 0;
            int y = 13;

            for (int i = 0; i < MAX_AUTO_SUL_PONTE; i++)  // finchè siamo sotto alla soglia di auto
            {
                if (Active_Threads[i] is null || Active_Threads[i].ThreadState == ThreadState.Stopped) // trova il primo posto libero nei thread attivi
                {
                    y += i;
                    Active_Threads[i] = Thread.CurrentThread;
                    break;
                }
            }

            int speed = rnd.Next(50, 500); // per randomizzare la velocità

            for (int i = 0; i < CONSOLE_LENGTH - Thread.CurrentThread.Name.Length; i++)
            {
                Thread.Sleep(speed); // imposta la velocità
                pos[y - 13] = i;

                while (i == 40 - Thread.CurrentThread.Name.Length && statoPonte)
                {
                    Thread.Sleep(500);
                }

                lock (_lock) // prende il lock e scrive l'auto
                {                    
                    SetCursorPosition(x + i, y);
                    Write(' ' + Thread.CurrentThread.Name + ' ');
                }
            }

            _sem.Release(); // il semaforo rilascia
        }

        #endregion

        #region CheckAutoSulPonte()

        static bool CheckAutoSulPonte() // per controllare se si chiude il ponte con auto sopra
        {
            bool autoSulPonte = false; // controlla auto sul ponte
            for (int i = 0; i < Active_Threads.Length; i++) // scorre la lista dei thread attvii
            {
                if (Active_Threads[i] is null || Active_Threads[i].ThreadState == ThreadState.Stopped) continue; // se non ci sono thread

                int current_Thread_Name_Length = Active_Threads[i].Name.Length; 
                if (pos[i] + current_Thread_Name_Length > 40 && pos[i] + current_Thread_Name_Length < 80) autoSulPonte = true; // mette a true se trova auto sul ponte
            }
            return autoSulPonte;
        }

        #endregion

        #region Main(string[] args)

        static void Main(string[] args)
        {
            OutputEncoding = Encoding.Unicode;
            CursorVisible = false;
            Title = "Matteo Angiolillo 4°H 2025-01-27 PonteLevatoio"; // titolo console
            
            StampaMappa();            
            StampaPonteAlzato();
            StampaComandi();


            while (true)
            {
                if (KeyAvailable) 
                {
                    // lettura comando utente
                    char c = char.ToUpper(ReadKey(true).KeyChar);
                    switch (c)
                    {
                        case 'A': // clicca a per aggiungere auto
                            checkLimAuto++;
                            if (checkLimAuto >= 16)
                            {
                                break;
                            }                            
                            AddAuto();
                            break;

                        case 'C': // clicca C per abbassare ponte
                            StampaPonteChiuso();
                            break;
                        case 'O': // clicca O per alzare ponte
                            if (!CheckAutoSulPonte()) StampaPonteAlzato(); // controlla se ci sono macchine sopra
                            break;
                    }
                }
            }
            

        }
        #endregion
    }
}
