Create table ApiUserAccess
(
   UserName varchar(20) primary key,
   [Password] varchar(30),
   TaxCode varchar(20),
   CreateDate DateTime default getdate(),
   IpMachine varchar(150),
   Status int
)
go
--them key cho table productinv
  alter table ProductInv add primary key (id)