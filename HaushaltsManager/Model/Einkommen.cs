namespace HaushaltsManager.Model
{
    public class Einkommen
    {
        public long Id { get; set; }
        public int PersonId { get; set; }
        public int Jahr {  get; set; }
        public string Name { get; set; }
        public double Wert { get; set; }
        public EinnahmeFrequenz EinnahmeHaeufigkeit { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
