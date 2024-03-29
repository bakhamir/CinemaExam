create proc pUserGetRole
@login nvarchar(1000),
@pwd nvarchar(1000)
as
select accessRole from Users where username like '%' + @login  +'%' and pwd like '%' + @pwd  +'%'

pUserGetRole amir,amir

CREATE PROCEDURE pAddSeance
    @Price INT,
    @Seatings INT,
    @SeanceTime NVARCHAR(100)
AS
BEGIN
    INSERT INTO seance (price, seatings, seanceTime)
    VALUES (@Price, @Seatings, @SeanceTime);
END

select * from movie


CREATE PROCEDURE pAddMovie
    @Actors NVARCHAR(MAX),
    @Genres NVARCHAR(1000),
    @SeanceId INT,
    @Title NVARCHAR(1000),
    @About NVARCHAR(1000),
    @Writer NVARCHAR(1000),
    @MovieTime NVARCHAR(1000)
AS
BEGIN
    INSERT INTO Movie (actors, genres, seanceId, title, about, writer, movieTime)
    VALUES (@Actors, @Genres, @SeanceId, @Title, @About, @Writer, @MovieTime);
END

create table movie(
id int identity,
actors nvarchar(max),
genres nvarchar(1000),
seanceId int,
title nvarchar(1000),
about nvarchar(1000),
writer nvarchar(1000),
movieTime nvarchar(1000),
ImageData VARBINARY(MAX),
ImageName NVARCHAR(1000)
)

drop table movie
 alter PROCEDURE pAddMovie
    @Actors NVARCHAR(MAX),
    @Genres NVARCHAR(1000),
    @SeanceId INT,
    @Title NVARCHAR(1000),
    @About NVARCHAR(1000),
    @Writer NVARCHAR(1000),
    @MovieTime NVARCHAR(1000),
    @ImageData VARBINARY(MAX),
    @ImageName NVARCHAR(1000)
AS
BEGIN
    INSERT INTO Movie (actors, genres, seanceId, title, about, writer, movieTime, imageData, imageName)
    VALUES (@Actors, @Genres, @SeanceId, @Title, @About, @Writer, @MovieTime, @ImageData, @ImageName);
END

select * from movie

CREATE PROCEDURE GetAllMovies
AS
BEGIN
    SELECT ImageName FROM movie;
END

 
CREATE PROCEDURE GetSeanceByMovie
@SeanceId int
AS
BEGIN
    SELECT * FROM seance where id = @SeanceId;
END
    GetSeanceByMovie 1
create table seance(
id int identity,
price int,
seatings int,
seanceTime nvarchar(1000)
)
drop table seance

select * from seance