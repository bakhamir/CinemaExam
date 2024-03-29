using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace CinemaWPF
{
    /// <summary>
    /// Логика взаимодействия для AdminPanel.xaml
    /// </summary>
    public partial class AdminPanel : Window
    {
        string imagePath = "";
        public AdminPanel()
        {
            InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            using (HttpClient client = new HttpClient())
            {
                string req = $"http://localhost:5183/AddSeance?price={price.Text}&seatings={seatings.Text}&seanceTime={time.Text}";
                var response = await client.GetAsync(req);
                var json = await response.Content.ReadAsStringAsync();
                if (json == "1")
                {
                    MessageBox.Show("Seance added");
                }
                else
                {
                    MessageBox.Show("Error something went wrong");
                }
            }

        }
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.jpg, *.jpeg, *.png, *.bmp) | *.jpg; *.jpeg; *.png; *.bmp";

            if (openFileDialog.ShowDialog() == true)
            {
                // Получаем путь к выбранному файлу изображения
                imagePath = openFileDialog.FileName;
                imgShow.Content= openFileDialog.FileName;
            }

        }
        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            using (HttpClient client = new HttpClient())
            {
                // Создаем multipart/form-data контент
                MultipartFormDataContent formData = new MultipartFormDataContent();

                // Добавляем текстовые поля в контент
                formData.Add(new StringContent(actors.Text), "actors");
                formData.Add(new StringContent(genres.Text), "genres");
                formData.Add(new StringContent(seanceId.Text), "seanceId");
                formData.Add(new StringContent(title.Text), "title");
                formData.Add(new StringContent(about.Text), "about");
                formData.Add(new StringContent(writer.Text), "writer");
                formData.Add(new StringContent(movieTime.Text), "movieTime");

                // Если изображение выбрано, добавляем его в контент
                if (!string.IsNullOrEmpty(imagePath))
                {
                    byte[] imageData = File.ReadAllBytes(imagePath);
                    ByteArrayContent imageContent = new ByteArrayContent(imageData);
                    formData.Add(imageContent, "image", System.IO.Path.GetFileName(imagePath));
                }

                // Отправляем запрос POST на сервер
                HttpResponseMessage response = await client.PostAsync("http://localhost:5183/AddMovie", formData);

                // Обрабатываем ответ от сервера
                string json = await response.Content.ReadAsStringAsync();
                if (json == "1")
                {
                    MessageBox.Show("Movie added");
                }
                else
                {
                    MessageBox.Show("Error: something went wrong");
                }
            }
        }


    }
}
