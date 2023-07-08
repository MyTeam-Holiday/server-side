CREATE TABLE [dbo].[MyTeamHoliday]
(
	GuId BINARY(16) NOT NULL PRIMARY KEY,              
	[HolidayName] VARCHAR(50),
	[HolidayStatus] INT,
	[HolidayTimeStamp] INT NOT NULL,
	[UserGuId] BINARY(16),
	--CONSTRAINT `fk_Holiday_User`
		--FOREIGN KEY (UserGuId) REFERENCES `User` (GuId)
		--ON DELETE CASCADE
		--ON UPDATE RESTRICT
)
