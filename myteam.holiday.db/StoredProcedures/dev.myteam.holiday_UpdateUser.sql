DELIMITER $$

CREATE PROCEDURE th_UpdateUser(
	IN GuIdIn VARCHAR(50),
    IN UserNameIn VARCHAR(50),
    IN UserEmailIn VARCHAR(50),
	IN PasswordHashIn VARCHAR(256),
	IN PasswordSaltIn VARCHAR(256)
	)
BEGIN

UPDATE `User`
	SET
    UserName = UserNameIn,
    UserEmail = UserEmailIn,
	PasswordHash = PasswordHashIn,
	PasswordSalt = PasswordSaltIn
	WHERE 
	UuidToBin(GuIdIn) = GuId;
	
SELECT UuidFromBin(GuId) AS GuId, 
    Username,
    UserEmail, 
    PasswordHash, 
    PasswordSalt, 
    HasVerified 
    FROM `User`
    WHERE
    GuId = UuidToBin(GuIdIn);

END $$
