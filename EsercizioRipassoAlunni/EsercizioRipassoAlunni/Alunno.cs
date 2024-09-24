/*
 * Matteo Angiolillo, 4°H, 2024-09-20
 * Struttura dati per salvare le informazioni degli alunni
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace EsercizioRipassoAlunni
{
    internal class Alunno
    { // variabili private 
        private string nome, cognome;
        private string genere;
        private DateOnly dataDiNascita;
        private string classe, indirizzo;
        

        // Costruttore
        public Alunno(string nome, string cognome, string genere, DateOnly dataDiNascita, string classe, string indirizzo)
        {
            this.nome = nome; // assegna il nome dato dall'utente ala variabile dell aclasse 
            this.cognome = cognome;
            this.genere = genere;
            this.dataDiNascita = dataDiNascita;
            this.classe = classe;
            this.indirizzo = indirizzo;
        }
        // metodi che ritornano i nomi inseriti dall'utente
        public string getNome() {  return nome; }
        public string getCognome() {  return cognome; }
        public string getGenere() {  return genere; }
        public DateOnly getDataDiNascita() {  return dataDiNascita; }
        public string getClasse() {  return classe; }
        public string getIndirizzo() {  return indirizzo; }
        
    }
}
