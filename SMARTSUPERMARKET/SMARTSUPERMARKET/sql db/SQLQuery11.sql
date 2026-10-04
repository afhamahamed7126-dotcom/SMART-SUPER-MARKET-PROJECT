
create table tblAdmin 
(
AdminID nvarchar(50) primary key ,
[password] nvarchar (50) ,
FullName nvarchar (50)
)

Select * from tblAdmin

Select top 1 AdminID,password,FullName from tblAdmin Where AdminID=@AdminID and password= @Password 

Create table tblseller 
(
SellerID int identity (1,1) primary Key,
SellerName nvarchar(50) Unique,
SellerAge int ,
Sellerphone nvarchar(10),
Sellerpass nvarchar(50)
)

Select * from tblseller

Select top 1 SellerName,Sellerpass from tblseller Where SellerName=@SellerName and Sellerpass= @Sellerpass 

create table tblCategory
(
CatID int identity(1,1) primary key not null,
CategoryName nvarchar(50),
CategoryDesc nvarchar(50)
)
Select * from tblCategory
