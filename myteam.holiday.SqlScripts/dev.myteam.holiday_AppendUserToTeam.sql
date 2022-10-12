DELIMITER $$

CREATE PROCEDURE th_AppendUserFromTeam(
	IN TeamGuIdIn VARCHAR(36),
    IN UserGuIdIn VARCHAR(36)
	)
BEGIN

INSERT INTO `TeamUser`(
    GuId,
    UserGuId,
    TeamGuId,
    UserRoleGuId
    )
    VALUES(
    UuidToBin(UUID()),    
    UuidToBin(UserGuIdIn),
    UuidToBin(TeamGuIdIn),
    UuidToBin('490769ae-47c4-11ed-9c61-b5ef8ef9351e')
    );

END $$