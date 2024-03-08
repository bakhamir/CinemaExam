 
 
 create table Users(
 id int identity,
 username nvarchar(1000),
 pwd nvarchar(1000),
 accessRole nvarchar(1000),
 ticketId int
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



create proc pUser;2
@id int  
as
select accessRole from Users where id = @id



 insert into Users Values('john','john','user','1')

 insert into Users Values('mary','mary','admin','2')

 insert into Users Values('dave','dave','user','3')

 select* from Users

 exec pUser; 2 1

 select* from Users where username like 'john' and pwd like 'john'

 exec pUser; 3 'steve','steve','admin','4'

 create proc pUser; 3
 @login nvarchar(1000),
 @pwd nvarchar(1000),
 @role nvarchar(1000)
 as
 insert into Users values(@login, @pwd, @role,0)

 pUser;3 'joe','joe','admin'

