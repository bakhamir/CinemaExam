using CinemaWPF.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Shapes;
using static System.Net.Mime.MediaTypeNames;

namespace CinemaWPF
{
    /// <summary>
    /// Логика взаимодействия для SeanceWindow.xaml
    /// </summary>
    public partial class SeanceWindow : Window
    {
        public ObservableCollection<seance> Seances { get; set; }
        movie currentMovie;
        Users currentUser;
        public SeanceWindow(movie movie, Users user)
        {
            InitializeComponent();
            currentMovie = movie;
            currentUser = user;
            Available.Content = $"ДОСТУПНЫЕ CЕАНСЫ НА ФИЛЬМ - {movie.title}";
            Info.Content = $"В РОЛЯХ - {currentMovie.actors} РЕЖИСЕР - {currentMovie.writer} СИНОПСИС - {currentMovie.about} ЖАНР - {currentMovie.genres} ДЛИТЕЛЬНОСТЬ - {currentMovie.movieTime}";
            Seances = new ObservableCollection<seance>();
            LoadSeances();
            // Установка контекста данных окна
            DataContext = this;
        }
        private async void LoadSeances()
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync($"http://localhost:5183/GetSeanceByMovie?SeanceId={currentMovie.seanceId}");
                if (response.IsSuccessStatusCode)
                {
 
                    string jsonContent = await response.Content.ReadAsStringAsync();
 
                    var seances = JsonConvert.DeserializeObject<List<seance>>(jsonContent);
                    foreach (var seance in seances)
                    {
                        Seances.Add(seance);  
                    }

                }
            }
        }
        private void BuyTicket_Click(object sender, RoutedEventArgs e)
        {

            // Проверяем, выбран ли какой-то элемент
            if (SEANCES.SelectedItem != null)
            {
                // Преобразуем выбранный элемент к типу Seance
                seance selectedSeance = (seance)SEANCES.SelectedItem;
                BuyTicket buy = new BuyTicket(currentUser, selectedSeance, currentMovie);
                buy.ShowDialog();
                // Здесь можно выполнить операции по покупке билета для выбранного сеанса
                // Например, можно открыть новое окно для покупки билета и передать туда данные о выбранном сеансе
                // Или вызвать другие методы для обработки покупки билета
            }
        }

    }
}
