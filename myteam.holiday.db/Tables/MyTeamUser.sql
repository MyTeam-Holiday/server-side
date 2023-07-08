CREATE TABLE [dbo].[MyTeamUser]
(
	GuId BINARY(16) NOT NULL PRIMARY KEY,              
	[userName] VARCHAR(50),
	[userEmail] VARCHAR(50),
	[passwordHash] VARCHAR (256) NULL,
	[passwordSalt] VARCHAR(256) NULL,
	[hasVefiried] Bit NOT NULL DEFAULT 0, 
    [dateCreated] DATETIME NOT NULL 
)
