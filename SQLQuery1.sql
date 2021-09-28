Create database BD_Factura1
go
use BD_Factura1
go 
create table tbl_Factura(
idFactura int identity primary key,
ciudad varchar(20),
fecha smalldatetime,
cliente varchar(40),
direccion varchar(100),
numeroCedula varchar(10),
telefono varchar(10),
subtotal money,
total money
);
go
create table tbl_Detalle(
idDetalle int identity primary key,
cantidad int, 
descripcion varchar(200), 
valorU money,
valorT money, 
idFactura int foreign key references tbl_Factura(idFactura)
);
go

 insert into tbl_Factura
 values ('Quito',
 '2020-09-03 12:43:10',
 'Eduardo Paolo Revelo Vizcaino',
 'Ponceano',
 '1722585446',
 '2807929',
 31.4,
 100.2)
 go
 insert into tbl_Factura
 values ('Cuenca',
 '2020-10-03 12:43:10',
 'Eduardo Paolo Revelo Vizcaino',
 'Ponceano',
 '1722585446',
 '2807929',
 31.4,
 100.2)
 go
 insert into tbl_Factura
 values ('Cuenca',
 '2020-10-03 12:43:10',
 'EPN',
 'Ponceano',
 '1722585446',
 '2807929',
 31.4,
 100.2)
 go

  insert into tbl_Detalle
 values (1,
 'Papas',
 31.54,
 100.2,
 1)
 go
  go 
 select * from tbl_Factura
 go
  select * from tbl_Detalle
 go
 delete from tbl_Factura
 where idFactura=1

delete from tbl_Detalle
 where idFactura=2