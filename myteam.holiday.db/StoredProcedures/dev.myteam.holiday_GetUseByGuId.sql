DELIMITER $$

CREATE PROCEDURE th_GetUserByGuId(
	IN GuIdIn VARCHAR(36),
	OUT userNameOut VARCHAR(50),
	OUT userEmailOut VARCHAR(50)
	)
BEGIN

SELECT Username, UserEmail INTO userNameOut, userEmailOut FROM `User`
	WHERE
	GuId = UuidToBin(GuIdIn);
	
END $$