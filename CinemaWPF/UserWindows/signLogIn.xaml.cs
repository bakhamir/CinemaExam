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
namespace CinemaWPF
{
    /// <summary>
    /// Логика взаимодействия для signLogIn.xaml
    /// </summary>
    public partial class signLogIn : Window
    {
        private Users currentUser;

        public signLogIn(Users user)
        {
            InitializeComponent();
            currentUser = user;
            welcome.Content= $"Добро пожаловать {currentUser.username}";
            // Используйте currentUser для отображения информации о пользователе на этой форме
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show($"Hello {currentUser.username}");
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
