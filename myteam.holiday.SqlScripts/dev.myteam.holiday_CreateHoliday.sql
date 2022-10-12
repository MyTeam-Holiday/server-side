DELIMITER $$

CREATE PROCEDURE th_CreateHoliday(
    IN HolidayNameIn VARCHAR(50),
    IN HolidayStatusIn INT,
    IN HolidayTimeStampIn INT,
    IN TeamGuIdIn VARCHAR(36),
    IN UserGuIdIn VARCHAR(36)
)

BEGIN 

DECLARE HolidayGuIdOut VARCHAR(36);

INSERT INTO `Holiday`(
    GuId,
    HolidayName,
    HolidayStatus,
    HolidayTimeStamp,
    UserGuId
    )
    VALUES(
    UuidToBin(UUID()),
    HolidayNameIn,
    HolidayStatusIn,
    HolidayTimeStampIn,
    UuidToBin(UserGuIdIn)
    );

SELECT UuidFromBin(GuId) INTO HolidayGuIdOut
    FROM Holiday
    WHERE 
    HolidayName = HolidayNameIn AND
    HolidayTimeStamp = HolidayTimeStampIn;

INSERT INTO `TeamHoliday`(
    GuId,
    HolidayGuId,
    TeamGuId
    )
    VALUES(
    UuidToBin(UUID()),
    UuidToBin(HolidayGuIdOut),
    UuidToBin(TeamGuIdIn)
    );  

END $$