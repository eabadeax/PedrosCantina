using Microsoft.Data.SqlClient;

public class MedarbejderRepository
{
    private readonly string _connectionString;

    public MedarbejderRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    // Create
    public void Add(Medarbejder medarbejder)
    {
        using SqlConnection connection = new SqlConnection(_connectionString);

        string sql = @"
        INSERT INTO Medarbejder
        (Navn, Telefon, Email, ErLeder)
        VALUES
        (@Navn, @Telefon, @Email, @ErLeder)";

        using SqlCommand command = new SqlCommand(sql, connection);

        command.Parameters.AddWithValue("@Navn", medarbejder.Navn);
        command.Parameters.AddWithValue("@Telefon", medarbejder.Telefon);
        command.Parameters.AddWithValue("@Email", medarbejder.Email);
        command.Parameters.AddWithValue("@ErLeder", medarbejder.ErLeder);

        connection.Open();
        command.ExecuteNonQuery();
    }

    // Read
    public List<Medarbejder> GetAll()
    {
        List<Medarbejder> medarbejdere = new List<Medarbejder>();

        using SqlConnection connection =
            new SqlConnection(_connectionString);

        string sql = "SELECT * FROM Medarbejder";

        using SqlCommand command =
            new SqlCommand(sql, connection);

        connection.Open();

        using SqlDataReader reader = command.ExecuteReader();

        while (reader.Read())
        {
            Medarbejder medarbejder = new Medarbejder(
                reader["Navn"].ToString(),
                reader["Telefon"].ToString(),
                reader["Email"].ToString(),
                (bool)reader["ErLeder"]
            );

            medarbejder.MedarbejderId =
                (int)reader["MedarbejderId"];

            medarbejdere.Add(medarbejder);
        }

        return medarbejdere;
    }

    // Update
    public void Update(Medarbejder medarbejder)
    {
        using SqlConnection connection =
            new SqlConnection(_connectionString);

        string sql = @"
        UPDATE Medarbejder
        SET Navn = @Navn,
            Telefon = @Telefon,
            Email = @Email,
            ErLeder = @ErLeder
        WHERE MedarbejderId = @MedarbejderId";

        using SqlCommand command =
            new SqlCommand(sql, connection);

        command.Parameters.AddWithValue("@Navn", medarbejder.Navn);
        command.Parameters.AddWithValue("@Telefon", medarbejder.Telefon);
        command.Parameters.AddWithValue("@Email", medarbejder.Email);
        command.Parameters.AddWithValue("@ErLeder", medarbejder.ErLeder);
        command.Parameters.AddWithValue("@MedarbejderId", medarbejder.MedarbejderId);

        connection.Open();

        command.ExecuteNonQuery();
    }

    // Delete
    public void Delete(int medarbejderId)
    {
        using SqlConnection connection =
            new SqlConnection(_connectionString);

        string sql = @"
        DELETE FROM Medarbejder
        WHERE MedarbejderId = @MedarbejderId";

        using SqlCommand command =
            new SqlCommand(sql, connection);

        command.Parameters.AddWithValue(
            "@MedarbejderId",
            medarbejderId
        );

        connection.Open();

        command.ExecuteNonQuery();
    }
}