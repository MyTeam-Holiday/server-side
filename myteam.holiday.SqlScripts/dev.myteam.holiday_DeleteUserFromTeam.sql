DELIMITER $$

CREATE PROCEDURE th_DeleteUserFromTeam(
	IN TeamGuIdIn VARCHAR(36),
    IN UserGuIdIn VARCHAR(36)
	)
BEGIN

DELETE FROM `TeamUser`
    WHERE 
    TeamGuId = UuidToBin(TeamGuIdIn) AND 
    UserGuId = UuidToBin(UserGuIdIn);

END $$
