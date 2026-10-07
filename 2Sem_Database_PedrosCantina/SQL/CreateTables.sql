CREATE TABLE Medarbejder
(
    MedarbejderId INT IDENTITY(1,1) PRIMARY KEY,
    Navn NVARCHAR(100) NOT NULL,
    Telefon NVARCHAR(20),
    Email NVARCHAR(100),
    ErLeder BIT NOT NULL DEFAULT 0
);

CREATE TABLE Vagt
(
    VagtId INT IDENTITY(1,1) PRIMARY KEY,
    Dato DATE NOT NULL,
    StartTid TIME NOT NULL,
    SlutTid TIME NOT NULL
);

CREATE TABLE MedarbejderVagt
(
    MedarbejderId INT NOT NULL,
    VagtId INT NOT NULL,

    PRIMARY KEY (MedarbejderId, VagtId),

    FOREIGN KEY (MedarbejderId)
        REFERENCES Medarbejder(MedarbejderId),

    FOREIGN KEY (VagtId)
        REFERENCES Vagt(VagtId)
);