DELIMITER $$

CREATE PROCEDURE th_GetTeams()
BEGIN

SELECT UuidFromBin(GuId) AS GuId,
    TeamName,
    UuidFromBin(BaseGuId),
    PasswordHash,
    PasswordSalt, 
    InviteLink,
    HasPrivate,
    OnlyInvite
	FROM `Team`;
	
END $$