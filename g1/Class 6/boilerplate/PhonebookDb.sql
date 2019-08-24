use PhonebookDb

create table Users
(
	[Id] int identity(1, 1) primary key not null,
	[Username] nvarchar(20) not null,
	[Password] nvarchar(20) not null,
	[FirstName] nvarchar(30) not null,
	[LastName] nvarchar(30) not null,
);

create table Contacts
(
	[Id] int identity(1, 1) primary key not null,
	[FirstName] nvarchar(30) not null,
	[Lastname] nvarchar(30) not null,
	[Email] nvarchar(40) null,
	[Address] nvarchar(30) null,
	[Phonenumber] nvarchar(30) null,
	[UserId] int foreign key references Users(Id) not null,
);

go

insert into Users([FirstName], [Lastname], [Username], [Password])
values
	('Igor', 'Mitkovski', 'igor.mitkovski', '123456'),
	('Dejan', 'Blazheski', 'dejan.blazheski', '123456'),
	('Stojko', 'Smilevski', 'stojko.smilevski', '123456')
;

insert into Contacts([FirstName], [Lastname], [Email], [Address], [Phonenumber], [UserId])
values
	('Bojan', 'Gjorgjievski', 'bojan.gjorgjievski@gmail.com', 'SomeAddress1', '123456', 1),
	('Nikola', 'Petreski', 'dejan.blazheski@hotmail.com', 'SomeAddress2', '123456', 1),
	('Riste', 'Mechkaroski', 'riste.mechkaroski@live.com', 'SomeAddress3', '123456', 2),
	('Stojanche', 'Majstorot', 'stojko.smilevski@live.com', 'SomeAddress4', '123456', 3),
	('Jordan', 'Kazmata', 'stojko.smilevski@hotmail.com', 'SomeAddress5', '123456', 3),
	('Miroslav', 'Rabota', 'stojko.smilevski@gmail.com', 'Some Address6', '123456', 3)
;
