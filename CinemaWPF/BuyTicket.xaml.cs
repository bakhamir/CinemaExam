using System;
using System.Collections.Generic;
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
using CinemaWPF.Models;
using System.Data.SqlClient;
namespace CinemaWPF
{
    /// <summary>
    /// Логика взаимодействия для BuyTicket.xaml
    /// </summary>
    public partial class BuyTicket : Window
    {
        Users currentUser;
        seance currentSeance;
        public BuyTicket(Users user,seance seance,movie movie)
        {
            InitializeComponent();
            welcome.Content = $"Здравствуйте {user.username} {seance.id}";
            info.Content = $"Подтвердить";
            currentUser= user;
            currentSeance = seance;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            currentUser.ticketid = currentSeance.id;

            MessageBox.Show("purchase succesfull");
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
            signLogIn menu = new signLogIn(currentUser);
            menu.Show();
        }
    }
}
