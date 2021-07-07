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
            var zapytanie =
from c in dc.Klienci
select new { c.Id_klienta, c.Imię, c.Nazwisko, c.Password,c.aktywny };

            bool zalogowany = false;
                foreach (var item in zapytanie)
                {
                    if (item.Imię.TrimEnd().Equals(textBoxImie.Text) && item.Nazwisko.TrimEnd().Equals(textBoxNazwisko.Text) && item.Password.TrimEnd().Equals(PasswordBox.Password)&&item.aktywny!=false)
                    {
                        OknoKlienta okno1 = new OknoKlienta(item.Id_klienta);
                    zalogowany = true;
                        okno1.Show();
                        this.Close();


                    }
                }
           if(zalogowany.Equals(false))
                MessageBox.Show("Nieprawidłowe dane logowania.");

            



            
           
    





        }

    
    }
}

