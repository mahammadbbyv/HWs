use Hospital

create table Diseases(
	Id int primary key not null identity(1,1),
	[Name] nvarchar(100) not null check ([Name] != '') unique,
	Severity int not null check (Severity >= 1) default 1
);

create table Doctors(
	Id int primary key not null identity(1,1),
	[Name] nvarchar(MAX) not null check ([Name] != ''),
	Phone char(10) not null,
	Premium money not null check(Premium >= 0) default 0,
	Salary money not null check(Salary > 0),
	Surname nvarchar(MAX) not null check (Surname != '')
);

create table Examinations(
	Id int primary key not null identity(1,1),
	[DayOfWeek] int not null check([DayOfWeek] >= 1 and [DayOfWeek] <= 7),
	[Name] nvarchar(100) not null check ([Name] != '') unique,
	StartTime time not null check (StartTime >= '8:00:00' and StartTime <= '18:00:00'),
	EndTime time not null check (EndTime >= '8:00:00' and EndTime <= '18:00:00')
);

create table Wards(
	Id int primary key not null identity(1,1),
	Building int not null check (Building >= 1 and Building <= 5),
	[Floor] int not null check ([Floor] >= 1),
	[Name] nvarchar(20) not null check ([Name] != '') unique
);
create table Departments(
	Id int not null primary key identity(1,1),
	Building int not null check (1 <= Building and Building <= 5),
	[Financing] money not null check (Financing >= 0) default 0,
	[Floor] int not null check ([Floor] >= 1),
	[Name] nvarchar(100) not null check ([Name] != '') unique
);


--select * from Wards;
-- #1

--select Surname, Phone from Doctors;
-- #2

--select Distinct Building from Wards;
-- #3

--select [Name] as NameOfDiseas, Severity as SeverityOfDisease from Diseases;
-- #4 and #5

--select [Name] from Departments where (Building = 5 and Financing > 30000);
-- #6

--select [Name] from Departments where (Building = 3 and Financing >= 12000 and Financing <= 15000);
-- #7

--select [Name] from Wards where(Building >= 3 and Building <= 5 and [Floor] = 1);
-- #8

--select [Name], Building, Financing from Departments where (Building = 3 or Building = 6 and Financing >= 11000 and Financing <= 25000);
-- #9


