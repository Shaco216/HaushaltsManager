namespace HaushaltsManager.Model;

public class Beleg
{
    private float _beleg;
    public int Id { get; set; }
    public string Betrag { get; set; }
    public float BetragNum 
    {
        get {return float.Parse(Betrag); }
    }
    public string Datum { get; set; }
    public int KategorieId { get; set; }
    public string? Kategorie { get; set; }
    public string Name { get; set; }
    public string? Beschreibung { get; set; }
    public int Jahr {  get; set; }
    public string? Speicherpfad { get; set; }
    public int PersonId { get; set; }
    public string? Person { get; set; }

}
