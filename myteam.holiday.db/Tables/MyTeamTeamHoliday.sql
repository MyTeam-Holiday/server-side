CREATE TABLE [dbo].[MyTeamTeamHoliday]
(
	GuId BINARY(16) NOT NULL PRIMARY KEY, 
	[HolidayGuId] BINARY(16),
	[TeamGuId] BINARY(16),
	--CONSTRAINT 'fk_TeamHoliday_Holiday'
	--FOREIGN KEY (HolidayGuId) REFERENCES 'Holiday' (GuId)
	--ON DELETE CASCADE
	--ON UPDATE RESTRICT,
	
	--CONSTRAINT 'fk_TeamHoliday_Team'
		--FOREIGN KEY (TeamGuId) REFERENCES `Team` (GuId)
		--ON DELETE CASCADE
		--ON UPDATE RESTRICT
		
)
