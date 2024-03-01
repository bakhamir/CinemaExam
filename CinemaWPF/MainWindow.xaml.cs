using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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

namespace CinemaWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public async void Button_Click(object sender, RoutedEventArgs e)
        {
            using (HttpClient client = new HttpClient())
            {
                string req = $"http://localhost:5183/Register?login={regName.Text}&password={regPwd.Text}&secretCode={admCode.Text}";
                var response = await client.GetAsync(req);
                        signLogIn authPage = new signLogIn();
                        this.Close();
                        authPage.Show();
            }
        }

        public async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            using (HttpClient client = new HttpClient())
            {
                string req = $"http://localhost:5183/Login?login={logName.Text}&password={logPwd.Text}";
                var response = await client.GetAsync(req);
                signLogIn authPage = new signLogIn(); 
                var json = await response.Content.ReadAsStringAsync();
                if (json == "true")
                {
                    this.Close();
                    authPage.Show();
                }
                else
                {
                    MessageBox.Show("Неправильные вводные данные Еррор");
                }
                       
            }

        }
    }
}
