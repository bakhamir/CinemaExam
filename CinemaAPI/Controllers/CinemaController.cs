using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using CinemaWPF.Models;
using Dapper;

namespace CinemaAPI.Controllers
{
    public class CinemaController : Controller
    {
        private readonly string conStr = "Data Source=207-3; Initial Catalog=CinemaDb;Integrated Security=True;TrustServerCertificate=Yes";
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost("/Login")]
        public async Task<bool> Login(string login, string password)
        {
            using (SqlConnection db = new SqlConnection(conStr))
            {
                DynamicParameters p = new DynamicParameters();
                p.Add("login", login); p.Add("pwd", password);
                var res = db.Query("pUser", p, commandType: System.Data.CommandType.StoredProcedure);
                if (res != null)
                    return true;
                else
                    return false;
            }
        }
        [HttpPost("/GetRole")]
        public async Task<string> GetRole(string id)
        {
            using (SqlConnection db = new SqlConnection(conStr))
            {
                return db.ExecuteScalar("pUser;2", id, commandType: System.Data.CommandType.StoredProcedure).ToString();
            }
        }
        [HttpPost("/Register")]
        public async Task<bool> Register(string login, string password)
        {
            using (SqlConnection db = new SqlConnection(conStr))
            {
                DynamicParameters p = new DynamicParameters();
                p.Add("login", login); p.Add("pwd", password);
                var res = db.Query("pUser;3", p, commandType: System.Data.CommandType.StoredProcedure);
                if (res != null)
                    return true;
                else
                    return false;
            }
        }
        /*
         create table Users(
id int identity,
username nvarchar(1000),
pwd nvarchar(1000),
accessRole nvarchar(1000)
)

create table movie(
id int identity,
actors nvarchar(max),
genres nvarchar(1000),
seanceId int,
title nvarchar(1000),
about nvarchar(1000),
writer nvarchar(1000),
movieTime nvarchar(1000)
)
drop table seance

create table seance(
id int identity,
price int,
seatings int,
seanceTime nvarchar(1000)
)

create table places(
id int identity,
seanceId int,
isTaken bit
)

create table ticket(	
id int identity,
seanceId int,
movieId int,
movieName nvarchar(1000),
price int 
)

create proc pUser
@login nvarchar(1000),
@pwd nvarchar(1000)
as
select * from Users where username like @login and pwd like @pwd

insert into Users values('john','john','user')

create proc pUser;2
@id int  
as
select accessRole from Users where id = @id

create proc pUser;3 
@login nvarchar(1000),
@pwd nvarchar(1000),
@role nvarchar(1000)
as
insert into Users
         */

    }
}
