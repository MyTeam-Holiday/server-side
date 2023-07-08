DELIMITER $$

CREATE PROCEDURE th_PreCreateUser(
	IN userNameIn VARCHAR(50),
	IN userEmailIn VARCHAR(50),
	out GuIdOut VARCHAR(36)
	)
BEGIN

INSERT INTO `User`(
	GuId,
	Username,
	UserEmail
	)
	VALUES (
	UuidToBin(UUID()),
	userNameIn,
	userEmailIn
	);
	
SELECT UuidFromBin(GuId) INTO GuIdOut FROM `User`
	WHERE
	Username = userNameIn AND
	UserEmail = userEmailIn;
	
END $$
