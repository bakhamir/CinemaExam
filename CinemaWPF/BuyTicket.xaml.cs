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
using System.Net.Http;

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
            currentUser = user;
            currentSeance = seance;
            welcome.Content = $"Здравствуйте {user.username} {seance.id}";
            if (seance.seatings == 0)
            {
                MessageBox.Show("ВСЕ МЕСТА ЗАНЯТЫ");
                this.Close();
                signLogIn menu = new signLogIn(currentUser);
                menu.Show();
            }
            else
            {
                info.Content = $"Мест свободно {seance.seatings}";
  
            }
         
            

        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            currentUser.ticketid = currentSeance.id;
            using (HttpClient client = new HttpClient())
            {
                string req = $"http://localhost:5183/UpdateUserTicketId?id={currentSeance.id}&userid={currentUser.id}";
                var response = await client.GetAsync(req);
                var json = response.Content.ReadAsStringAsync();
                string Sreq = $"http://localhost:5183/DecreaseSeats";
                var Sresponse = await client.GetAsync(Sreq);
                var Sjson = Sresponse.Content.ReadAsStringAsync();
                if (json != null && Sjson != null)
                {
                    currentSeance.seatings--;
                    MessageBox.Show("purchase succesfull");
                    this.Close();
                    signLogIn menu = new signLogIn(currentUser);
                    menu.Show();
                }
                else
                {
                    MessageBox.Show("something went wrong");
                }
            }

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
            signLogIn menu = new signLogIn(currentUser);
            menu.Show();
        }

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            using (HttpClient client = new HttpClient())
            {
                string req = $"http://localhost:5183/ReturnTicket?seanceid={currentSeance.id}&userid={currentUser.id}";
                var response = client.GetAsync(req);
                if (response != null)
                {
                    MessageBox.Show("RETURNED");
                }
                else
                {
                    MessageBox.Show("err something went wrong");
                }
            }
          
        }
    }
}
