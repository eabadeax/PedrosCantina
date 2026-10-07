
string connectionString =
    "Server=localhost;Database=PedrosCantina;Trusted_Connection=True;TrustServerCertificate=True;";

MedarbejderRepository repository =
    new MedarbejderRepository(connectionString);

// Create

/*Medarbejder medarbejder =
    new Medarbejder("Mads", "66666666", "mads@cantina.dk", false);

    repository.Add(medarbejder); */

// Read
List<Medarbejder> medarbejdere =
    repository.GetAll();

foreach (Medarbejder m in medarbejdere)
{
    Console.WriteLine(
        $"{m.MedarbejderId} - " +
        $"{m.Navn} - " +
        $"{m.Telefon} - " +
        $"{m.Email} - " +
        $"Leder: {m.ErLeder}"
    );
}

// Update

/*Medarbejder medarbejderTilUpdate =
    new Medarbejder("Mads", "66666667", "madsny@cantina.dk", false);

medarbejderTilUpdate.MedarbejderId = 6;

repository.Update(medarbejderTilUpdate); */

// Delete

//repository.Delete(6);



// Vis månedsplan
VagtplanRepository vagtplanRepository =
    new VagtplanRepository(connectionString);

vagtplanRepository.VisMånedsplan(10, 2026);

// Vis månedsbelastning
vagtplanRepository.VisMånedsbelastning(10, 2026);

// Vis årsbelastning
vagtplanRepository.VisÅrsbelastning(2026);

// Vis kontaktoplysninger for ledige medarbejdere
vagtplanRepository.VisKontaktVedSygdom(1);

// Opret månedsplan
// vagtplanRepository.OpretManedsplan(12, 2026);

// Tilføj medarbejder til vagt
//vagtplanRepository.TildelMedarbejderTilVagt(1, 369);

//Fjern medarbejder fra vagt
vagtplanRepository.FjernMedarbejderFraVagt(1, 369);

