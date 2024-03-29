    using System;
    using System.Collections.Generic;
using System.IO;
using System.Linq;
    using System.Net.Http;
using System.Net.Http.Json;
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
    using CinemaWPF.ViewModels;
        using Newtonsoft.Json;


namespace CinemaWPF
    {
        /// <summary>
        /// Логика взаимодействия для signLogIn.xaml
        /// </summary>
        public partial class signLogIn : Window
        {
            private Users currentUser;
            private MovieViewModel viewModel;
            public signLogIn(Users user)
            {
                InitializeComponent();
                currentUser = user;

                LoadMovies("http://localhost:5183/GetAllMovies"); // Загружаем данные о фильмах 
          
            }
        private async void LoadMovies(string query)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(query);

                    if (response.IsSuccessStatusCode)
                    {
                        string jsonContent = await response.Content.ReadAsStringAsync();
                        var movies = JsonConvert.DeserializeObject<List<movie>>(jsonContent);
                        foreach (var movie in movies)
                        {
                          
                            CreatePosters(new List<movie> { movie });
                        }
                    }
                    else
                    {
                        MessageBox.Show("Не удалось загрузить данные о фильмах");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Произошла ошибка: {ex.Message}");
                }
            }
        }
  

        private void CreatePosters(List<movie> movies)
        {
                  foreach (var movie in movies)
                {
              
                    // Создаем элемент Image и загружаем изображение из массива байтов
                    Image image = new Image();
                    BitmapImage bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    bitmap.StreamSource = new MemoryStream(movie.ImageData); // Предполагается, что ImageData содержит массив байтов изображения
                    bitmap.EndInit();
                    image.Source = bitmap;
                    image.Width = 150; // Установите желаемую ширину
                    image.Height = 200; // Установите желаемую высоту
                         image.Margin = new Thickness(10);    // Добавляем элемент Image в Grid (или другой контейнер)
                    image.MouseLeftButtonUp += (sender, e) => ShowMovieDetails(movie);

                Posters.Children.Add(image); // Замените YourGrid на имя вашего контейнера, в котором нужно отобразить изображение
                }
        }
        private void ShowMovieDetails(movie movie)
        {
           
            SeanceWindow detailsWindow = new SeanceWindow(movie,currentUser);
            detailsWindow.ShowDialog(); // Отображаем окно как диалоговое
        }


        private void Button_Click(object sender, RoutedEventArgs e)
            {
                if (currentUser.accessRole == "admin")
                {
                    AdminPanel panel = new AdminPanel();
                    this.Close();
                    panel.Show();
                }
                else
                {
                    MessageBox.Show("отказано в доступе");
                }
            }

            private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
            {

            }

            private void Button_Click_1(object sender, RoutedEventArgs e)
            {
                Posters.Children.Clear();
                LoadMovies($"http://localhost:5183/GetMoviesByTitle?title={movieName.Text}");
            }
        }
    }
