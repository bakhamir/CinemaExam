using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using CinemaWPF.Models;
using Dapper;
using static Dapper.SqlMapper;
using System.Collections.Generic;
using System.Data;
using System.Security.AccessControl;

namespace CinemaAPI.Controllers
{
    public class CinemaController : Controller
    {
        private readonly string conStr = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=CinemaDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        private readonly string adminCode = "iwasbornintheusa";
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet("/Login")]
        public async Task<bool> Login(string login, string password)
        {
            using (SqlConnection db = new SqlConnection(conStr))
            {
                DynamicParameters p = new DynamicParameters();
                p.Add("login", login);
                p.Add("pwd", password);

                // Используем QueryFirstOrDefault вместо Query
                var res = await db.QueryFirstOrDefaultAsync("pUser", p, commandType: System.Data.CommandType.StoredProcedure);

                // Проверяем, была ли получена хотя бы одна строка
                if (res != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        [HttpGet("/GetRole")]
        public async Task<string> GetRole(string secretCode)
        {
            if (secretCode == adminCode)
            {
                return "admin";
            }
            else
            {
                return "user";
            }
        }

        [HttpGet("/UserGetRole")]
        public async Task<string> UserGetRole(string login, string password)
        {
            string accessRole = "";
            using (var connection = new SqlConnection(conStr))
            {
                DynamicParameters p = new DynamicParameters();
                p.Add("login", login);
                p.Add("pwd", password);
                accessRole = connection.QueryFirstOrDefault<string>("pUserGetRole" , p, commandType: System.Data.CommandType.StoredProcedure);
            }
            return accessRole;
        }
        [HttpGet("/Register")]
        public async Task<bool> Register(string login, string password, string admpass)
        {
            try
            {
                using (SqlConnection db = new SqlConnection(conStr))
                {
                    await db.OpenAsync();

                    // Проверяем, является ли пользователь администратором
                    string role = (admpass == adminCode) ? "admin" : "user";

                    // Выполняем вставку нового пользователя
                    string query = "INSERT INTO Users (username, pwd, accessRole, ticketId) VALUES (@login, @password, @role, 0)";
                    var affectedRows = await db.ExecuteAsync(query, new { login, password, role });

                    return affectedRows > 0;
                }
            }
            catch (Exception ex)
            {
                // Обработка исключений
                Console.WriteLine("Ошибка при регистрации пользователя: " + ex.Message);
                return false;
            }
        }
        [HttpGet("/AddSeance")]
        public async Task<int> AddSeance(int price, int seatings, string seanceTime)
        {
            using (var db = new SqlConnection(conStr))
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Price", price);
                parameters.Add("@Seatings", seatings);
                parameters.Add("@SeanceTime", seanceTime);

                return await db.ExecuteAsync("pAddSeance", parameters, commandType: System.Data.CommandType.StoredProcedure);
            }
        }
        [HttpPost("/AddMovie")]
        public async Task<IActionResult> AddMovie(string actors, string genres, int seanceId, string title, string about, string writer, string movieTime, IFormFile image)
        {
            // Преобразование изображения в массив байтов
            byte[] imageData = null;
            if (image != null && image.Length > 0)
            {
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    await image.CopyToAsync(memoryStream);
                    imageData = memoryStream.ToArray();
                }
            }

            // Вызов хранимой процедуры для добавления фильма
            using (SqlConnection db = new SqlConnection(conStr))
            {
                DynamicParameters p = new DynamicParameters();
                p.Add("Actors", actors);
                p.Add("Genres", genres);
                p.Add("SeanceId", seanceId);
                p.Add("Title", title);
                p.Add("About", about);
                p.Add("Writer", writer);
                p.Add("MovieTime", movieTime);
                p.Add("ImageData", imageData);
                p.Add("ImageName", image != null ? image.FileName : null);

                // Вызываем хранимую процедуру для добавления фильма
                await db.ExecuteAsync("pAddMovie", p, commandType: System.Data.CommandType.StoredProcedure);
            }

            return Ok("1"); // Возвращаем успешный результат
        }
        [HttpGet("GetAllMovies")]
        public async Task<IActionResult> GetAllMovies()
        {
            using (var db = new SqlConnection(conStr))
            {
                var movies = await db.QueryAsync<movie>("GetAllMovies", commandType: System.Data.CommandType.StoredProcedure);
                return Ok(movies);
            }
        }
        [HttpGet("GetSeanceByMovie")]
        public async Task<IActionResult> GetSeanceByMovie(int SeanceId)
        {
            try
            {
                using (var db = new SqlConnection(conStr))
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("SeanceId", SeanceId);

                    var seances =  db.Query<seance>("GetSeanceByMovie", parameters, commandType: CommandType.StoredProcedure);

                    return Ok(seances);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Произошла ошибка: {ex.Message}");
            }
        }
        [HttpGet("UpdateUserTicketId")]
        public async Task<IActionResult> UpdateUserTicketId(int id)
        {
            try
            {
                using (var db = new SqlConnection(conStr))
                {
                    // Создаем строку запроса UPDATE
                    string query = $"UPDATE USERS SET TICKETID = @TicketId WHERE UserId = @UserId";

                    // Создаем анонимный объект с параметрами запроса
                    var parameters = new { TicketId = id, UserId = 1 }; // Замените 1 на реальный Id пользователя

                    // Выполняем запрос к базе данных
                    var rowsAffected = await db.ExecuteAsync(query, parameters);

                    // Проверяем количество затронутых строк
                    if (rowsAffected > 0)
                    {
                        return Ok(); // Возвращаем успешный результат
                    }
                    else
                    {
                        return NotFound(); // Если ни одна строка не была изменена
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Произошла ошибка: {ex.Message}"); // Возвращаем код ошибки 500 с сообщением об ошибке
            }
        }

    }
}

//        create table Users(
//        id int identity,
//        username nvarchar(1000),
// pwd nvarchar(1000),
// accessRole nvarchar(1000),
// ticketId int
// )
// drop table users;
//create table movie(
//id int identity,
//actors nvarchar(max),
//genres nvarchar(1000),
//seanceId int,
//title nvarchar(1000),
//about nvarchar(1000),
//writer nvarchar(1000),
//movieTime nvarchar(1000)
//)
 

//create table seance(
//id int identity,
//price int,
//seatings int,
//seanceTime nvarchar(1000)
//)

//create table places(
//id int identity,
//seanceId int,
//isTaken bit
//)

//create table ticket(
//id int identity,
//seanceId int,
//movieId int,
//movieName nvarchar(1000),
//price int 
//)

//create proc pUser
//@login nvarchar(1000),
//@pwd nvarchar(1000)
//as
//select* from Users where username like @login and pwd like @pwd



//create proc pUser;2
//@id int  
//as
//select accessRole from Users where id = @id



// insert into Users Values('john','john','user','1')

// insert into Users Values('mary','mary','admin','2')

// insert into Users Values('dave','dave','user','3')

// select* from Users

// exec pUser  'man', 'man'

// select* from Users where username like 'john' and pwd like 'john'

// exec pUser; 3 'steve','steve','admin','4'

// create proc pUser;3
// @login nvarchar(1000),
// @pwd nvarchar(1000),
// @role nvarchar(1000)
// as
// insert into Users values(@login, @pwd, @role,0)

// pUser;3 'e','e','user'

// select* from users where username like 'et'

// create proc pUserReg
// @login nvarchar(1000),
// @pwd nvarchar(1000),
// @role nvarchar(1000)
// as
// insert into Users values(@login, @pwd, @role,0)
