using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Bank;

namespace WpfApplication1
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
       
        LINQBazaBankDataContext dc = new LINQBazaBankDataContext(Bank.Properties.Settings.Default.BankConnectionString);

        public MainWindow()
        {
            InitializeComponent();

        }
        private void zalogujButtonClick(object sender, RoutedEventArgs e)
        {
            try
            {
                Klienci zalogowanyKlient = dc.Klienci.ToList().Where(
                 a => textBoxImie.Text.TrimEnd().ToLower() == a.Imię.TrimEnd().ToLower()
                 && textBoxNazwisko.Text.TrimEnd().ToLower().Equals(a.Nazwisko.TrimEnd().ToLower())
                 && PasswordBox.Password.TrimEnd().Equals(a.Password.TrimEnd()
                 )
                     ).FirstOrDefault();

                if (zalogowanyKlient != null)
                {

                    if (zalogowanyKlient.aktywny == true)
                    {
                        OknoKlienta okno1 = new OknoKlienta((zalogowanyKlient as Klienci).Id_klienta);
                        okno1.Show();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Podany klient jest nieaktywny");
                    }


                }
                else
                {
                    MessageBox.Show("Błędne dane logowania");
                }
            }
            catch
            {
                MessageBox.Show("Błąd logowania do bazy danych.");

            }

          

        }

    
    }
}

