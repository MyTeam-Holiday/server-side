DELIMITER $$

CREATE PROCEDURE th_UpdateTeam(
	IN GuIdIn VARCHAR(50),
    IN TeamNameIn VARCHAR(50),
    IN BaseGuIdIn VARCHAR(36),
	IN PasswordHashIn VARCHAR(256),
	IN PasswordSaltIn VARCHAR(256),
    IN InviteLinkIn VARCHAR(256),
    IN HasPrivateIn BOOL,
    IN OnlyInviteIn BOOL
	)
BEGIN

UPDATE `Team`
	SET
    TeamName = TeamNameIn,
    BaseGuId = UuidToBin(BaseGuIdIn),
	PasswordHash = PasswordHashIn,
	PasswordSalt = PasswordSaltIn,
    InviteLink = InviteLinkIn,
    HasPrivate = HasPrivateIn,
    OnlyInvite = OnlyInviteIn
	WHERE 
	UuidToBin(GuIdIn) = GuId;
	
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
