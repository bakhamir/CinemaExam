using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

		public movie(int id_, string actors_, string genres_, int seanceId_, string title_, string about_, string writer_, string movieTime_)
		{
			this.id = id_;
			this.actors = actors_;
			this.genres = genres_;
			this.seanceId = seanceId_;
			this.title = title_;
			this.about = about_;
			this.writer = writer_;
			this.movieTime = movieTime_;
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
