create database dboConcesionarios;


create table Direcciones
(
direccionID int identity primary key,
poblacion varchar(50),
ciudad varchar(50)
);

go;

create table Concesionarios
(
concesionarioID int identity primary key,
nombre varchar(50) not null,
direccionID int,
stockID int,
foreign key(direccionID) references Direcciones(direccionID)
);

alter table Concesionarios
truncate foreign key(stockID) references Stock(stockID);

drop table Concesionarios;
drop table Direcciones;

create table Vehiculos
(
vehiculoID int identity primary key,
marca varchar(50),
modelo varchar(50),
km int,
vendido varchar(1),
constraint chk_vendido check (vendido in ('s','n'))
);

go;

create table Stock
(
stockID int identity primary key,
concesionarioID int,
vehiculoID int,
foreign key(vehiculoID) references Vehiculos(vehiculoID),
foreign key(concesionarioID) references Concesionarios(concesionarioID)
);

go;

insert into Direcciones values ('Arteixo','A Coruña');
insert into Direcciones values ('Carballo','A Coruña');
insert into Direcciones values ('Vimianzo','A Coruña');

insert into Vehiculos values ('Fiat','Idea',12000,'n');
insert into Vehiculos values ('Seat','Ibiza',220000,'n');
insert into Vehiculos values ('Renoult','clio',100000,'s');

insert into Stock values (2,1);
insert into Stock values (2,2);
insert into Stock values (2,3);

insert into dbo.Concesionarios values ('ArtConces', 1);
insert into dbo.Concesionarios values ('CarballoCoches', 2);
insert into dbo.Concesionarios values ('VimiMotor', 3);

select v.vehiculoID, v.marca, v.modelo, v.km, v.vendido from stock s inner join Vehiculos v on v.vehiculoID=s.vehiculoID;




