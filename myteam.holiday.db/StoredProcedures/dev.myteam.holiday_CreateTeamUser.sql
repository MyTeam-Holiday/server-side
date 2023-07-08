DELIMITER $$

CREATE PROCEDURE th_CreateTeamUser(
    IN teamGuIdIn VARCHAR(36),
    IN userGuIdIn VARCHAR(36)
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
        UuidToBin(userGuIdIn),
        UuidToBin(teamGuIdIn),
        UuidToBin('490764b4-47c4-11ed-9c61-b5ef8ef9351e')
    );

END $$