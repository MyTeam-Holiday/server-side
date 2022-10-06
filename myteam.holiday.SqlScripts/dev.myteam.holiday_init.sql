CREATE DATABASE `TeamHoliday`;

USE TeamHoliday;

CREATE FUNCTION UuidToBin(_uuid BINARY(36))
  RETURNS BINARY(16)
  LANGUAGE SQL  DETERMINISTIC  CONTAINS SQL  SQL SECURITY INVOKER
RETURN
  UNHEX(CONCAT(
      SUBSTR(_uuid, 15, 4),
      SUBSTR(_uuid, 10, 4),
      SUBSTR(_uuid,  1, 8),
      SUBSTR(_uuid, 20, 4),
      SUBSTR(_uuid, 25) ));

CREATE FUNCTION UuidFromBin(_bin BINARY(16))
  RETURNS BINARY(36)
  LANGUAGE SQL  DETERMINISTIC  CONTAINS SQL  SQL SECURITY INVOKER
RETURN
  LCASE(CONCAT_WS('-',
      HEX(SUBSTR(_bin,  5, 4)),
      HEX(SUBSTR(_bin,  3, 2)),
      HEX(SUBSTR(_bin,  1, 2)),
      HEX(SUBSTR(_bin,  9, 2)),
      HEX(SUBSTR(_bin, 11))
           ));

CREATE TABLE `User` (
	GuId VARCHAR(36) NOT NULL PRIMARY KEY,              
	Username VARCHAR(50),
	UserEmail VARCHAR(50),
	PasswordHash VARCHAR (256) NULL,
	PasswordSalt VARCHAR(256) NULL,
	HasVefiried BOOL NOT NULL DEFAULT FALSE
	);

CREATE TABLE `UserRole` (
	GuId VARCHAR(36) NOT NULL PRIMARY KEY,              
	RoleName VARCHAR(50),
	RoleStatus INT NOT NULL
	);
	
CREATE TABLE `Base` (
	GuId VARCHAR(36) NOT NULL PRIMARY KEY, 
	BaseName VARCHAR(50),
	BaseSecret VARCHAR(256)
	);
	
CREATE TABLE `Team` (
	GuId VARCHAR(36) NOT NULL PRIMARY KEY, 
	TeamName VARCHAR(50) NOT NULL,
	BaseGuId VARCHAR(36) NOT NULL,
	PasswordHash VARCHAR(256) NULL,
	PasswordSalt VARCHAR(256) NULL,
	InviteLink VARCHAR(256) NULL,
	HasPrivate BOOL,
	OnlyInvite BOOL,
	CONSTRAINT `fk_Team_Base`
		FOREIGN KEY (BaseGuId) REFERENCES `Base` (GuId)
		ON DELETE CASCADE
		ON UPDATE RESTRICT
	);
	
CREATE TABLE `TeamUser` (
	GuId VARCHAR(36) NOT NULL PRIMARY KEY, 
	UserGuId VARCHAR(36),
	TeamGuId VARCHAR(36),
	UserRoleGuId VARCHAR(36),
	
	CONSTRAINT `fk_TeamUser_User`
		FOREIGN KEY (UserGuId) REFERENCES `User` (GuId)
		ON DELETE CASCADE
		ON UPDATE RESTRICT,
		
	CONSTRAINT `fk_TeamUser_Team`
		FOREIGN KEY (TeamGuId) REFERENCES `Team` (GuId)
		ON DELETE CASCADE
		ON UPDATE RESTRICT,
		
	CONSTRAINT `fk_TeamUser_UserRole`
		FOREIGN KEY (UserRoleGuId) REFERENCES `UserRole` (GuId)
		ON DELETE CASCADE
		ON UPDATE RESTRICT		
	);
	
CREATE TABLE `Holiday` (
	GuId VARCHAR(36) NOT NULL PRIMARY KEY,              
	HolidayName VARCHAR(50),
	HolidayStatus INT,
	HolidayTimeStamp INT NOT NULL,
	UserGuId VARCHAR(36),
	CONSTRAINT `fk_Holiday_User`
		FOREIGN KEY (UserGuId) REFERENCES `User` (GuId)
		ON DELETE CASCADE
		ON UPDATE RESTRICT
	);	
	
CREATE TABLE `TeamHoliday` (
	GuId VARCHAR(36) NOT NULL PRIMARY KEY, 
	HolidayGuId VARCHAR(36),
	TeamGuId VARCHAR(36),
	CONSTRAINT `fk_TeamHoliday_Holiday`
	FOREIGN KEY (HolidayGuId) REFERENCES `Holiday` (GuId)
	ON DELETE CASCADE
	ON UPDATE RESTRICT,
	
	CONSTRAINT `fk_TeamHoliday_Team`
		FOREIGN KEY (TeamGuId) REFERENCES `Team` (GuId)
		ON DELETE CASCADE
		ON UPDATE RESTRICT
		
		
	);	
	
INSERT INTO `UserRole` (
	Guid,
	RoleName,
	RoleStatus
	)	
	
	VALUES (
	UuidToBin(UUID()),
	"Admin",
	3
	),
	(
	UuidToBin(UUID()),
	"Moderator",
	2
	),
   (
	UuidToBin(UUID()),
	"User",
	1
	);