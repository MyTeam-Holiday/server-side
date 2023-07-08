DELIMITER $$

CREATE PROCEDURE th_GetHolidayByName(
	IN HolidayNameIn VARCHAR(50)
	)
BEGIN

SELECT UuidFromBin(GuId) AS GuId,
    HolidayName,
    HolidayStatus,
    HolidayTimeStamp,
    UuidFromBin(UserGuId) AS UserGuId
	FROM `Holiday`
	WHERE
	HolidayName = HolidayNameIn;
	
END $$