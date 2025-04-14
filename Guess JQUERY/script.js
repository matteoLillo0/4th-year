// Matteo Angiolillo 4°H 2025-02-28 Script per gioco di dadi indovinare i numeri casuali

// controllare nei form che ci siano solo numeri e che siano accettabili rispetto al credito che hai 

// Inizializza il saldo

// PROGRAMMA GESTITO CON JQUERY
let balance = 100;

$('#lanciaDadiBtn').on('click', gioca);

function gioca() {
    

    const numeroScommesso = parseInt($('#number').val());
    const scommessaSoldi = parseInt($('#scommessaSoldi').val());

    // controlli input
    if (isNaN(numeroScommesso) || numeroScommesso < 2 || numeroScommesso > 12) { // per il numero
        alert("Per favore, scegli un numero valido tra 2 e 12."); // messaggio di errore
        return;
    }

    if (isNaN(scommessaSoldi) || scommessaSoldi < 1 || scommessaSoldi > balance) { // e per la scommessa
        alert("La puntata deve essere un numero valido e non può superare il saldo disponibile."); // messaggio di errore
        return;
    }

    // Genera i numeri casuali dei due dadi
    const dadoUno = Math.floor(Math.random() * 6) + 1;
    const dadoDue = Math.floor(Math.random() * 6) + 1;
    const sum = dadoUno + dadoDue;

    // Visualizza il risultato dei dadi
    const risultato = $('#risultato');
    risultato.html(`<div class="result-details">Hai lanciato i dadi! Il risultato è: <strong>${dadoUno} + ${dadoDue} = ${sum}</strong></div>`);

    // Aggiungi il messaggio di vittoria o sconfitta
    let message = '';
    if (numeroScommesso === sum) { 
        balance += scommessaSoldi;  // L'utente vince
        risultato.removeClass('lose-message').addClass('win-message');  // Cambia il colore in verde
        message = `<div>Congratulazioni! Hai indovinato! Il tuo saldo ora è: <strong>${balance}€</strong></div>`; // crea il div per la vittoria
    } else {
        balance -= scommessaSoldi;  // L'utente perde
        risultato.removeClass('win-message').addClass('lose-message');  // Cambia il colore in rosso
        message = `<div>Purtroppo, non hai indovinato. Il tuo saldo ora è: <strong>${balance}€</strong></div>`; // crea il div per sconfitta
    }

    // Aggiungi il messaggio finale di vittoria/sconfitta sotto al risultato
    risultato.append(message);

    // Aggiorna il saldo visualizzato
    $('#balance').text(`Saldo attuale: ${balance}`);

    // Mostra il bottone per il prestito se il saldo è zero
    if (balance <= 0) {
        $('#btnPrestitoContainer').show(); // Mostra il bottone
    }
}

function resetGame() {
    // Reset del saldo e nascondi il bottone del prestito
    balance = 100;
    $('#btnPrestitoContainer').hide(); // Nascondi il bottone

    // Resetta il risultato del gioco
    const risultato = $('#risultato');
    risultato.html(''); // Rimuove eventuali messaggi di vittoria/sconfitta

    // Resetta anche le classi di colore (rimuovendo win-message e lose-message)
    risultato.removeClass('win-message lose-message');

    // Aggiorna il saldo
    $('#balance').text(`Saldo attuale: ${balance}`);
}
