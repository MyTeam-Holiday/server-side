DELIMITER $$

CREATE PROCEDURE th_GetUsers()
BEGIN

SELECT UuidFromBin(GuId) AS GuId, Username, UserEmail, PasswordHash, PasswordSalt, HasVerified FROM `User`;
	
END $$