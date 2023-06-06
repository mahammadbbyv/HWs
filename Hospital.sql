use Hospital

create TABLE Departments (
	[Id] int check (Id not like '') primary key identity(1, 1),
	[Building] int not null check(Building >= 1 AND Building <= 5),
	[Name] nvarchar(100) not null check(Name not like '') unique
);

create table Doctors (
	[Id] int check (Id not like '') primary key identity(1, 1),
	[Name] nvarchar(max) not null check([Name] not like ''),
	[Premium] money not null check([Premium] >= 0) DEFAULT '0',
	[Salary] money not null check([Salary] > 0),
	[Surname] nvarchar(max) not null check([Surname] not like '')
);

create table Wards (
	[Id] int check (Id not like '') primary key identity(1, 1),
	[Name] nvarchar(20) not null check([Name] not like '') unique,
	[Places] int not null check(Places >= 1),
	[DepartmentId] int not null foreign key references Departments
);

create table Examinations (
	[Id] int check (Id not like '') primary key identity(1, 1),
	[Name] nvarchar(100) not null check(Name not like '') unique
);

create table DoctorsExaminations (
	[Id] int check (Id not like '') primary key identity(1, 1),
	[EndTime] time not null,add shit[Name] nvarchar(100) not null check([Name] not like '') unique,
	[StartTime] time not null check([StartTime] between '8:00' and '18:00'),
	[DoctorId] int check (DoctorId not like '') foreign key references Doctors,
	[ExaminationId] int check (ExaminationId not like '') not null foreign key references Examinations,
	[WardId] int check (WardId not like '') not null foreign key references Wards
);

create function getDoctorsPremiumTotal() returns money as
	begin declare @TotalPremium money;
	select @TotalPremium = SUM(Premium) from Doctors;
	return @TotalPremium;
end;

create function GetDptCountByBuilding(@buildingNum int) returns int as
begin
	declare @dptCount int;
	select @dptCount = COUNT(*) from Departments
	where Departments.Building = @buildingNum;
	return @dptCount;
end;

create function GetExaminationNameFromID(@ExaminationID int) returns int as
begin
	declare @ExaminationName nvarchar;
	select @ExaminationName = Examinations.Name from Examinations 
	where Examinations.Id = @ExaminationID
	return @ExaminationName; 
end;

create function GetWardCapacity(@WardID int) returns int as
begin
	declare @WardCapacity int;
	select @WardCapacity = Wards.Places from Wards
	where Wards.Id = @WardID
	return @WardCapacity;
end;

create Function GetDoctorFullName(@doctorId int) returns nvarchar(max) as
begin
	declare @FullName nvarchar(max);
	select @FullName = concat(Name, ' ', Surname) from Doctors 
	where Id = @doctorId; 
	return @FullName;
end;

create procedure IncreaseDoctorsPremium(@PremiumIncrease money) as
begin
	update Doctors set Doctors.Premium = Doctors.Premium + @PremiumIncrease;
end;

create procedure UpdateDoctorsSalary (@NewSalary money) as 
begin
	update Doctors set Salary = @NewSalary;
end;

create Procedure AddDepartment (@Building int, @Name nvarchar(100)) as 
begin 
	insert into Departments (Building, Name)
	Values (@Building, @Name);
end;

create AssignDoctorToExamination (@DoctorId int, @ExaminationId int, @WardId int) as 
begin 
	insert into DoctorsExaminations (DoctorId, ExaminationId, WardId) 
	Values (@DoctorId, @ExaminationId, @WardId); 
end;

create Procedure RemoveDoctorFromExamination (@DoctorExaminationId int) as 
begin 
	delete From DoctorsExaminations 
	where Id = @DoctorExaminationId; 
end;