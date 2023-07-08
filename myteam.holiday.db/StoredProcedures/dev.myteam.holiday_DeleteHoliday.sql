DELIMITER $$

CREATE PROCEDURE th_DeleteHoliday(
    IN HolidayGuIdIn VARCHAR(36)
)

BEGIN 

DELETE FROM `Holiday`
    WHERE 
    GuId = UuidToBin(HolidayGuIdIn);

DELETE FROM `TeamHoliday`
    WHERE 
    HolidayGuId = UuidToBin(HolidayGuIdIn);

END $$