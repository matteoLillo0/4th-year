// Matteo Angiolillo 4°H 2025-03-21 testo della consegna su "Consegna.txt"
// Funzione per validare il form

$('#registrationForm').on('submit', function(e) {

    e.preventDefault(); // Blocca l'invio del form

    let isValid = true; // per controllare errori

    // reset errori
    $('.error').hide(); // all'inizio errori nascosti
    $('.error').text(''); // reset testi errori

    // valida nome e cognome
    let name = $('#name').val();
    let namePattern = /^[A-Za-zà-ùÀ-Ù\s]+$/; // Solo lettere e spazi

    if (!name.match(namePattern) || name.length < 3 || name.length > 50) // controlla matcha il pattern e la length
    {
        isValid = false; // blocca e dice non è valido
        $('#nameError').text('Nome e Cognome devono contenere solo lettere e spazi, e tra 3 e 50 caratteri.'); // messaggio di errore
        $('#nameError').show(); // mostra errore
    }

    // valida data di nascita 

    let birthDate = $('#birthDate').val();
    let birthDateObj = new Date(birthDate);
    let today = new Date();
    let minAgeDate = new Date(today.getFullYear() - 120, today.getMonth(), today.getDate()); // L'utente deve avere almeno 120 anni

    // controllo nella console
    console.log("Data di nascita: ", birthDate);
    console.log("Oggi: ", today);
    console.log("Minimo data di nascita: ", minAgeDate);

    if (!birthDate || birthDateObj > today) // controlla non sia nel futuro
    {
        isValid = false;
        $('#birthDateError').text('La data di nascita non può essere futura.');
        $('#birthDateError').show(); // mostra errore
    } 
    else if (birthDateObj < minAgeDate) // controlla sia vivo :D
    {
        isValid = false;
        $('#birthDateError').text('L\'utente deve essere vivo (massimo 120 anni).');
        $('#birthDateError').show(); // mostra errore
    }

    // valida mail
    let email = $('#email').val();
    let emailPattern = /^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,6}$/;

    if (!email.match(emailPattern)) // se l'email matcha col pattern
    {
        isValid = false;
        $('#emailError').text('Inserisci una email valida.');
        $('#emailError').show(); // mostra errore
    }

    // valida nome utente
    let username = $('#username').val();

    if (username.length < 3 || username.length > 20 || !/^[A-Za-z0-9_]+$/.test(username)) // controllo per lunghezza e matching pattern
    {
        isValid = false;
        $('#usernameError').text('Il nome utente deve essere tra 3 e 20 caratteri e contenere solo lettere, numeri o underscore.');
        $('#usernameError').show(); // mostra errore
    }

    // valida password
    let password = $('#password').val();
    let passwordPattern = /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,20}$/; // password pattern

    if (!password.match(passwordPattern)) // controlla password pattern
    {
        isValid = false;
        $('#passwordError').text('La password deve essere lunga tra 8 e 20 caratteri, con almeno una lettera maiuscola, una minuscola e un numero.');
        $('#passwordError').show(); // mostra errore
    }

    if (isValid) // SE E' VALIDO
    {
        // mostra i dati validi
        $('#confirmName').text(name);
        $('#confirmBirthDate').text(birthDate);
        $('#confirmEmail').text(email);
        $('#confirmUsername').text(username);
        $('#confirmation').show();
    }
});

// per confermare
$('#confirmBtn').on('click', function()
{
    alert('Dati confermati!'); // alert dati confermati
    $('#registrationForm')[0].reset();
    $('#confirmation').hide();
});

