use Watchdog;

/* contains objects (servers) to watch for*/
if not exists(select * from sys.tables where name='Observable' and type='U')
	create table Observable
	(
		ObservableID uniqueidentifier not null,
		[name] nvarchar(100),
		Remarks nvarchar(max),
		constraint PK_Observable primary key (ObservableID)
	);

/* contains timestamps for the checks of the observables */
if not exists(select * from sys.tables where name='Check' and type='U')
	create table [Check]
	(
		CheckID uniqueidentifier not null,
		ObservableID uniqueidentifier not null,
		[TimeStamp] datetime not null default getdate(),
		Success bit not null,
		constraint PK_Check primary key (CheckID),
		constraint FK_Check_Observable foreign key (ObservableID)
			references Observable(ObservableID)
	);