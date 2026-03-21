# Progetti 4° Anno (2024-2025) - ITT Blaise Pascal

Questo repository raccoglie i progetti pratici e le esercitazioni sviluppati durante il quarto anno di studi all'ITT Blaise Pascal. L'obiettivo principale di questa raccolta è consolidare i fondamenti dello sviluppo software, spaziando dalla programmazione di sistema e concorrente fino allo sviluppo di interfacce web, in preparazione al futuro percorso universitario in Ingegneria e Scienze Informatiche.

## 💻 Stack Tecnologico

L'ambiente di sviluppo si concentra principalmente sull'ecosistema .NET e sulle tecnologie web front-end:
* **C# (76.3%)**: Utilizzato per la logica applicativa, la programmazione orientata agli oggetti (OOP), la gestione del multithreading, la programmazione di rete con i socket e lo sviluppo di interfacce grafiche tramite WPF (Windows Presentation Foundation).
* **HTML5 & CSS3 (19%)**: Strutturazione e styling delle pagine web, con l'integrazione di framework come Bootstrap per garantire un design responsivo.
* **JavaScript & jQuery (4.7%)**: Aggiunta di dinamicità lato client, manipolazione del DOM in tempo reale e validazione dei dati.

## 📂 Panoramica dei Progetti

Di seguito un'analisi tecnica delle directory presenti nel repository:

### Sviluppo Web (Front-End)
* **`Sito personale`**: Sviluppo di un sito web statico per mettere in pratica i concetti avanzati di markup e stilizzazione UI.
* **`Form Validation`**: Implementazione di logiche di validazione lato client su moduli web. Il progetto sfrutta Bootstrap per la struttura grafica e script in jQuery per intercettare gli eventi di submit e controllare i dati inseriti dall'utente prima dell'invio al server.
* **`Guess JQUERY`**: Un mini-gioco interattivo che sfrutta le funzioni di jQuery per la manipolazione del DOM, la gestione dello stato della partita e il binding degli eventi di gioco.

### Sviluppo Desktop e OOP (C#)
* **`EsercizioRipassoAlunni`**: Esercitazione focalizzata sulla Programmazione Orientata agli Oggetti (OOP). Approfondisce l'implementazione delle classi, l'incapsulamento dei dati e l'istanziazione degli oggetti.
* **`WpfAppMatriceConv`**: Progetto desktop basato su WPF. Esplora il layout basato su XAML e la gestione degli eventi UI per l'elaborazione di dati strutturati (matrici).

### Programmazione Concorrente e Reti (C#)
* **`ThreadAttraversaBinari`**, **`ThreadCorsaNet`** e **`PonteLevatolo`**: Serie di applicazioni dedicate allo studio approfondito del multithreading in C#. Questi progetti affrontano i problemi classici della programmazione concorrente: la sincronizzazione dei processi, l'uso dei *lock* o dei semafori per la gestione della memoria condivisa, e la prevenzione delle *race conditions*.
* **`WpfServer`**: Un'applicazione di rete che implementa la comunicazione tramite Socket. Il progetto utilizza un'interfaccia grafica WPF per gestire la connessione, mettersi in ascolto su specifiche porte e scambiare stream di dati simulando un'architettura client-server.

## ⚙️ Come eseguire i progetti
* **Progetti C#/WPF**: È necessario disporre di **Visual Studio** con i carichi di lavoro per lo "Sviluppo per desktop .NET" installati. Basta aprire il file `.sln` relativo e avviare il debug.
* **Progetti Web**: I progetti in HTML/JS sono puramente client-side. È sufficiente navigare nella rispettiva cartella e aprire il file `index.html` (o equivalente) con qualsiasi browser web moderno.
