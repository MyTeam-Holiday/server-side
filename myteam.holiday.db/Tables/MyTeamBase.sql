CREATE TABLE [dbo].[MyTeamBase]
(
	GuId BINARY(16) NOT NULL PRIMARY KEY, 
	[BaseName] VARCHAR(50),
	[BaseSecret] VARCHAR(256)
)
