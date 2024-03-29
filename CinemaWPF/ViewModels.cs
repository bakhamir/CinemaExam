using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CinemaWPF.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace CinemaWPF.ViewModels
{
    public class MovieViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<movie> movies;

        public ObservableCollection<movie> Movies
        {
            get { return movies; }
            set
            {
                movies = value;
                OnPropertyChanged(nameof(Movies));
            }
        }

        public MovieViewModel()
        {
            Movies = new ObservableCollection<movie>();
        }

        // Метод для добавления фильма в коллекцию
        public void AddMovie(movie movie)
        {
            Movies.Add(movie);
        }

        // Метод для удаления фильма из коллекции
        public void RemoveMovie(movie movie)
        {
            Movies.Remove(movie);
        }

        // Событие изменения свойств
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
