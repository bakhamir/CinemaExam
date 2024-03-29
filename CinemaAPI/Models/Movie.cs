using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace CinemaWPF.Models
{
	public class Users
	{
		public int id { get; set; }
		public string username { get; set; }
		public string pwd { get; set; }
		public string accessRole { get; set; }
		public int ticketid { get; set; }
		public Users(int id_, string username_, string pwd_, string accessRole_, int ticketid_)
		{
			this.id = id_;
			this.username = username_;
			this.pwd = pwd_;
			this.accessRole = accessRole_;
			this.ticketid = ticketid_;
		}
	}
    public class movie
    {
        public int id { get; set; }
        public string actors { get; set; }
        public string genres { get; set; }
        public int seanceId { get; set; }
        public string title { get; set; }
        public string about { get; set; }
        public string writer { get; set; }
        public string movieTime { get; set; }
        [JsonProperty("ImageData")]
        public byte[] ImageData { get; set; }
        [JsonProperty("ImageName")]
        public string ImageName { get; set; }
		 
        public movie(int id, string actors, string genres, int seanceId, string title, string about, string writer, string movieTime, byte[] ImageData, string ImageName)
        {
            this.id = id;
            this.actors = actors;
            this.genres = genres;
            this.seanceId = seanceId;
            this.title = title;
            this.about = about;
            this.writer = writer;
            this.movieTime = movieTime;
            this.ImageData = ImageData;
            this.ImageName = ImageName;
        }
    }
    public class seance
	{
		public int id { get; set; }
		public int price { get; set; }
		public int seatings { get; set; }
		public string seanceTime { get; set; }

		public seance(int id_, int price_, int seatings_, string seanceTime_)
		{
			this.id = id_;
			this.price = price_;
			this.seatings = seatings_;
			this.seanceTime = seanceTime_;
        }
        public seance()
        {
            // Добавьте значения по умолчанию для полей
            this.id = 0;
            this.price = 0;
            this.seatings = 0;
            this.seanceTime = "";
        }
    }
	public class places
	{
		public int id { get; set; }
		public int seanceId { get; set; }
		public bool isTaken { get; set; }

		public places(int id_, int seanceId_, bool isTaken_)
		{
			this.id = id_;
			this.seanceId = seanceId_;
			this.isTaken = isTaken_;
		}
	}
	public class ticket
	{
		public int id { get; set; }
		public int seanceId { get; set; }
		public int movieId { get; set; }
		public string movieName { get; set; }
		public int price { get; set; }

		public ticket(int id_, int seanceId_, int movieId_, string movieName_, int price_)
		{
			this.id = id_;
			this.seanceId = seanceId_;
			this.movieId = movieId_;
			this.movieName = movieName_;
			this.price = price_;
		}
	}
 
}
