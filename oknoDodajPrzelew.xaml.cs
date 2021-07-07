using Bank;
using System;
using System.Collections.Generic;
using System.Data.Linq;
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
using System.Windows.Shapes;

namespace WpfApplication1
{
    public partial class oknoDodajPrzelew : Window
    {
        Window okno;

        List<Przelewy> listaPrzelewów = new List<Przelewy>();
        LINQBazaBankDataContext dc = new LINQBazaBankDataContext(Bank.Properties.Settings.Default.BankConnectionString);
        private List<Klienci> listaKlientow = new List<Klienci>();
        List<int> indeksy = new List<int>();
        int idKlienta;
        public oknoDodajPrzelew(Window okno, int idKlienta)
        {
            InitializeComponent();
            this.okno = okno;
            this.  idKlienta = idKlienta;
            
            
        }
   
      
        private void dodaj_Click(object sender, RoutedEventArgs e)
        {

            decimal decimalParse1;
            int intParse1;
            if (!decimal.TryParse(textBoxKwotaPrzelewu.Text, out decimalParse1))
            {

                MessageBox.Show("Błędna kwota przelewu. Musi być liczba.");
                textBoxKwotaPrzelewu.Text = "";
            }
            else  if (!int.TryParse(TextBoxRachunekOdbiorcy.Text, out intParse1))
            {
                MessageBox.Show("Błędny numer rachunku Odbiorcy. Musi być liczba.");
                textBoxKwotaPrzelewu.Text = "";
            }
            else
            {
                var KlientDoZamiany = dc.Klienci.First(p => p.Id_klienta == idKlienta);

                if (KlientDoZamiany.Środki >= decimalParse1)
                {


                    DateTime tempData = DateTime.Now;


                    Przelewy bob = new Przelewy();
                    bob.Nazwa_odbiorcy = textBoxNazwaOdbiorcy.Text;
                    bob.Numer_rachunku_odbiorcy = intParse1;
                    bob.Kwota = decimalParse1;
                    bob.Tytuł_przelewu = textBoxTytulem.Text;
                    bob.Nadawca = idKlienta;
                    bob.Data = tempData;
                    dc.Przelewy.InsertOnSubmit(bob);
                    dc.SubmitChanges();

                    KlientDoZamiany.Środki = KlientDoZamiany.Środki - bob.Kwota;
                    dc.SubmitChanges();

                   
                   



                    MessageBox.Show("Przelew udał sie.");

                    this.Close();

                }
                else
                {
                    MessageBox.Show("Za mało środków na koncie.");
                }

            }
            

        }

        private void anuluj_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }



        private void zaznaczonytextBoxKwotaPrzelewu(object sender, RoutedEventArgs e)
        {
            textBoxKwotaPrzelewu.Foreground = Brushes.Black;
            textBoxKwotaPrzelewu.FontWeight = FontWeights.Normal;
            textBoxKwotaPrzelewu.Text = "";
        }

        private void zaznaczonytextBoxNazwaOdbiorcy(object sender, RoutedEventArgs e)
        {
            textBoxNazwaOdbiorcy.Foreground = Brushes.Black;
            textBoxNazwaOdbiorcy.FontWeight = FontWeights.Normal;
            textBoxNazwaOdbiorcy.Text = "";
        }

        private void zaznaczonyTextBoxRachunekOdbiorcy(object sender, RoutedEventArgs e)
        {
            TextBoxRachunekOdbiorcy.Foreground = Brushes.Black;
            TextBoxRachunekOdbiorcy.FontWeight = FontWeights.Normal;
            TextBoxRachunekOdbiorcy.Text = "";
        }

        private void zaznaczonytextBoxTytulem(object sender, RoutedEventArgs e)
        {
            textBoxTytulem.Foreground = Brushes.Black;
            textBoxTytulem.FontWeight = FontWeights.Normal;
            textBoxTytulem.Text = "";
        }

       

      }
}