-- TESTDATA Medarbejdere

INSERT INTO Medarbejder (Navn, Telefon, Email, ErLeder)
VALUES
('Pedro', '11111111', 'pedro@cantina.dk', 1),
('Jacoby', '22222222', 'jacoby@cantina.dk', 1),
('Anna', '33333333', 'anna@cantina.dk', 0),
('Emil', '44444444', 'emil@cantina.dk', 0),
('Sofie', '55555555', 'sofie@cantina.dk', 0);

-- TESTDATA Oktober-vagter
INSERT INTO Vagt (Dato, StartTid, SlutTid)
VALUES
('2026-10-01', '09:00', '14:00'),
('2026-10-01', '14:00', '19:00'),
('2026-10-02', '09:00', '14:00'),
('2026-10-02', '14:00', '19:00');

-- TESTDATA Relationer

INSERT INTO MedarbejderVagt (MedarbejderId, VagtId)
VALUES
(1, 1),
(3, 1),
(4, 1),

(2, 2),
(3, 2),
(5, 2),

(1, 3),
(4, 3),

(2, 4),
(3, 4),
(5, 4);
