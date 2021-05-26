USE testing

IF OBJECT_ID('NetworkUser') IS NOT NULL DROP TABLE NetworkUser
CREATE TABLE NetworkUser
(
	Id int PRIMARY KEY identity(1,1),
	Name varchar(max) not null,
	Login  varchar(20) not null
)

-- �������� ������, ������������ �� ������������ ������� NetworkUser ������������ �� ���� ����� Login.
alter table 
  NetworkUser 
add 
  unique(Login)

-- �������� ������, ������� ��������� � ���� ������� NetworkUser ���� �������������: ����� admin � ������ admin � test � ������ ���� ��������. � ��������� ���� ������.
INSERT INTO NetworkUser
(
Name,
Login
)
values
('admin', 'admin'),
('���� ��������', 'test')
go

-- �������� ��������� SQL AddingUserProc � ����������� Name � Login, ������� ��������� ������ � ������� NetworkUser.
create procedure AddingUserProc (
  @Name varchar(max)
, @Login varchar(20)
)
AS
BEGIN

  insert into NetworkUser (
	Name
  , Login
  )
  Values
    (@Name, @Login)
END