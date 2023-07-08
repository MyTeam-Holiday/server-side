DELIMITER $$

CREATE PROCEDURE th_DeleteUser(
	IN GuIdIn VARCHAR(36)
	)
BEGIN

DELETE FROM `User`
    WHERE 
    GuId = UuidToBin(GuIdIn);

END $$
