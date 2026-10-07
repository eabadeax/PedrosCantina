public class Medarbejder
{
    private int _medarbejderId;
    private string _navn;
    private string _telefon;
    private string _email;
    private bool _erLeder;

    public int MedarbejderId
    {
        get {  return _medarbejderId; }
        set {  _medarbejderId = value;}
    }
    public string Navn
    {
        get { return _navn; }
        set { _navn = value; }
    }
    public string Telefon
    {
        get { return _telefon; }
        set { _telefon = value; }
    }
    public string Email
    {
        get { return _email; }
        set { _email = value; }
    }
    public bool ErLeder
    {
        get { return _erLeder; }
        set { _erLeder = value; }
    }

    public Medarbejder()
    {
    }

    public Medarbejder(string Navn, string Telefon, string Email, bool ErLeder)
    {
        _navn = Navn;
        _telefon = Telefon;
        _email = Email;
        _erLeder = ErLeder;
    }
}
