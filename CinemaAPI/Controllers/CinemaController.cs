﻿using Microsoft.AspNetCore.Mvc;
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
