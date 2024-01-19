namespace HaushaltsManager.Model;

public class Beleg
{
    public Guid Id { get; set; }
    public double Betrag { get; set; }
    public DateTime Datum { get; set; }
    public DateTime Time { get; set; }
    public Kategorie Kategorie { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

}
