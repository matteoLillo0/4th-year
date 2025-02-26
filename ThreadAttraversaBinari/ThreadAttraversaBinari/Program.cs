/*
 * 
 *  Matteo Angiolillo, 4°H, 2024-12-16
 *  Sviluppare un app console per simulare, con l'utilizzo dei thread, l'attraversamento di un pedona su dei binari, con treni che passano a vel random
 *
*/

using Microsoft.Win32.SafeHandles;
using System;
using System.Threading;
using static System.Console; // per velocizzare scrittura

namespace CorsaConTreni
{
    internal class Program
    {
        static Object _lock = new Object(); // lock per le risorse
        static Random _random = new Random(); // per i numeri random
        static Thread[] _treni = new Thread[2]; // thread per i treni

        #region Scrivi() metodo generico per testo

        private static void Scrivi(int sinistra, int sopra, string testo) // metodo per scrivere testo generico
        {
            lock (_lock) // lock della risorsa console
            {
                SetCursorPosition(sinistra, sopra); // imposto la posizione del cursore
                Write(testo); // scrivo il testo richiesto
            }
        }

        #endregion

        #region class Treno

        class Treno // classe per metodi e grafica treno
        {
            private int idTreno; // identificativo del treno
            private int velocita; // vel
            private string[] graficaTreno = // grafica del treno
            {
                "╔═══╗",
                "║   ║",
                "║   ║",
                "║   ║",
                "╚═══╝",
                "  |  ",
                "╔═══╗",
                "║   ║",
                "║   ║",
                "║   ║",
                "╚═══╝",
                "  |  ",
                "╔═══╗",
                "║   ║",
                "║   ║",
                "║   ║",
                "╚═══╝",
            };

            #region Costruttore

            public Treno(int n) // costruttore del treno
            {
                idTreno = n; // numero del treno
                velocita = _random.Next(50, 500); // troviamo la velocità col random
            }

            #endregion

            #region StampaTreno()

            public void StampaTreno() // metodo per stampare il treno
            {
                int x = 30 * idTreno + (6 * (idTreno - 1)), y = 0, y1; 

                while (y < 30) // fino all'height
                {
                    y1 = y;
                    foreach (var r in graficaTreno) // per ogni carattere dell'array del treno
                    {
                        if (y1 == 30)
                            break;
                        Scrivi(x, y1++, r);
                    }

                    Thread.Sleep(velocita); // così stampa (si muove) alla velocità random
                    Scrivi(x, y++, new string(' ', 5)); // metodo per scrivere
                }
            }

            #endregion

        }

        #endregion

        #region class Persona

        class Persona
        {
            public int pos { get; private set; } // posizione
            public string nome { get; private set; } // stringa nome

            private string[] graficaPersona =
            {
                @"  [] ",
                @" /||\",
                @"  /\ "
            };

            #region Costruttore

            public Persona(int pos, string name) // costruttore
            {
                this.pos = pos; // per la funzione Muovi (x)
                this.nome = name;
            }

            #endregion

            #region Muovi() 

            public void Muovi() // per muovere il personaggio
            { 
                while (pos < 115) // entro la width
                {
                    if (pos == 25 && _treni[0].IsAlive) // se passa il treno
                    {                        
                        Thread.Sleep(50); // thread si ferma
                        continue; 
                    }

                    if (pos == 60 && _treni[1].IsAlive) // se passa l'altro
                    {
                        Thread.Sleep(50); 
                        continue; 
                    }

                    for (int i = 0; i < graficaPersona.Length; i++) // stampa la persona
                    { 
                        Scrivi(pos, 10 + i, graficaPersona[i]); // prendendo i caratteri dall'array di stringhe
                    }

                    pos++; // va avanti e aspetta

                    Thread.Sleep(50);
                }
            }

            #endregion

        }

        #endregion

        #region StampaFerrovia() stampa la grafica solo writeline

