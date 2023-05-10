use Hospital2;
--create table Departments(
--	Id int not null primary key identity(1,1),
--	Building int not null check (1 <= Building and Building <= 5),
--	[Name] nvarchar(100) not null check ([Name] != '') unique
--);

--create table Doctors(
--	Id int primary key not null identity(1,1),
--	[Name] nvarchar(MAX) not null check ([Name] != ''),
--	Surname nvarchar(MAX) not null check (Surname != ''),
--	Premium money not null check(Premium >= 0) default 0,
--	Salary money not null check(Salary > 0)
--);

--create table Examinations(
--	Id int primary key not null identity(1,1),
--	[DayOfWeek] int not null check([DayOfWeek] >= 1 and [DayOfWeek] <= 7),
--	[Name] nvarchar(100) not null check ([Name] != '') unique,
--	StartTime time not null check (StartTime >= '8:00:00' and StartTime <= '18:00:00'),
--	EndTime time not null check (EndTime >= '8:00:00' and EndTime <= '18:00:00')
--);

--create table Wards(
--	Id int primary key not null identity(1,1),
--	Places int not null check (Places >= 1),
--	[Floor] int not null check ([Floor] >= 1),
--	[Name] nvarchar(20) not null check ([Name] != '') unique,
--	DepartmentId int not null foreign key references Departments([Id])
--);
--create table DoctorsExaminations(
--	Id int primary key not null identity(1,1),
--	[Name] nvarchar(100) not null check ([Name] != '') unique,
--	StartTime time not null check (StartTime >= '8:00:00' and StartTime <= '18:00:00'),
--	EndTime time not null check (EndTime >= '8:00:00' and EndTime <= '18:00:00'),
--	DoctorId int not null foreign key references Doctors([Id]),
--	ExaminationId int not null foreign key references Examinations([Id]),
--	WardId int not null foreign key references Wards([Id])
--);

--select * from Wards
--Where Places > 10;

--select Departments.Building, Count(*) from Wards
--inner join Departments on Departments.Id = Wards.DepartmentId

--select Departments.[Name], Count(*) from Wards
--inner join Departments on Departments.Id = Wards.DepartmentId

--select Departments.[Name], SUM(Doctors.Salary) as Total from DoctorsExaminations
--inner join Doctors on Doctors.Id = DoctorsExaminations.DoctorId
--inner join Wards on Wards.Id = DoctorsExaminations.WardId
--inner join Departments on Wards.DepartmentId = Departments.Id
--group by Departments.[Name]

--select Departments.[Name] from DoctorsExaminations
--inner join Doctors on Doctors.Id = DoctorsExaminations.DoctorId
--inner join Wards on Wards.Id = DoctorsExaminations.WardId
--inner join Departments on Wards.DepartmentId = Departments.Id
--group by Departments.[Name]
--HAVING COUNT(Doctors.Id) >= 5;

--select AVG(Doctors.Salary + Doctors.Premium) as Average from Doctors

--select Min(Wards.Places) as [Name] from Wards
--group by Wards.[Name]

--select Departments.Building from Wards
--inner join Departments on Wards.DepartmentId = Departments.Id
--group by Departments.[Name]
--HAVING SUM(Wards.Places) >= 100;

create function GetAllDoctors()
returns table
as
	return
		(select * from Doctors)
select * from GetAllDoctors();

create function GetAllDoctorsExcept(@except CHAR(20))
returns table
as
	begin
		if @except = 'Name'
		begin
			THEN return (select Id, Surname, Premium, Salary from Doctors);
		end
		else
		begin
			if @except = 'Surname'
			begin
				THEN return (select Id, [Name], Premium, Salary from Doctors)
			end
		
			else 
			begin
				if @except = 'Premium'
				begin
					THEN return (select Id, [Name], Surname, Salary from Doctors)
				end
				else 
				begin
					if @except = 'Salary'
					begin
					THEN return (select Id, [Name], Surname, Premium from Doctors)
					end
				end
			end
		end
	end

