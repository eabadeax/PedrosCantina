-- SE MÅNEDSPLAN
SELECT
    Vagt.Dato,
    FORMAT(StartTid, 'hh\:mm') AS StartTid,
    FORMAT(SlutTid, 'hh\:mm') AS SlutTid,
    Medarbejder.Navn,
    Medarbejder.ErLeder
FROM Vagt
JOIN MedarbejderVagt
    ON Vagt.VagtId = MedarbejderVagt.VagtId
JOIN Medarbejder
    ON MedarbejderVagt.MedarbejderId = Medarbejder.MedarbejderId
WHERE MONTH(Vagt.Dato) = 10
  AND YEAR(Vagt.Dato) = 2026
ORDER BY Vagt.Dato, Vagt.StartTid;


-- SE MÅNEDSBELASTNING FOR MEDARBEJDERE
SELECT
    Medarbejder.Navn,
    COUNT(MedarbejderVagt.VagtId) * 5 AS AntalTimer
FROM Medarbejder
JOIN MedarbejderVagt
    ON Medarbejder.MedarbejderId = MedarbejderVagt.MedarbejderId
JOIN Vagt
    ON MedarbejderVagt.VagtId = Vagt.VagtId
WHERE MONTH(Vagt.Dato) = 10
  AND YEAR(Vagt.Dato) = 2026
GROUP BY Medarbejder.Navn
ORDER BY AntalTimer DESC;


-- SE ÅRSBELASTNING FOR MEDARBEJDERE
SELECT
    Medarbejder.Navn,
    COUNT(MedarbejderVagt.VagtId) * 5 AS AntalTimer
FROM Medarbejder
JOIN MedarbejderVagt
    ON Medarbejder.MedarbejderId = MedarbejderVagt.MedarbejderId
JOIN Vagt
    ON MedarbejderVagt.VagtId = Vagt.VagtId
WHERE YEAR(Vagt.Dato) = 2026
GROUP BY
    Medarbejder.Navn
ORDER BY 
AntalTimer DESC;

    -- FÅ KONTAKTOPLYSNINGER PÅ LEDIGE MEDARBEJDERE
SELECT
    Medarbejder.Navn,
    Medarbejder.Telefon,
    Medarbejder.Email
FROM Medarbejder
WHERE Medarbejder.MedarbejderId NOT IN
(
    SELECT MedarbejderVagt.MedarbejderId
    FROM MedarbejderVagt
    WHERE MedarbejderVagt.VagtId = 1
);