DELIMITER $$

CREATE PROCEDURE th_CreateTeam(
	IN userGuIdIn VARCHAR(36),
	IN teamNameIn VARCHAR(50),
	IN baseGuIdIn VARCHAR(36),
	IN PasswordSaltIn VARCHAR(256),
    IN PasswordHashIn VARCHAR(256),
    IN HasPrivateIn BOOL,
    IN OnlyInviteIn BOOL
	)
	
BEGIN
DECLARE TeamGuIdOut VARCHAR(36);
INSERT INTO `Team`(
	GuId,
	TeamName,
	BaseGuId,
    PasswordSalt,
    PasswordHash,
    HasPrivate,
    OnlyInvite
	)
	VALUES (
	UuidToBin(UUID()),
	teamNameIn,
	UuidToBin(baseGuIdIn),
    PasswordSaltIn,
    PasswordHashIn,
    HasPrivateIn,
    OnlyInviteIn
	);
	


SELECT UuidFromBin(GuId) INTO TeamGuIdOut FROM `Team`
	WHERE
	TeamName = teamNameIn;
	
CALL th_CreateTeamUser(teamGuIdOut, userGuIdIn);

END $$
