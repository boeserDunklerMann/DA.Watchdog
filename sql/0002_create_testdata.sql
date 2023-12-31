use Watchdog;

declare @oid uniqueidentifier = newid();

delete from [Check];
delete from ObservableMeta;
delete from Observable;

insert into Observable
values (@oid, N'Test���', N'Hinweistext');

insert into [Check] (CheckID, ObservableID, Success)
values
	(NEWID(), @oid, 0),
	(NEWID(), @oid, 1),
	(NEWID(), @oid, 0),
	(NEWID(), @oid, 0)
;

insert into ObservableMeta
values (@oid, GETDATE(), 'localhost', 1);