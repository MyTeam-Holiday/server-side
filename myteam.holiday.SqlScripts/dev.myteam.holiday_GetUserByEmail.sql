DELIMITER $$

CREATE PROCEDURE th_GetUserByEmail(
	IN Email VARCHAR(50)
	)
BEGIN

SELECT UuidFromBin(GuId) AS GuId, Username, UserEmail, PasswordHash, PasswordSalt, HasVerified
	FROM `User`
	WHERE
	UserEmail = Email;
	
END $$