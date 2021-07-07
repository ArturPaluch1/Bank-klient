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
    /// <summary>
    /// Logika interakcji dla klasy OknoKlienta.xaml
    /// </summary>
    public partial class OknoKlienta : Window
    {
        LINQBazaBankDataContext dc = new LINQBazaBankDataContext(Bank.Properties.Settings.Default.BankConnectionString);
        private List<Klienci> listaKlientow = new List<Klienci>();
        private List<Kredyty> listaKredytow = new List<Kredyty>();
        private List<Lokaty> listaLokat = new List<Lokaty>();
        private List<Przelewy> listaPrzelewow = new List<Przelewy>();
      
        int idKlienta;
        public OknoKlienta(int id_klienta)
        {
            idKlienta = id_klienta;
            InitializeComponent();
    
                wczytajBaze("Klienci");
                wczytajBaze("Kredyty");
                wczytajBaze("Lokaty");
                wczytajBaze("Przelewy");
            




        }

        public void wczytajBaze(string tabela)
        {
            switch (tabela)
            {
                case "Klienci":
                    {
                        dc.Refresh(RefreshMode.OverwriteCurrentValues, dc.Klienci);
                        var zapytanie = dc.ExecuteQuery<Klienci>("select [Id klienta],[imię] ,nazwisko,[telefon],[miasto], ulica,[środki]from [klienci] where [id klienta] = "+ idKlienta + "");
                        listaKlientow.Clear();

                        foreach (Klienci item in zapytanie)
                        {
                           
                            listaKlientow.Add(item);
                        }
                        dataGridKlient.ItemsSource = null;
                        dataGridKlient.ItemsSource = listaKlientow;



                        break;
                    }
                case "Kredyty":
                    {
                        dc.Refresh(RefreshMode.OverwriteCurrentValues, dc.Kredyty);
                        var zapytanie = dc.ExecuteQuery<Kredyty>("select [Id kredytu],[aktywny],[Data założenia], [kwota kredytu] ,klient,[rodzaj kredytu],[kredytu udzielił]from [kredyty] where klient = " + idKlienta + "");
                        listaKredytow.Clear();

                        foreach (Kredyty item in zapytanie)
                        {

                            if(item.aktywny.Equals(true))
                            {
                               
                                listaKredytow.Add(item);
                            }
                          
                        }
                        dataGridKlienciKredyt.ItemsSource = null;
                        dataGridKlienciKredyt.ItemsSource = listaKredytow;





                        


                        break;
                    }

                case "Lokaty":
                    {
                        dc.Refresh(RefreshMode.OverwriteCurrentValues, dc.Lokaty);
                        listaLokat.Clear();
                        var zapytanie = dc.ExecuteQuery<Lokaty>("select [Id lokaty],[Data założenia],[wysokość lokaty] ,klient,[id rodzaju lokaty],[lokaty udzielił]from [lokaty] where klient = " + idKlienta + "");


                        foreach (Lokaty item in zapytanie)
                        {
                            if (item.aktywny.Equals(true))
                            {
                               
                                listaLokat.Add(item);
                            }

                              
                           
                        }
                        dataGridKlienciLokata.ItemsSource = null;
                        dataGridKlienciLokata.ItemsSource = listaLokat;




                        break;
                    }
                case "Przelewy":
                    {
                        dc.Refresh(RefreshMode.OverwriteCurrentValues, dc.Przelewy);
                        listaPrzelewow.Clear();
                        var zapytanie = dc.ExecuteQuery<Przelewy>("select [Id Przelewu],[Kwota],[Nazwa odbiorcy],[Nadawca],[Tytuł przelewu] from Przelewy where nadawca = " + idKlienta + "");


                        foreach (Przelewy item in zapytanie)
                        {
                           
                            listaPrzelewow.Add(item);
                        }
                        dataGridKlienciPrzelew.ItemsSource = null;
                        dataGridKlienciPrzelew.ItemsSource = listaPrzelewow;

 break;
                    }

            }


        }

       

        private void button_Click_wyjdz(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void button_Click_wyloguj(object sender, RoutedEventArgs e)
        {
            MainWindow okno = new MainWindow();
            okno.Show();
            this.Close();

        }

        private void ButtonPrzelewClick(object sender, RoutedEventArgs e)
        {
            oknoDodajPrzelew okno1 = new oknoDodajPrzelew(this,idKlienta);
            okno1.Show();

            okno1.Closed += oknoClosed;
        }

        public void oknoClosed(object sender, System.EventArgs e)
        {
                            wczytajBaze("Przelewy");
                        
                  

            }

        private void FocusKonto(object sender, RoutedEventArgs e)
        {
            wczytajBaze("Klienci");
        }
    }
}
