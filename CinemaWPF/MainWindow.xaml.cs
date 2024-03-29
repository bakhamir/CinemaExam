using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
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
using CinemaWPF.Models;
namespace CinemaWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static Users user = new Users();
        public MainWindow()
        {
            InitializeComponent();
        }

        public async void Button_Click(object sender, RoutedEventArgs e)
        {
            using (HttpClient client = new HttpClient())
            {
                user.username = regName.Text;
                user.pwd = regPwd.Text;
                string req = $"http://localhost:5183/Register?login={user.username}&password={user.pwd}&admpass={admCode.Text}";
                string rolereq = $"http://localhost:5183/GetRole?secretCode={admCode.Text}";
                var response = await client.GetAsync(req);
                var roleResponse = await client.GetAsync(rolereq);
                var json = await roleResponse.Content.ReadAsStringAsync();
                user.accessRole = json;
                signLogIn authPage = new signLogIn(user);
                this.Close();
                authPage.Show();
            }
        }

        public async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            using (HttpClient client = new HttpClient())
            {
                user.username = logName.Text;
                user.pwd = logPwd.Text;
                string req = $"http://localhost:5183/Login?login={user.username}&password={user.pwd}";
                var response = await client.GetAsync(req);
                var json = await response.Content.ReadAsStringAsync();
                Console.WriteLine(json); 
                string rolereq = $"http://localhost:5183/UserGetRole?login={user.username}&password={user.pwd}";
                var roleresponse = await client.GetStringAsync(rolereq);

                user.accessRole = roleresponse;

                signLogIn authPage = new signLogIn(user);
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

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            AdminPanel adminPanel = new AdminPanel();
            this.Close();
            adminPanel.Show();
        }
    }
}
