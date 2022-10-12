DELIMITER $$

CREATE PROCEDURE th_SetRoleToUser(
	IN UserGuIdIn VARCHAR(36),
    IN TeamGuIdIn VARCHAR(36),
    IN UserRoleGuIdIn VARCHAR(36)
	)
BEGIN

UPDATE `TeamUser`
    SET
    UserRoleGuId = UuidToBin(UserRoleGuIdIn)
	WHERE 
    UserGuId = UuidToBin(UserGuIdIn) AND
    TeamGuId = UuidToBin(TeamGuIdIn);


END $$