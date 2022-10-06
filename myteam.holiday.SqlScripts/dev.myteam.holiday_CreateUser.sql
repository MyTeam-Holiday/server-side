DELIMITER $$

CREATE PROCEDURE th_CreateUser(
	IN GuIdIn VARCHAR(50),
	IN PasswordHashIn VARCHAR(256),
	IN PasswordSaltIn VARCHAR(256),
	out GuIdOut VARCHAR(36)
	)
BEGIN

UPDATE `User`
	SET
	PasswordHash = PasswordHashIn,
	PasswordSalt = PasswordSaltIn,
	HasVefiried = TRUE;
	
SELECT UuidFromBin(GuId) INTO GuIdOut FROM `User`
	WHERE
	PasswordHash = PasswordHashIn;
	
END $$