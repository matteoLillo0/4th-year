/*
    Matteo Angiolillo, 4°H, 2024-11-04, Realizzare un programma che implementi l'uso di thread, nel realizzare una corsa tra omini --> 2024-11-12 -> 2024-11-25
*/

using System;
using System.ComponentModel;
using System.Text;
using System.Threading;
using static System.Console; // per semplificare la scrittura dei Write e Read


namespace ConsoleAppThreadCorsaNET
{
    class Program
    {
        static int posAndrea = 0; // corrisponde alla colonna 0, la colonna di partenza, la colonna finale è la 115 (0->115)
        static int posBaldo = 0;
        static int posCarlo = 0;
        static int classifica = 0; // non usata al momento
        static string comando = "";
        static object _lockCons = new object(); // serve per gestire chi accede alle risorse comuni, per evitare i conflitti

        // dichiaro e istanzio i thread, per farli correre tutti assieme

        static Thread thAndrea;
        static Thread thBaldo;
        static Thread thCarlo;


        #region Pronti() -> stampa iniziale
        static void Pronti() // funzione per visualizzare la posizione di partenza;
        {
            // stampa omino Andrea

            SetCursorPosition(posAndrea, 3); // iniziamo a stampare l'omino
            Write("  []");
            SetCursorPosition(posAndrea, 4);
            Write(@" /██\"); ;
            SetCursorPosition(posAndrea, 5);
            Write(@"  ┘└");

            // stampa omino Baldo

            SetCursorPosition(posBaldo, 8); // iniziamo a stampare l'omino
            Write("  ()");
            SetCursorPosition(posBaldo, 9);
            Write(@" -██-");
            SetCursorPosition(posBaldo, 10);
            Write(@"  ╝╚");

            //stampa omino Carlo

            SetCursorPosition(posCarlo, 13); // iniziamo a stampare l'omino
            Write("  <>");
            SetCursorPosition(posCarlo, 14);
            Write(@" -▓▓-");
            SetCursorPosition(posCarlo, 15);
            Write(@"  /\");
        }

        #endregion

        #region static void Andrea() -> animazione Andrea

        static void Andrea()
        {
            int vel = 50; // velocità personaggio

            do 
            {
                // gestore del join (aspetta)
                if(comando.Length == 3)
                    if (comando[1] == 'J' && comando[0] == 'A')
                        switch (comando[2])
                        {
                            case 'B':
                                thBaldo.Join();
                                break;

                            case 'C':
                                thCarlo.Join();
                                break;

                            default:
                                break;
                        }

                posAndrea++;
                Thread.Sleep(vel); // serve per gestire la velocità dell'animazione

                lock (_lockCons) // questa istruzione richiede il testimone, se il testimone ce l'ha lo prende e va avanti, se il lock è occupato si ferma, quind può essere bloccante
                {
                    Stato();
                    SetCursorPosition(posAndrea, 5);
                    Write(@"  ┘└"); ; // gambe
                }
                Thread.Sleep(vel);
                lock (_lockCons)
                {

                    Stato();
                    SetCursorPosition(posAndrea, 4);
                    Write(@" /██\"); // braccia
                }

                Thread.Sleep(vel);
                lock (_lockCons)
                {

                    Stato();
                    SetCursorPosition(posAndrea, 3);
                    Write(@"  []"); // testa
                }

            } while (posAndrea < 115); // il ciclo si ripete fino a che non si arriva alla fine della finestra

            // il primo che arriva si prende la sua posizione incrementando la variabile, gli altri di seguito

            lock (_lockCons) // lock per accedere alla risorsa console
            {
                Stato();
                classifica++;
                SetCursorPosition(115, 2);
                Write(classifica);

            }
        }

        #endregion

        #region static void Baldo() -> animazione Baldo

        static void Baldo()
        {
            int vel = 50;
            do
            {
                if (comando.Length == 3)
                    if (comando[1] == 'J' && comando[0] == 'B')
                        switch (comando[2])
                        {
                            case 'A':
                                thAndrea.Join();
                                break;

                            case 'C':
                                thCarlo.Join();
                                break;

                            default:
                                break;
                        }
                posBaldo++;
                Thread.Sleep(vel); // serve per gestire la velocità dell'animazione
                lock (_lockCons)
                {
                    Stato();
                    SetCursorPosition(posBaldo, 10);
                    Write(@"  ╝╚"); // gambe
                }
                Thread.Sleep(vel);
                lock (_lockCons)
                {
                    Stato();
                    SetCursorPosition(posBaldo, 9);
                    Write(@" ┌██┐"); // braccia
                }
                Thread.Sleep(vel);
                lock (_lockCons)
                {
                    Stato();
                    SetCursorPosition(posBaldo, 8);
                    Write("  ()"); // testa
                }

            } while (posBaldo < 115); // il ciclo si ripete fino a che non si arriva alla fine della finestra

            lock (_lockCons)
            {
                Stato();
                classifica++;
                SetCursorPosition(115, 7);
                Write(classifica);

            }
        }

        #endregion

        #region static void Carlo() -> animazione Carlo

        static void Carlo()
        {
            int vel = 30;
            do
            {
                if (comando.Length == 3)
                    if (comando[1] == 'J' && comando[0] == 'C')
                        switch (comando[2])
                        {
                            case 'B':
                                thBaldo.Join();
                                break;

                            case 'A':
                                thAndrea.Join();
                                break;

                            default:
                                break;
                        }

                posCarlo++;
                Thread.Sleep(vel); // serve per gestire la velocità dell'animazione
                lock (_lockCons)
                {
                    Stato();
                    SetCursorPosition(posCarlo, 15);
                    Write(@"  /\"); // gambe
                }
                Thread.Sleep(vel);
                lock (_lockCons)
                {
                    Stato();
                    SetCursorPosition(posCarlo, 14);
                    Write(@" -▓▓-"); // braccia
                }
                Thread.Sleep(vel);
                lock (_lockCons)
                {
                    Stato();
                    SetCursorPosition(posCarlo, 13);
                    Write("  <>"); // testa
                }

            } while (posCarlo < 115); // il ciclo si ripete fino a che non si arriva 


            lock (_lockCons)
            {
                Stato();
                classifica++;
                SetCursorPosition(115, 12);
                Write(classifica);
            }
        }

        #endregion

        #region static void Stato() -> stampa lo stato dei thread e se sono attivi
        static void Stato()
        {
            // stato di Andrea
            SetCursorPosition(0, 1);
            Write("Andrea -> " + thAndrea.ThreadState + "                               ");
            SetCursorPosition(50, 1);
            Write("Is alive -> " + thAndrea.IsAlive + "                               ");

            // stato di Baldo
            SetCursorPosition(0, 7);
            Write("Baldo -> " + thBaldo.ThreadState + "                               ");
            SetCursorPosition(50, 7);
            Write("Is alive -> " + thBaldo.IsAlive + "                               ");

            // stato di Carlo
            SetCursorPosition(0, 12);
            Write("Carlo -> " + thCarlo.ThreadState + "                               ");
            SetCursorPosition(50, 12);
            Write("Is alive -> " + thCarlo.IsAlive + "                               ");
        }

        #endregion

        #region static void stampaMenu() -> stampa il menu sotto i corridori

        static void StampaMenu(string message = "MENU", int offset = 0) // in questo modo usiamo i paramentri opzionali 
        {
            SetCursorPosition(offset + 0, 20);
            Write(message);
            SetCursorPosition(offset + 0, 22);
            Write("Andrea (A)");
            SetCursorPosition(offset + 0, 23);
            Write("Baldo  (B)");
            SetCursorPosition(offset + 0, 24);
            Write("Carlo  (C)");
        }

        #endregion

        #region static void ClearMenuAzioni() -> cancella il menu azioni
        static void ClearMenuAzioni()
        {
            SetCursorPosition(33, 20);
            Write("                                                     ");
            SetCursorPosition(33, 22);
            Write("                                                     ");
            SetCursorPosition(33, 23);
            Write("                                                     ");
            SetCursorPosition(33, 24);
            Write("                                                     ");
            SetCursorPosition(33, 25);
            Write("                                                     ");
        }

        #endregion

        #region AccettaComandi()

        static void AccettaComandi()
        {
            Thread thAzione; // per gestire su che thread lavorare 
            comando = "";
            char choice = ' ';

            // leggo il comando

            choice = ReadKey(true).KeyChar;
            choice = char.ToUpper(choice);

            // salvo il thread scelto dall'utente in thAzione

            switch (choice) 
            {
                case 'A':
                    thAzione = thAndrea;
                    break;
                case 'B':
                    thAzione = thBaldo;
                    break;
                case 'C':
                    thAzione = thCarlo;
                    break;
                default:
                    return;
            }

            comando += choice;

            // legge azione da interpretare

            choice = StampaMenuAzioni("Azione su " + thAzione.Name);
            switch (choice)
            {
                case 'S': // sospende il thread
                    lock (_lockCons)
                        if(thAzione.ThreadState.Equals(ThreadState.Running) || thAzione.ThreadState.Equals(ThreadState.WaitSleepJoin))
                            thAzione.Suspend();
                    break;

                case 'R': // riprende il thread
                    if (thAzione.ThreadState.Equals(ThreadState.Suspended))
                            thAzione.Resume();
                    break;

                case 'A': // uccide il thread
                    lock (_lockCons)
                    {
                        if(thAzione.ThreadState.Equals(ThreadState.Running) || thAzione.ThreadState.Equals(ThreadState.WaitSleepJoin))
                            thAzione.Abort();
                    }                       
                    break;

                case 'J': // con il join gestiamo un thread che aspetta l'altro

                    comando += choice;
                    StampaMenu(message: "chi deve aspettare?", offset: 60);
                    choice = char.ToUpper(ReadKey(true).KeyChar);
                    comando += choice;
                    break;

                default:
                    return;
            }
            lock (_lockCons)
                ClearMenuAzioni(); // dopo averlo utilizzato cancelliamo il menu azioni per pulire la console

        }

        #endregion

        #region static char StampaMenuAzioni(string titolo) -> stampa menu azioni per il thread spec

        static char StampaMenuAzioni(string titolo)
        {
            char c;
            lock (_lockCons)

            {
                SetCursorPosition(33, 20);
                Write(titolo);
                SetCursorPosition(33, 22);
                Write("Sospendere (S)");
                SetCursorPosition(33, 23);
                Write("Riprendere (R)");
                SetCursorPosition(33, 24);
                Write("Abort      (A)");
                SetCursorPosition(33, 25);
                Write("Aspetta    (J)");
            }

            // leggi input
            c = ReadKey(true).KeyChar;
            return char.ToUpper(c);

        }

        #endregion

        #region Main()

        static void Main(string[] args)
        {
            OutputEncoding = Encoding.Unicode;
            CursorVisible = false;

            #region intestazione
            Title = "Corsa Thread | Matteo Angiolillo 4°H | 2024-11-12";
            ForegroundColor = ConsoleColor.Cyan;
            WriteLine("Matteo Angiolillo | 4°H | 2024-11-12 | Programma che simula una corsa utilizzando il concetto dei thread");
            ForegroundColor = ConsoleColor.White;
            #endregion 

            Pronti(); // visualizza la grafica di partenza

            // Dichiaro e istanzio i thread

            thAndrea = new Thread(Andrea);
            thAndrea.Name = "Andrea";
            thBaldo = new Thread(Baldo);
            thBaldo.Name = "Baldo";
            thCarlo = new Thread(Carlo);
            thCarlo.Name = "Carlo";

            // stampa lo stato

            Stato();           
            SetCursorPosition(0, 19);
            Write("premere invio per partire..."); // qui siamo prima della partenza
            ReadLine();
            SetCursorPosition(0, 20);
            Write("             ");

            // la corsa è partita

            StampaMenu();

            // L'ordine di start non rispecchia l'ordine di arrivo, a causa dello scheduler

            thAndrea.Start(); // il metodo start non è bloccante, quindi possono partire assieme
            thBaldo.Start();
            thCarlo.Start();

            do
            {                
                lock (_lockCons)
                {                    
                    StampaMenu();
                }
                if (Console.KeyAvailable) AccettaComandi();
            } while (thAndrea.IsAlive || thBaldo.IsAlive || thCarlo.IsAlive);

            thAndrea.Join();
            thBaldo.Join();
            thCarlo.Join();

            Stato();

            ReadLine();
        }

        #endregion
    }
}