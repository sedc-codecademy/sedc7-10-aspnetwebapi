

create table Tasks
(
	id int primary key not null,
	title nvarchar(50) not null,
	description nvarchar(250),
	isComplete bit not null default 0
);

insert into Tasks (id, title, description, isComplete)
values
	(1, 'Task 1', 'Task 1 description', 0),
	(2, 'Task 2', 'Task 2 description', 0),
	(3, 'Task 3', 'Task 3 description', 0),
	(4, 'Task 4', 'Task 4 description', 0),
	(5, 'Task 5', 'Task 5 description', 0),
	(6, 'Task 6', 'Task 6 description', 0)
;