create database hala_store;
go
use hala_store;
go
create table category(
	id_c int primary key,
	nom_c varchar(25) not null,
);
create table produit(
	idP int primary key,
	NomP varchar(25) not null,
	stock int not null,
	price decimal(8, 2),
	brand varchar(25) not null,
	id_c int foreign key references category(id_c),
);
create table client(
	idC int primary key,
	NomC varchar(25) not null,
	cin varchar(10) not null,
	pasword varchar(10) not null,
);
create table factur(
	id_F int primary key identity,
	Nomclient varchar(25) not null,
	NomProduit varchar(25) not null,
	Qnt int not null,
	price decimal(8,2),
	total decimal(9,2),
);
create table command(
	idCmd int primary key,
	Nomclient varchar(25) not null,
	NomProduit varchar(25) not null,
	Qnt int not null,
	price decimal(8,2),
	total decimal(9, 2),
	Date_Cmd date,
	id_F int foreign key references factur(id_F),
);
select * from factur
select * from command

drop TABLE command


  
