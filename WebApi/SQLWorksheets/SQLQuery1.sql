﻿select *
from BufferItems

select *
from Users

insert into Users (Username, Password)
values ('admin', 'admin');

delete from BufferItems
where id > 33

delete from Users
where id > 1

update BufferItems
set Value = 'OldBuffer2'
where id = 33