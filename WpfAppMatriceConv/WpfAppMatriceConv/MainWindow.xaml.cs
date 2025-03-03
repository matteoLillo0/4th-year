// Matteo Angiolillo 4°H 2025-03-03 Realzziare una wpf app per gestire i filtri a un'immagine in modo asincrono con una matrice di convoluzione

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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

namespace WpfAppMatriceConv
{
    /// <summary>
    /// Logica di interazione per MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region MainWindow() e globali

        public MainWindow()
        {
            InitializeComponent();
        }

        private System.Drawing.Bitmap imgOriginale; // immagine originale

        #endregion

        #region btnCaricaFoto_Click

        private void btnCaricaFoto_Click(object sender, RoutedEventArgs e)
        {
            string _fileName; // stringa per il percorso

            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog(); // crea la dlg box
            dlg.DefaultExt = ".png";
            dlg.Filter = "Image Files(*.jpg; *.jpeg; *.png; *.gif; *.tiff; *.webp)| *.jpg; *.jpeg; *.png; *.gif; *.tiff; *.webp"; // filtro per le ext

            bool? result = dlg.ShowDialog(); // in caso di successo

            if (result == true) // mostran la foto e la mette in wpf
            {
                _fileName = dlg.FileName;
                BitmapImage bitmap = new BitmapImage(new Uri(_fileName)); // bitmap con l'img
                lblNomeFile.Content = _fileName; // percorso per la label
                imgFoto.Source = bitmap;
                imgOriginale = new System.Drawing.Bitmap(_fileName);

            }
        }

        #endregion

        #region Convoluzione

        private System.Drawing.Bitmap Convoluzione(System.Drawing.Bitmap img, float[,] matrice) // fuzione convoluzione
        {
            System.Drawing.Bitmap imgRis = new System.Drawing.Bitmap(img.Width, img.Height);
            for (int i = 1; i<img.Width - 1; i++) // cicla per la width
            {
                for(int j = 1; j<img.Height - 1; j++) // e cicla per l'height
                {
                    imgRis.SetPixel(i, j, CalcolaConvoluzione(img, i, j, matrice)); // calcola la convoluzione del pixel
                }
            }

            return imgRis; // ritorna l'immagine trasformata
        }

        #endregion

        #region CalcolaConvoluzione

        private System.Drawing.Color CalcolaConvoluzione(System.Drawing.Bitmap img, int x, int y, float[,] matrice)
        {
            float R = 0, G = 0, B = 0; // prende gli rgb

            for (int i = 0; i < 3; i++) // cicla per la matrice
            {
                for(int j = 0; j< 3; j++)
                {
                    System.Drawing.Color pixel = img.GetPixel(x + i - 1, y + j - 1);
                    R += pixel.R * matrice[i, j]; // rgb per i pixel della matrice
                    G += pixel.G * matrice[i, j];
                    B += pixel.B * matrice[i, j];
                }
            }

            // normalizza per non uscire dai valori
            R = Math.Min(Math.Max(R, 0), 255);
            G = Math.Min(Math.Max(G, 0), 255);
            B = Math.Min(Math.Max(B, 0), 255);

            // Restituiamo il nuovo colore risultante dalla convoluzione
            return System.Drawing.Color.FromArgb(255, (int)R, (int)G, (int)B);

        }

        #endregion

        #region ControllaFiltroTesti()

        // per controllare l'input
        private bool ControllaTxtFiltri()
        {
            try
            {
                bool result = true; // istanzia il res a true

                foreach (TextBox item in grid_filtri.Children) // cicla su ogni oggetto della grid (ogni txtBox)
                {

                    double value;
                    result = double.TryParse(item.Text, out value); // prova il parse di ogni input
                    if (!result)  // se fallisce
                    {
                        // se fallisce lo dice all'utente
                        MessageBox.Show($"Un filtro che hai inserito non è un numero, cambialo.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                        break;
                    }
                }
                return result; // ritorna il result, anche se uno solo fallisce è fallito il result
            }
            catch (Exception ex)
            {
                return false; // prende l'eccezione e ritorna il risultato a false perchè sicuramente un risultato è sbagliato
            }            
        }

        #endregion
        
        #region btnTrasforma_Click()

        private async void btnTrasforma_Click(object sender, RoutedEventArgs e) // metodo asyncrono
        {
            if (imgOriginale != null) // se è stata caricata un'immagine
            {

                if (!ControllaTxtFiltri()) return; // se non funziona lancia l'errore
                
                
                // crea la matrice dalla txtBox
                float[,] matrice = new float[3, 3]
                {
                    { float.Parse(txt00.Text), float.Parse(txt01.Text), float.Parse(txt02.Text) },
                    { float.Parse(txt10.Text), float.Parse(txt11.Text), float.Parse(txt12.Text) },
                    { float.Parse(txt20.Text), float.Parse(txt21.Text), float.Parse(txt22.Text) }
                };

                // Applica la convoluzione
                System.Drawing.Bitmap imgTrasformata = Convoluzione(imgOriginale, matrice);

                // Converte limmagine per farla vedere da wpf
                System.IO.MemoryStream ms = new System.IO.MemoryStream(); // salva l'immagine nel flusso di memoria
                imgTrasformata.Save(ms, System.Drawing.Imaging.ImageFormat.Png); // in formato png
                ms.Seek(0, System.IO.SeekOrigin.Begin); // ci puntiamo all'inizio del flusso

                // crea la bitmapimage per caricarla dal ms
                BitmapImage bitmapResult = new BitmapImage();
                bitmapResult.BeginInit();
                bitmapResult.StreamSource = ms; // prende la sourse dal flusso di memoria
                bitmapResult.EndInit();

                // carica nell'ui
                imgFoto.Source = bitmapResult;
            }
            else
            {   // messaggio di errore
                MessageBox.Show("Carica prima un'immagine.", "Errore", MessageBoxButton.OK, MessageBoxImage.Error);
            }            
        }
        #endregion
    }
}
