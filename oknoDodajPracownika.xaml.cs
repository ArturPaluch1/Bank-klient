using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
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

using System.Data.OleDb;
using Bank;

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for oknoDodaj.xaml
    /// </summary>
    /// 

        ////////do zrobienia
    public partial class OknoDodajPracownika : Window
    {
        Window okno;

        List<Pracownik> listaPracownikow;
        LINQBazaBankDataContext dc = new LINQBazaBankDataContext(Bank.Properties.Settings.Default.BankConnectionString);

        public OknoDodajPracownika(Window okno)
        {
            listaPracownikow = new List<Pracownik>();
            InitializeComponent();

            this.okno = okno;


        }

        private void dodaj_Click(object sender, RoutedEventArgs e)
        {



            if (textBoxImie.Text != "" && textBoxNazwisko.Text != "" && textBoxImie.Text != "Musisz uzupełnić" && textBoxNazwisko.Text != "Musisz uzupełnić" && PasswordBoxHaslo.Password != "" && PasswordBoxHaslo.Password != "Musisz uzupełnić" && textBoxTelefon.Text != "" && textBoxTelefon.Text != "Musisz uzupełnić" && textBoxPESEL.Text != "" && textBoxPESEL.Text != "Musisz uzupełnić" && textBoxWynagrodzenie.Text != "" && textBoxWynagrodzenie.Text != "Musisz uzupełnić")
            {

                DateTime tempData = DateTime.Now;
                string data = tempData.ToShortDateString();




                LINQBazaBankDataContext dc = new LINQBazaBankDataContext(Bank.Properties.Settings.Default.BankConnectionString);



                Pracownicy bob = new Pracownicy();
                bob.Imię_pracownika = textBoxImie.Text.TrimEnd();
                bob.Nazwisko_pracownika = textBoxNazwisko.Text.TrimEnd();
                bob.Password = PasswordBoxHaslo.Password.ToString().TrimEnd();
                bob.PESEL = int.Parse(textBoxPESEL.Text);
                bob.Data_zatrudnienia = tempData;
                bob.Wynagrodzenie = Decimal.Parse(textBoxWynagrodzenie.Text);
                bob.Telefon = int.Parse(textBoxTelefon.Text);
                



                dc.Pracownicy.InsertOnSubmit(bob);
                dc.SubmitChanges();




                MessageBox.Show("Dodawanie powiodło się.");

                this.Close();

            }

            else
            {



                if (textBoxImie.Text == "")
                {
                    textBoxImie.Foreground = Brushes.Red;
                    textBoxImie.FontWeight = FontWeights.Bold;
                    textBoxImie.Text = "Musisz uzupełnić";
                }
                if (textBoxNazwisko.Text == "")
                {
                    textBoxNazwisko.Foreground = Brushes.Red;
                    textBoxNazwisko.FontWeight = FontWeights.Bold;
                    textBoxNazwisko.Text = "Musisz uzupełnić";
                }
                if (PasswordBoxHaslo.Password == "")
                {
                    PasswordBoxHaslo.Foreground = Brushes.Red;
                    PasswordBoxHaslo.FontWeight = FontWeights.Bold;
                    PasswordBoxHaslo.Password = "Musisz uzupełnić";
                }
                if (textBoxTelefon.Text == "")
                {
                    textBoxTelefon.Foreground = Brushes.Red;
                    textBoxTelefon.FontWeight = FontWeights.Bold;
                    textBoxTelefon.Text = "Musisz uzupełnić";
                }
                if (textBoxPESEL.Text == "")
                {
                    textBoxPESEL.Foreground = Brushes.Red;
                    textBoxPESEL.FontWeight = FontWeights.Bold;
                    textBoxPESEL.Text = "Musisz uzupełnić";
                }
                if (textBoxWynagrodzenie.Text == "")
                {
                    textBoxWynagrodzenie.Foreground = Brushes.Red;
                    textBoxWynagrodzenie.FontWeight = FontWeights.Bold;
                    textBoxWynagrodzenie.Text = "Musisz uzupełnić";
                }

            }




        }

        private void anuluj_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


        private void zaznaczonyTextboxImie(object sender, RoutedEventArgs e)
        {
            textBoxImie.Foreground = Brushes.Black;
            textBoxImie.FontWeight = FontWeights.Normal;
            textBoxImie.Text = "";
        }
        private void zaznaczonyTextboxNazwisko(object sender, RoutedEventArgs e)
        {
            textBoxNazwisko.Foreground = Brushes.Black;
            textBoxNazwisko.FontWeight = FontWeights.Normal;
            textBoxNazwisko.Text = "";
        }

        private void zaznaczonyPasswordBoxHaslo(object sender, RoutedEventArgs e)
        {
            PasswordBoxHaslo.Foreground = Brushes.Black;
            PasswordBoxHaslo.FontWeight = FontWeights.Normal;
            PasswordBoxHaslo.Password = "";
        }

        private void zaznaczonyTextboxTelefon(object sender, RoutedEventArgs e)
        {
            textBoxTelefon.Foreground = Brushes.Black;
            textBoxTelefon.FontWeight = FontWeights.Normal;
            textBoxTelefon.Text = "";
        }

        private void zaznaczonyTextboxPESEL(object sender, RoutedEventArgs e)
        {
            textBoxPESEL.Foreground = Brushes.Black;
            textBoxPESEL.FontWeight = FontWeights.Normal;
            textBoxPESEL.Text = "";
        }

        private void zaznaczonyTextboxWynagrodzenie(object sender, RoutedEventArgs e)
        {
            textBoxWynagrodzenie.Foreground = Brushes.Black;
            textBoxWynagrodzenie.FontWeight = FontWeights.Normal;
            textBoxWynagrodzenie.Text = "";
        }

    }
}
