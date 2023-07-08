USE $(TeamHolidayDBName);
GO;

INSERT INTO [dbo].[MyTeamUserRole] (Guid, RoleName, RoleStatus)	
	
	VALUES (
	UuidToBin(UUID()),
	'Admin',
	3
	),
	(
	UuidToBin(UUID()),
	'Moderator',
	2
	),
   (
	UuidToBin(UUID()),
	'User',
	1
	)
	GO;