        static private void StampaFerrovia() // stampa la grafica
        {
            SetCursorPosition(0, 1);
            WriteLine("-------------------------------------------------------------------------------------------------------------------------------");
            WriteLine("                             |" + new string(' ', 5) + "|Stato treno 1                |" + new string(' ', 5) + "|Stato treno 2");
            WriteLine("                             |" + new string(' ', 5) + "|                             |" + new string(' ', 5) + "|");
            WriteLine("                             |" + new string(' ', 5) + "|Is alive =                   |" + new string(' ', 5) + "|Is alive = ");
            WriteLine("                             |" + new string(' ', 5) + "|                             |" + new string(' ', 5) + "|");
            WriteLine("                             |" + new string(' ', 5) + "|                             |" + new string(' ', 5) + "|");
            WriteLine("                             |" + new string(' ', 5) + "|                             |" + new string(' ', 5) + "|");
            WriteLine("                             |" + new string(' ', 5) + "|                             |" + new string(' ', 5) + "|");            
            WriteLine("                             -" + new string(' ', 5) + "-                             -" + new string(' ', 5) + "-");
            WriteLine("                             " + new string(' ', 5) + "                                " + new string(' ', 5));
            WriteLine("                             " + new string(' ', 5) + "                                " + new string(' ', 5));
            WriteLine("                             " + new string(' ', 5) + "                                " + new string(' ', 5));
            WriteLine("                             -" + new string(' ', 5) + "-                             -" + new string(' ', 5) + "-");
            WriteLine("                             |" + new string(' ', 5) + "|                             |" + new string(' ', 5) + "|");
            WriteLine("                             |" + new string(' ', 5) + "|                             |" + new string(' ', 5) + "|");
            WriteLine("                             |" + new string(' ', 5) + "|                             |" + new string(' ', 5) + "|");
            WriteLine("                             |" + new string(' ', 5) + "|                             |" + new string(' ', 5) + "|");
            WriteLine("                             |" + new string(' ', 5) + "|                             |" + new string(' ', 5) + "|");
            WriteLine("                             |" + new string(' ', 5) + "|                             |" + new string(' ', 5) + "|");
            WriteLine("                             |" + new string(' ', 5) + "|                             |" + new string(' ', 5) + "|");
            WriteLine("                             |" + new string(' ', 5) + "|                             |" + new string(' ', 5) + "|");
            WriteLine("                             |" + new string(' ', 5) + "|                             |" + new string(' ', 5) + "|");
            WriteLine("                             |" + new string(' ', 5) + "|                             |" + new string(' ', 5) + "|");
            WriteLine("                             |" + new string(' ', 5) + "|                             |" + new string(' ', 5) + "|");
            WriteLine("                             |" + new string(' ', 5) + "|                             |" + new string(' ', 5) + "|");
            WriteLine("                             |" + new string(' ', 5) + "|                             |" + new string(' ', 5) + "|");
            WriteLine("                             |" + new string(' ', 5) + "|                             |" + new string(' ', 5) + "|");
            WriteLine("-----------------------------       -----------------------------       -----------------------------------------------");

        }

        #endregion

        #region AggiornaStato()

        static void AggiornaStato() // aggiorna stato dei thread
        {   
            bool treno0, treno1; // corrispondono all'id dei treni
            do
            {
                treno0 = _treni[0].IsAlive;  // prende lo stato
                treno1 = _treni[1].IsAlive;
                Scrivi(47, 4, treno0.ToString()); // scrive lo stato
                Scrivi(83, 4, treno1.ToString()); 
                Thread.Sleep(20);
            } while (treno0 || treno1);
        }

        #endregion

        #region Main

        static void Main(string[] args)
        {
            CursorVisible = false; // disattiva la visibilità del cursore

            #region intestazione e settaggio finestra

            // Intestazione console
            Title = "Matteo Angiolillo | 4°H | 2024-12-16 | ConsoleApp ThreadAttraversaBinari"; // titolo                       
            SetWindowSize(2000, 2000); // importante: size della finestra

            ForegroundColor = ConsoleColor.Cyan;// colore per l'intestazione
            Scrivi(0, 0, "Nome: Matteo Angiolillo             Classe: 4°H | Data: 2024-12-16          Titolo: ConsoleAppThreadAttraversaBinari");
            ForegroundColor = ConsoleColor.White; // ritorna default

            #endregion 

            Treno treno1 = new Treno(1), treno2 = new Treno(2); // istanzia dalla classe treno

            _treni[0] = new Thread(treno1.StampaTreno); // istanzia i thread
            _treni[1] = new Thread(treno2.StampaTreno);

            StampaFerrovia(); // stampa la grafica

            // partono i thread
            _treni[0].Start();
            _treni[1].Start();

            Thread stato = new Thread(AggiornaStato); // nuovo thread per gli stati
            stato.Start(); // parte

            // creazione persona

            Persona mario = new Persona(0, "Mario");
            Thread persona = new Thread(mario.Muovi); // thread persona
            persona.Start(); // parte il thread            

            #region stampa messaggio chiusura

            while (persona.IsAlive) // per aspettare che termini il thread persona
                Thread.Sleep(10);
            SetCursorPosition(30, 29); // ci si posiziona infondo e in mezzo 
            ForegroundColor = ConsoleColor.Green; // colore testo verde
            Write("Premi un tasto per chiudere il programma..."); // mess
            ForegroundColor = ConsoleColor.White; // ritorna colore default
            ReadKey(); // aspetta il tasto

            #endregion

        }

        #endregion

    }
}