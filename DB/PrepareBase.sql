USE testing

IF OBJECT_ID('NetworkUser') IS NOT NULL DROP TABLE NetworkUser
CREATE TABLE NetworkUser
(
	Id int PRIMARY KEY identity(1,1),
	Name varchar(max) not null,
	Login  varchar(20) not null
)

-- Напишите скрипт, выставляющий на существующую таблицу NetworkUser уникальность по полю логин Login.
alter table 
  NetworkUser 
add 
  unique(Login)

-- Напишите скрипт, который добавляет в вашу таблицу NetworkUser двух пользователей: логин admin с именем admin и test с именем Тест Тестович. И выполните этот скрипт.
INSERT INTO NetworkUser
(
Name,
Login
)
values
('admin', 'admin'),
('Тест Тестович', 'test')