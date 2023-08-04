-- Switch to the database context
USE `dev.myteam`;

-- Create the AspNetRoles table
CREATE TABLE AspNetRoles (
    Id VARCHAR(255) NOT NULL,
    Name VARCHAR(256),
    NormalizedName VARCHAR(256),
    ConcurrencyStamp LONGTEXT,
    PRIMARY KEY (Id)
);

-- Create the AspNetUsers table
CREATE TABLE AspNetUsers (
    Id VARCHAR(255) NOT NULL,
    UserName VARCHAR(256),
    NormalizedUserName VARCHAR(256),
    Email VARCHAR(256),
    NormalizedEmail VARCHAR(256),
    EmailConfirmed TINYINT NOT NULL,
    PasswordHash LONGTEXT,
    SecurityStamp LONGTEXT,
    ConcurrencyStamp LONGTEXT,
    PhoneNumber LONGTEXT,
    PhoneNumberConfirmed TINYINT NOT NULL,
    TwoFactorEnabled TINYINT NOT NULL,
    LockoutEnd DATETIME(6),
    LockoutEnabled TINYINT NOT NULL,
    AccessFailedCount INT NOT NULL,
	EmailConfirmationAttempts INT NOT NULL,
    PRIMARY KEY (Id)
);

-- Create the AspNetRoleClaims table
CREATE TABLE AspNetRoleClaims (
    Id INT AUTO_INCREMENT NOT NULL,
    RoleId VARCHAR(255) NOT NULL,
    ClaimType LONGTEXT,
    ClaimValue LONGTEXT,
    PRIMARY KEY (Id),
    CONSTRAINT FK_AspNetRoleClaims_AspNetRoles_RoleId FOREIGN KEY (RoleId)
        REFERENCES AspNetRoles (Id) ON DELETE CASCADE
);

-- Create the AspNetUserClaims table
CREATE TABLE AspNetUserClaims (
    Id INT AUTO_INCREMENT NOT NULL,
    UserId VARCHAR(255) NOT NULL,
    ClaimType LONGTEXT,
    ClaimValue LONGTEXT,
    PRIMARY KEY (Id),
    CONSTRAINT FK_AspNetUserClaims_AspNetUsers_UserId FOREIGN KEY (UserId)
        REFERENCES AspNetUsers (Id) ON DELETE CASCADE
);

-- Create the AspNetUserLogins table
CREATE TABLE AspNetUserLogins (
    LoginProvider VARCHAR(128) NOT NULL,
    ProviderKey VARCHAR(128) NOT NULL,
    ProviderDisplayName LONGTEXT,
    UserId VARCHAR(255) NOT NULL,
    PRIMARY KEY (LoginProvider, ProviderKey),
    CONSTRAINT FK_AspNetUserLogins_AspNetUsers_UserId FOREIGN KEY (UserId)
        REFERENCES AspNetUsers (Id) ON DELETE CASCADE
);

-- Create the AspNetUserRoles table
CREATE TABLE AspNetUserRoles (
    UserId VARCHAR(255) NOT NULL,
    RoleId VARCHAR(255) NOT NULL,
    PRIMARY KEY (UserId, RoleId),
    CONSTRAINT FK_AspNetUserRoles_AspNetRoles_RoleId FOREIGN KEY (RoleId)
        REFERENCES AspNetRoles (Id) ON DELETE CASCADE,
    CONSTRAINT FK_AspNetUserRoles_AspNetUsers_UserId FOREIGN KEY (UserId)
        REFERENCES AspNetUsers (Id) ON DELETE CASCADE
);

-- Create the AspNetUserTokens table
CREATE TABLE AspNetUserTokens (
    UserId VARCHAR(255) NOT NULL,
    LoginProvider VARCHAR(128) NOT NULL,
    Name VARCHAR(128) NOT NULL,
    Value LONGTEXT,
    PRIMARY KEY (UserId, LoginProvider, Name),
    CONSTRAINT FK_AspNetUserTokens_AspNetUsers_UserId FOREIGN KEY (UserId)
        REFERENCES AspNetUsers (Id) ON DELETE CASCADE
);


-- Create the Roles table
CREATE TABLE Roles (
	Id VARCHAR(255) NOT NULL,
    Name VARCHAR(128),
    PRIMARY KEY (Id)	
);

-- Create the Teams table
CREATE TABLE Teams (
	Id VARCHAR(255) NOT NULL,
	Name VARCHAR(128) NOT NULL,
	PasswordHash VARCHAR(256) NULL,
	PasswordSalt VARCHAR(256) NULL,
	InviteLink VARCHAR(256) NULL,
	HasPrivate BOOL,
	OnlyInvite BOOL,
	PRIMARY KEY (Id)
);

-- Create the Holidays table
CREATE TABLE Holidays (
	Id VARCHAR(255) NOT NULL,
	Name VARCHAR (128) NOT NULL,
	HolidayStatus INT,
	HolidayTimeStamp INT NOT NULL,
	UserId VARCHAR(255),
	PRIMARY KEY (Id),
	CONSTRAINT FK_Holidays_AspNetUsers_UserId
		FOREIGN KEY (UserId) REFERENCES AspNetUsers (Id)
		ON DELETE CASCADE
		ON UPDATE RESTRICT
);

-- Create the TeamHolidays table
CREATE TABLE TeamHolidays (
	Id VARCHAR(255) NOT NULL,
	HolidayId VARCHAR(255) NOT NULL,
	TeamId VARCHAR(255) NOT NULL,
	PRIMARY KEY (Id),
	CONSTRAINT FK_TeamHolidays_Holidays_HolidayId
		FOREIGN KEY (HolidayId) REFERENCES Holidays (Id)
		ON DELETE CASCADE
		ON UPDATE RESTRICT,
		
	CONSTRAINT FK_TeamHolidays_Teams_TeamId
		FOREIGN KEY (TeamId) REFERENCES Teams (Id)
		ON DELETE CASCADE
		ON UPDATE RESTRICT
);

-- Create the TeamUsers table
CREATE TABLE TeamUsers (
	Id VARCHAR(255) NOT NULL,
	UserId VARCHAR(255) NOT NULL,
	TeamId VARCHAR(255) NOT NULL,
	RoleId VARCHAR(255) NOT NULL,
	PRIMARY KEY (Id),
	CONSTRAINT FK_TeamUsers_AspNetUsers_UserId
		FOREIGN KEY (UserId) REFERENCES AspNetUsers (Id)
		ON DELETE CASCADE
		ON UPDATE RESTRICT,
	
	CONSTRAINT FK_TeamUsers_Teams_TeamId
		FOREIGN KEY (TeamId) REFERENCES Teams (Id)
		ON DELETE CASCADE
		ON UPDATE RESTRICT,
		
	CONSTRAINT FK_TeamUsers_Roles_RoleId
		FOREIGN KEY (RoleId) REFERENCES Roles (Id)
		ON DELETE CASCADE
		ON UPDATE RESTRICT
);