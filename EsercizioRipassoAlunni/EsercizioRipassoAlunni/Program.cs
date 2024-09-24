/*
 * Matteo Angiolillo, 4°H, 2024-09-20
 * Realizzare un programma che legga da file informazioni su un alunno e le salvi in una struttura dati (programmazione a oggetti non necessaria)
 * Su ogni riga del file txt sono presenti le informazioni di ciascun alunno
*/

namespace EsercizioRipassoAlunni
{
    internal class Program
    {
        const string fileInput = @"..\..\..\elenco-alunni-classi.txt";
        static List<Alunno> alunni = new List<Alunno>(); // lista di tipo "Alunno" (Classe che abbiamo creato), per salvare ogni alunno
        static int contatoreAlunno = 0;
        static void LeggiFileAlunno(string input)
        {
            Alunno alunno = null;
            using(StreamReader sr = new StreamReader(input))
            {
                while (!sr.EndOfStream)
                {
                    string[] dati = sr.ReadLine().Split("\t");

                    contatoreAlunno++; // per capire a che riga ci troviamo per ritornare un errore + preciso

                    if (dati.Length != 6) // controlla che ogni alunno abbia tutti i dati presenti
                    {
                        Console.ForegroundColor = ConsoleColor.Red; // in tutti gli errori la console andrà a scriverli in rossso
                        Console.WriteLine($"Errore, all'alunno n°{contatoreAlunno} mancano dei dati");
                        Console.ForegroundColor = ConsoleColor.White;
                        continue;
                    }
                    if (dati[2] != "F" && dati[2] != "M") // controlla che i dati del genere siano corretti
                    {   
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"ERRORE: Il genere dell'alunno: {dati[0]}, {dati[1]} non è valido");
                        Console.ForegroundColor = ConsoleColor.White;
                        continue;
                    }

                    DateOnly data_di_nascita_letta; // conversione e lettura della data di nascita per inserirla poi nel costruttore

                    if (!DateOnly.TryParse(dati[3], out data_di_nascita_letta))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"Errore: La data di nascita dell'alunno: {dati[0]}, {dati[1]}, non è valida");
                        Console.ForegroundColor = ConsoleColor.White;
                        continue;
                    }

                    alunni.Add(new Alunno(dati[0], dati[1], dati[2], data_di_nascita_letta, dati[4], dati[5]));     
                }
            }
        }

        static void Main(string[] args) // main esecuzione delle funzioni di prima
        {
            Console.Title = "| Registro Alunni | Matteo Angiolillo | 4H | 2024-09-20 |"; // intestazione
            Console.ForegroundColor= ConsoleColor.Cyan;
            Console.WriteLine(" Registro Alunni | Matteo Angiolillo | 4H | 2024-09-20 |\n" +
                              " Lo scopo del programma è quello di salvare in una struttura di dati, una lista di alunni da un file .txt\n--------------------------------------------\n Premi invio per continuare...\n");
            Console.ForegroundColor = ConsoleColor.White;
            Console.ReadKey();

            LeggiFileAlunno(fileInput); // lettura da file

            foreach (Alunno alunno in alunni) // stampa per debug
            {
                Console.WriteLine("{0}\t{1}\t{2}\t{3}\t{4}\t{5}", // formattazione stringa compatta
                    alunno.getNome(), alunno.getCognome(),
                    alunno.getDataDiNascita(), alunno.getGenere(),
                    alunno.getClasse(), alunno.getIndirizzo());                   
                    
            }

        }
    }
}
