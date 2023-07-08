DELIMITER $$

CREATE PROCEDURE th_GetTeamByName(
	IN TeamNameIn VARCHAR(50)
	)
BEGIN

SELECT UuidFromBin(GuId) AS GuId,
    TeamName,
    UuidFromBin(BaseGuId),
    PasswordHash,
    PasswordSalt, 
    InviteLink,
    HasPrivate,
    OnlyInvite
	FROM `Team`
	WHERE
	TeamName = TeamNameIn;
	
END $$