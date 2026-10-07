using Microsoft.Data.SqlClient;

public class VagtplanRepository
{
    private string _connectionString;

    public VagtplanRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    // VIS MÅNEDSPLAN
    public void VisMånedsplan(int måned, int år)
    {
        using SqlConnection connection = new SqlConnection(_connectionString);

        string sql = @"
            SELECT
                Vagt.Dato,
                FORMAT(Vagt.StartTid, 'hh\:mm') AS StartTid,
                FORMAT(Vagt.SlutTid, 'hh\:mm') AS SlutTid,
                Medarbejder.Navn,
                Medarbejder.ErLeder
            FROM Vagt
            JOIN MedarbejderVagt
                ON Vagt.VagtId = MedarbejderVagt.VagtId
            JOIN Medarbejder
                ON MedarbejderVagt.MedarbejderId = Medarbejder.MedarbejderId
            WHERE MONTH(Vagt.Dato) = @Maned
              AND YEAR(Vagt.Dato) = @Ar
            ORDER BY Vagt.Dato, Vagt.StartTid;";

        using SqlCommand command =
            new SqlCommand(sql, connection);

        command.Parameters.AddWithValue("@Maned", måned);
        command.Parameters.AddWithValue("@Ar", år);

        connection.Open();

        using SqlDataReader reader = command.ExecuteReader();

        while (reader.Read())
        {
            Console.WriteLine(
                $"{reader["Dato"]} | " +
                $"{reader["StartTid"]} - {reader["SlutTid"]} | " +
                $"{reader["Navn"]} | " +
                $"Leder: {reader["ErLeder"]}"
            );
        }
    }


    // VIS MÅNEDSBELASTNING

    public void VisMånedsbelastning(int måned, int år)
    {
        using SqlConnection connection =
            new SqlConnection(_connectionString);

        string sql = @"
        SELECT
            Medarbejder.Navn,
            COUNT(MedarbejderVagt.VagtId) * 5 AS AntalTimer
        FROM Medarbejder
        JOIN MedarbejderVagt
            ON Medarbejder.MedarbejderId = MedarbejderVagt.MedarbejderId
        JOIN Vagt
            ON MedarbejderVagt.VagtId = Vagt.VagtId
        WHERE MONTH(Vagt.Dato) = @Maned
          AND YEAR(Vagt.Dato) = @Ar
        GROUP BY Medarbejder.Navn
        ORDER BY AntalTimer DESC;";

        using SqlCommand command =
            new SqlCommand(sql, connection);

        command.Parameters.AddWithValue("@Maned", måned);
        command.Parameters.AddWithValue("@Ar", år);

        connection.Open();

        using SqlDataReader reader = command.ExecuteReader();

        while (reader.Read())
        {
            Console.WriteLine(
                $"{reader["Navn"]} - " +
                $"{reader["AntalTimer"]} timer"
            );
        }
    }

    // VIS ÅRSBELASTNING
    public void VisÅrsbelastning(int år)
    {
        using SqlConnection connection =
            new SqlConnection(_connectionString);

        string sql = @"
        SELECT
            Medarbejder.Navn,
            COUNT(MedarbejderVagt.VagtId) * 5 AS AntalTimer
        FROM Medarbejder
        JOIN MedarbejderVagt
            ON Medarbejder.MedarbejderId = MedarbejderVagt.MedarbejderId
        JOIN Vagt
            ON MedarbejderVagt.VagtId = Vagt.VagtId
        WHERE YEAR(Vagt.Dato) = @Ar
        GROUP BY
            Medarbejder.Navn
        ORDER BY
            AntalTimer DESC;";
        using SqlCommand command =
            new SqlCommand(sql, connection);

        command.Parameters.AddWithValue("@Ar", år);

        connection.Open();

        using SqlDataReader reader = command.ExecuteReader();

        while (reader.Read())
        {
            Console.WriteLine(
                $"{reader["Navn"]} - " +
                $"{år}: " +
                $"{reader["AntalTimer"]} timer"
            );
        }
    }

    // KONTAKT VED SYGDOM

    public void VisKontaktVedSygdom(int vagtId)
    {
        using SqlConnection connection =
            new SqlConnection(_connectionString);

        string sql = @"
        SELECT
            Medarbejder.Navn,
            Medarbejder.Telefon,
            Medarbejder.Email
        FROM Medarbejder
        WHERE Medarbejder.MedarbejderId NOT IN
        (
            SELECT MedarbejderVagt.MedarbejderId
            FROM MedarbejderVagt
            WHERE MedarbejderVagt.VagtId = @VagtId
        );";

        using SqlCommand command =
            new SqlCommand(sql, connection);

        command.Parameters.AddWithValue("@VagtId", vagtId);

        connection.Open();

        using SqlDataReader reader = command.ExecuteReader();

        while (reader.Read())
        {
            Console.WriteLine(
                $"{reader["Navn"]} - " +
                $"{reader["Telefon"]} - " +
                $"{reader["Email"]}"
            );
        }
    }

    // OPRET MÅNEDSPLAN

    public void OpretManedsplan(int maned, int ar)
    {
        using SqlConnection connection =
            new SqlConnection(_connectionString);

        DateTime startDato = new DateTime(ar, maned, 1);
        DateTime slutDato = startDato.AddMonths(1).AddDays(-1);

        string sql = @"
        DECLARE @Dato DATE = @StartDato;

        WHILE @Dato <= @SlutDato
        BEGIN
            INSERT INTO Vagt (Dato, StartTid, SlutTid)
            VALUES
            (@Dato, '09:00', '14:00'),
            (@Dato, '14:00', '19:00');

            SET @Dato = DATEADD(DAY, 1, @Dato);
        END;";

        using SqlCommand command =
            new SqlCommand(sql, connection);

        command.Parameters.AddWithValue("@StartDato", startDato);
        command.Parameters.AddWithValue("@SlutDato", slutDato);

        connection.Open();

        command.ExecuteNonQuery();
    }

    // TILDEL MEDARBEJDER TIL VAGT

    public void TildelMedarbejderTilVagt(int medarbejderId, int vagtId)
    {
        using SqlConnection connection =
            new SqlConnection(_connectionString);

        string sql = @"
        INSERT INTO MedarbejderVagt
        (MedarbejderId, VagtId)
        VALUES
        (@MedarbejderId, @VagtId);";

        using SqlCommand command =
            new SqlCommand(sql, connection);

        command.Parameters.AddWithValue("@MedarbejderId", medarbejderId);
        command.Parameters.AddWithValue("@VagtId", vagtId);

        connection.Open();

        command.ExecuteNonQuery();
    }

    public void FjernMedarbejderFraVagt(int medarbejderId, int vagtId)
    {
        using SqlConnection connection =
            new SqlConnection(_connectionString);

        string sql = @"
        DELETE FROM MedarbejderVagt
        WHERE MedarbejderId = @MedarbejderId
          AND VagtId = @VagtId;";

        using SqlCommand command =
            new SqlCommand(sql, connection);

        command.Parameters.AddWithValue("@MedarbejderId", medarbejderId);
        command.Parameters.AddWithValue("@VagtId", vagtId);

        connection.Open();

        command.ExecuteNonQuery();
    }


}
