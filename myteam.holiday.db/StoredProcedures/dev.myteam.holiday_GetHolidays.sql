DELIMITER $$

CREATE PROCEDURE th_GetHolidays()
BEGIN

SELECT UuidFromBin(GuId) AS GuId,
    HolidayName,
    HolidayStatus,
    HolidayTimeStamp,
    UuidFromBin(UserGuId) AS UserGuId
	FROM `Holiday`;

END $$