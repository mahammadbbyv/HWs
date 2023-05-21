use [University 2];

create table Assistants(
	Id int identity(1,1) not null primary key,
	TeacherId int not null foreign key references Teachers(Id)
);

create table Curators(
	Id int identity(1,1) not null primary key,
	TeacherId int not null foreign key references Teachers(Id)
);

create table Deans(
	Id int identity(1,1) not null primary key,
	TeacherId int not null foreign key references Teachers(Id)
);

create table Departments(
	Id int identity(1,1) not null primary key,
	Building int not null check (1 <= Building and Building <= 5),
	[Name] nvarchar(100) not null check ([Name] != '') unique,
	FacultyId int not null foreign key references Faculties(Id),
	HeadId int not null foreign key references Heads(Id)
);

create table Faculties(
	Id int identity(1,1) not null primary key,
	Building int not null check (1 <= Building and Building <= 5),
	[Name] nvarchar(100) not null check ([Name] != '') unique,
	DeanId int not null foreign key references Deans(Id)
);

create table Groups(
	Id int identity(1,1) not null primary key,
	[Name] nvarchar(100) not null check ([Name] != '') unique,
	[Year] int not null check ([Year] between 1 and 5),
	DepartmentId int not null foreign key references Departments(Id)
);

create table GroupsCurators(
	Id int not null identity(1,1) primary key,
	CuratorId int not null foreign key references Curators(Id),
	GroupId int not null foreign key references Groups(Id)
);

create table GroupsLectures(
	Id int not null identity(1,1) primary key,
	GroupId int not null foreign key references Groups(Id),
	LectureId int not null foreign key references Lecture(Id)
);

create table Heads(
	Id int not null identity(1,1) primary key,
	TeacherId int not null foreign key references Teachers(Id)
);

create table LectureRooms(
	Id int not null identity(1,1) primary key,
	Building int not null check (1 <= Building and Building <= 5),
	[Name] nvarchar(100) not null check ([Name] != '') unique
);

create table Lectures(
	Id int not null identity(1,1) primary key,
	SubjectId int not null foreign key references [Subjects](Id),
	TeacherId int not null foreign key references Teachers(Id)
);

create table Schedules(
	Id int not null identity(1,1) primary key,
	Class int not null check (Class between 1 and 8),
	[DayOfWeek] int not null check ([DayOfWeek] between 1 and 7),
	[Week] int not null check ([Week] between 1 and 8),
	LectureId int not null foreign key references Lecture(Id),
	LectureRoomId int not null foreign key references LectureRooms(Id)
);

create table Subjects(
	Id int not null primary key identity(1,1),
	[Name] nvarchar(100) not null check ([Name] != '') unique
);

create table Teachers(
	Id int not null identity(1,1) primary key,
	[Name] nvarchar(100) not null check ([Name] != '') unique,
	Surname nvarchar(100) not null check (Surname != '') unique
);

--select LectureRooms.[Name] from LectureRooms
--inner join Schedules on Schedules.LectureRoomId = LectureRooms.Id
--inner join Lecture on Schedules.LectureId = Lecture.Id
--inner join Teachers on Lecture.TeacherId = Teachers.Id
--group by LectureRooms.[Name], Teachers.[Name]
--having Teachers.[Name] = 'Edward Hopper';

--select Teachers.[Name] from Teachers
--inner join Assistants on Assistants.TeacherId = Teachers.Id
--inner join Lecture on Lecture.TeacherId = Teachers.Id
--inner join GroupsLectures on GroupsLectures.LectureId = Lecture.Id
--inner join Groups on Groups.Id = GroupsLectures.GroupId
--where Assistants.TeacherId = Teachers.Id and Groups.[Name] = 'F505';

--select Subjects.[Name] from GroupsLectures 
--inner join Groups on GroupsLectures.GroupId = Groups.Id 
--inner join Lectures on GroupsLectures.LectureId = Lectures.Id 
--inner join Subjects on Lectures.SubjectId = Subjects.Id 
--inner join Teachers on Lectures.TeacherId = Teachers.Id 
--where Teachers.[Name] = N'Alex Carmack' AND Groups.Year = 5;

--select Teachers.Surname from Schedules 
--inner join Lectures on Schedules.LectureId = Lectures.Id 
--inner join Teachers on Lectures.TeacherId = Teachers.Id 
--where Schedules.DayOfWeek <> N'Monday';

--select LectureRooms.[Name] as [LectureName], LectureRooms.Building as [Building] from Schedules
--inner join LectureRooms on Schedules.LectureRoomId = LectureRooms.Id
--where (Schedules.DayOfWeek <> N'Wednesday' and Schedules.Week <> 2);

--select (Teachers.[Name] + ' ' + Teachers.Surname) as [FullName] from GroupsCurators 
--inner join Groups on GroupsCurators.GroupId = Groups.Id 
--inner join Departments on Groups.DepartmentId = Departments.id 
--inner join Curators on GroupsCurators.CuratorId = Curators.Id 
--inner join Teachers on Curators.TeacherId = Teachers.Id
--inner join Faculties on Departments.FacultyId = Faculties.Id 
--inner join Deans on Faculties.DeanId = Deans.Id 
--where Faculties.[Name] = N'Computer Science' AND Departments.[Name] <> N'Software Development' and Deans.TeacherId = Teachers.Id;

--select distinct Building from Faculties
--union select distinct Building from Departments
--union select distinct Building from LectureRooms;

--select Teachers.[Name] as [Name] from Deans
--inner join Teachers on Deans.TeacherId = Teachers.Id
--union all select Teachers.[Name] from Heads
--inner join Teachers on Heads.TeacherId = Teachers.Id
--union all select distinct Teachers.[Name] from Teachers
--union all select Teachers.[Name] from Curators
--inner join Teachers on Curators.TeacherId = Teachers.Id
--union all select Teachers.[Name] from Assistants
--inner join Teachers on Assistants.TeacherId = Teachers.Id;

--select distinct DayOfWeek from Schedules
--inner join LectureRooms on Schedules.LectureRoomId = LectureRooms.Id
--where (LectureRooms.[Name] = N'A311' or LectureRooms.[Name] = N'A104') and Building = 6;

