use [Hospital3];

create table Departments(
	Id int primary key not null identity(1,1),
	Building int,
	Financing int,
	[Name] nvarchar(MAX)
);

create table Doctors(
	Id int primary key not null identity(1,1),
	[Name] nvarchar(MAX),
	Surname nvarchar(MAX),
	Salary money
);

create table Examinations(
	Id int primary key not null identity(1,1),
	[Name] nvarchar(MAX)
);

create table Professors(
	DoctorsId int not null foreign key references Doctors([Id]),
	Name nvarchar(MAX)
);

create table Interns(
	DoctorsId int not null foreign key references Doctors([Id]),
	Name nvarchar(MAX)
);

create table Wards(
	Id int primary key not null identity(1,1),
	Places int,
	[Name] nvarchar(20),
	DepartmentId int not null foreign key references Departments([Id])
);

create table Diseases(
	Id int primary key not null identity(1,1),
	[Name] nvarchar(100)
);

create table DoctorsExaminations(
	Id int primary key not null identity(1,1),
	[Name] nvarchar(100) not null check ([Name] != '') unique,
	DiseasesId int not null foreign key references Diseases([Id]),
	DoctorId int not null foreign key references Doctors([Id]),
	ExaminationId int not null foreign key references Examinations([Id]),
	WardId int not null foreign key references Wards([Id])
);

--select Wards.[Name], Places from Wards
--inner join Departments on Wards.DepartmentId = Departments.Id
--where Departments.Building = 5 and Places >= 5;

--select Departments.[Name], Places from Departments
--inner join Wards on Wards.DepartmentId = Departments.Id
--inner join DoctorsExaminations on DoctorsExaminations.WardId = Wards.Id
--where COUNT(DoctorsExaminations.ExaminationId) > 0;

--select Diseases.[Name] from Diseases
--inner join DoctorsExaminations on DoctorsExaminations.DiseasesId = Diseases.Id
--where COUNT(DoctorsExaminations.ExaminationId) = 0;

--select Doctors.[Name], Doctors.Surname from Doctors
--inner join DoctorsExaminations on DoctorsExaminations.DoctorId = Doctors.Id
--where COUNT(DoctorsExaminations.ExaminationId) = 0;

--select Departments.[Name], Places from Departments
--inner join Wards on Wards.DepartmentId = Departments.Id
--inner join DoctorsExaminations on DoctorsExaminations.WardId = Wards.Id
--where COUNT(DoctorsExaminations.ExaminationId) = 0;