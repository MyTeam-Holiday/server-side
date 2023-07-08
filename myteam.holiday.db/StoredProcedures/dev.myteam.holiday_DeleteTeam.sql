DELIMITER $$

CREATE PROCEDURE th_DeleteTeam(
	IN GuIdIn VARCHAR(36)
	)
BEGIN

DELETE FROM `Team`
    WHERE 
    GuId = UuidToBin(GuIdIn);

END $$
