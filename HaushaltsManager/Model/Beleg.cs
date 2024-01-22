namespace HaushaltsManager.Model;

public class Beleg
{
    public int Id { get; set; }
    public double Betrag { get; set; }
    public DateTime Datum { get; set; }
    public DateTime Zeit { get; set; }
    public Kategorie Kategorie { get; set; }
    public string Name { get; set; }
    public string Beschreibung { get; set; }

}
