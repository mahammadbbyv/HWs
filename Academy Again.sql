use University

create table Curators (
	Id int primary key identity(1,1),
	[Name] nvarchar(max) not null,
	Surname nvarchar(max) not null 
);

create table Students (
	Id int primary key identity(1,1),
	[Name] nvarchar(max) not null,
	Rating int not null check (1 <= Rating and Rating <= 5),
	Surname nvarchar(max) not null 
);

create table Subjects (
	Id int primary key identity(1,1),
	[Name] nvarchar(100) not null unique 
);

create table Teachers (
	Id int primary key identity(1,1),
	IsProfessor bit not null default 0,
	[Name] nvarchar(max) not null,
	Salary money not null check (Salary > 0),
	Surname nvarchar(max) not null 
);

create table Lectures (
	Id int primary key identity(1,1),
	[Date] date not null,
	SubjectId int not null foreign key references Subjects(Id),
	TeacherId int not null foreign key references Teachers(Id)
);

create table Faculties (
	Id int primary key identity(1,1),
	[Name] nvarchar(100) not null unique
);

create table Departments (
	Id int primary key identity(1,1),Building int not null check (1 <= Building and Building <= 5),
	Financing money not null default 0,[Name] nvarchar(100) not null unique,
	FacultyId int not null foreign key references Faculties(Id) 
);

create table Groups (
	Id int primary key identity(1,1),
	[Name] nvarchar(10) not null unique,
	[Year] int not null check ([Year] BETWEEN 1 AND 5),
	DepartmentId int not null foreign key references Departments(Id) 
);

create table GroupsCurators (
	Id int primary key identity(1,1),
	CuratorId int not null foreign key references Curators(Id),
	GroupId int not null foreign key references Groups(Id) 
);

create table GroupsLectures (
	Id int primary key identity(1,1),
	GroupId int not null foreign key references Groups(Id),
	LectureId int not null foreign key references Lectures(Id) 
);

create table GroupsStudents (
	Id int primary key identity(1,1),
	GroupId int not null foreign key references Groups(Id),
	StudentId int not null foreign key references Students(Id) 
);

select Departments.Building from Departments 
where (select SUM(Departments.Financing) from Departments Group By Building) > 10000;

select Groups.[Name] from Groups 
inner join Departments on Groups.Id = Departments.Id 
inner join GroupsLectures on Groups.Id = GroupsLectures.GroupId 
where Groups.Year = 5 AND 10 < (select COUNT(GroupsLectures.Id) from GroupsLectures);

select Groups.[Name] from Groups 
inner join GroupsStudents on Groups.Id = GroupsStudents.GroupId 
inner join Students on GroupsStudents.StudentId = Students.Id
where ( (select AVG(Students.Rating) from Students) > (select (AVG(Students.Rating)) from Students where Groups.[Name] = 'D221'));

select Teachers.Surname as [Surname],Teachers.[Name] as [Name] from Teachers 
where (Teachers.Salary > (select AVG(Teachers.Salary) from Teachers where Teachers.IsProfessor = 1));

select Groups.[Name] from Groups 
inner join GroupsCurators on Groups.Id = GroupsCurators.GroupId 
inner join Curators on GroupsCurators.CuratorId = Curators.Id 
where ((select COUNT(Curators.Id) from Curators) > 1);

select Groups.[Name] from Groups 
inner join GroupsStudents on Groups.Id = GroupsStudents.GroupId 
inner join Students on GroupsStudents.StudentId = Students.Id 
where ( (select AVG(Students.Rating) from Students) > (select MIN(Students.Rating) from Students where Groups.[Year] = 5));

select Faculties.[Name] from Faculties 
inner join Departments on Faculties.Id = Departments.FacultyId 
where (Departments.Financing > (select Departments.Financing from Departments where Departments.[Name] = 'Computer Science'));

select Subjects.[Name], Teachers.[Name] + ' ' + Teachers.Surname as [Name] from Subjects 
inner join Lectures on Subjects.Id = Lectures.SubjectId 
inner join Teachers on Lectures.TeacherId = Teachers.Id 
group By Subjects.[Name], Teachers.[Name], Teachers.Surname 
having ( COUNT(Lectures.Id) = (select max(Lectures.Id) from Lectures))

select top 1 Subjects.[Name] from Subjects 
inner join Lectures on Subjects.Id = Lectures.SubjectId 
group by Subjects.[Name] 
order by COUNT(Lectures.Id)

select COUNT(Students.Id), COUNT(Subjects.Id) from Departments 
inner join Groups on Departments.Id = Groups.DepartmentId 
inner join GroupsStudents on Groups.Id = GroupsStudents.GroupId 
inner join Students on GroupsStudents.StudentId = Students.Id 
inner join GroupsLectures on Groups.Id = GroupsLectures.GroupId 
inner join Lectures on GroupsLectures.LectureId = Lectures.Id 
inner join Subjects on Lectures.SubjectId = Subjects.Id 
where Departments.[Name] = 'Software Development'