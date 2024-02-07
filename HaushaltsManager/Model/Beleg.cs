namespace HaushaltsManager.Model;

public class Beleg
{
    public int Id { get; set; }
    public double Betrag { get; set; }
    public DateTime Datum { get; set; }
    public int KategorieId { get; set; }
    public string Name { get; set; }
    public string Beschreibung { get; set; }
    public int Jahr {  get; set; }

}
