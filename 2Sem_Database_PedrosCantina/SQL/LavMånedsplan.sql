-- LAV NY MÅNEDSPLAN
-- Opretter ny måned
DECLARE @Dato DATE = '2026-11-01';

WHILE @Dato <= '2026-11-30'
BEGIN
    INSERT INTO Vagt (Dato, StartTid, SlutTid)
    VALUES
    (@Dato, '09:00', '14:00'),
    (@Dato, '14:00', '19:00');

    SET @Dato = DATEADD(DAY, 1, @Dato);
END;

-- Tildeler herefter vagter til medarbejdere
INSERT INTO MedarbejderVagt (MedarbejderId, VagtId)
VALUES (1, 125)

INSERT INTO MedarbejderVagt (MedarbejderId, VagtId)
VALUES (3, 125)

INSERT INTO MedarbejderVagt (MedarbejderId, VagtId)
VALUES (4, 125)

-- Fjerner medarbejder fra vagt

DELETE FROM MedarbejderVagt
WHERE MedarbejderId = 4
  AND VagtId = 125;

