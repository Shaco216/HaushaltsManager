using HaushaltsManager.Model;

namespace HaushaltsManager.Repository;

public static class SQLStatementProvider
{
    //public static string GatherTableNames = "SELECT name FROM sqlite_master WHERE type = 'table';";
    public static string CreateBelegTable = $"Create Table if not exists 'Belege' " +
                    $"(Id BigInt AUTO_INCREMENT, " +
                    $"Jahr Int, " +
                    $"Name varchar(255), " +
                    $"Beschreibung varchar(2000), " +
                    $"Datum varchar(255), " +
                    $"KategorieId Int, " +
                    $"Betrag float, " +
                    $"Speicherpfad varchar(2000)," +
                    $"PersonId Int," +
                    $"Primary Key(Id)," +
                    $"Foreign Key(Jahr) References Years(Jahr)," +
                    $"Foreign Key(KategorieId) References Kategorien(Id));";
    public static string SelectBelegeFromYear = @"Select * from Belege where Jahr = '@Year';";
    public static string SelectBelegeFromKategorieId = "Select * from Belege where KategorieId = '@KategorieId';";
    public static string InsertBeleg = "Insert Into Belege " +
        "(" +
        "Id," +
        "Jahr," +
        "Name," +
        "Beschreibung," +
        "Datum," +
        "KategorieId," +
        "Betrag," +
        "Speicherpfad," +
        "PersonId" +
        ") Values ('@Id','@Jahr','@Name','@Beschreibung','@Datum','@KategorieId','@Betrag','@Speicherpfad','@PersonId');";
    public static string InsertBelege(int id, int jahr, string name, string beschreibung, string datum, int kategorieId, double betrag, string speicherpfad, int personId)
    {
        string insertbelegsql = @$"Insert Into Belege 
        (
        Id,
        Jahr,
        Name,
        Beschreibung,
        Datum,
        KategorieId,
        Betrag,
        Speicherpfad,
        PersonId
        ) Values ('{id}','{jahr}','{name}','{beschreibung}','{datum}','{kategorieId}','{betrag}','{speicherpfad}','{personId}');";
        return insertbelegsql;
    }
    public static string UpdateBeleg = "Update Belege set " +
        "Jahr = '@Jahr'," +
        "Name = '@Name'," +
        "Beschreibung = '@Beschreibung'," +
        "Datum = '@Datum'," +
        "KategorieId = '@KategorieId'," +
        "Betrag = '@Betrag'," +
        "Speicherpfad = '@Speicherpfad'" +
        "PersonId = '@PersonId'" +
        "Where Id = '@Id';";
    public static string UpdateBelege(int id, int jahr, string name, string beschreibung, string datum, int kategorieId, float betrag, string speicherpfad, int personId)
    {
        string updatesql = @$"Update Belege set
        Jahr = '{jahr}',
        Name = '{name}',
        Beschreibung = '{beschreibung}',
        Datum = '{datum}',
        KategorieId = '{kategorieId}',
        Betrag = '{betrag}',
        Speicherpfad = '{speicherpfad}'
        PersonId = '{personId}'
        Where Id = '{id}';";
        return updatesql;
    }
    public static string UpdateBelegbyYear = "Update Belege set Jahr = '@Year' where Id = '@Id';";
    public static string UpdateBelegbyKategorie = "Update Belege set KategorieId = '@KategorieId' where Id = '@Id';";
    public static string DeleteBeleg = "Delete from Belege where Id = '@Id';";
    public static string DeleteBelegFromYear = "Delete from Belege where Jahr = '@Year';";

    public static string CreateYearsTable = "Create Table if not exists 'Years' (Jahr int Not Null, Primary Key(Jahr));";
    public static string GatherYears = "Select * from Years;";
    public static string InsertYear = "Insert Into Years (Jahr) Values (@Year);";
    public static string UpdateYear = "Update Years set Jahr = @Year where @PrevYear;";
    public static string DeleteYear = "Delete from Years where @Year";

    public static string CreateArchivedBelegeTable = @"Create Table if not exists 'ArchivedBelege' (Id BigInt, 
                                                    Jahr Int, Name varchar(255), Beschreibung varchar(255), Datum datetime,
                                                    Zeit datetime, KategorieId Int, Betrag float, Primary Key(Id,Jahr));";

    public static string CreateKategorieTable = @"Create Table if not exists 'Kategorien' (Id int AUTO_INCREMENT,
                                                  Name varchar(255) not null, Beschreibung varchar(2000), Primary Key(Id));";
    public static string InsertKategorie = "Insert Into Kategorien (Id,Name,Beschreibung) Values ('@Id','@KategorieName', '@Beschreibung');";
    public static string GatherKategories = "Select * from Kategorien;";
    public static string UpdateKategorie = @"Update Kategorien set Name = '@KategorieName', Beschreibung = '@Beschreibung' where Id = '@Id';";
    public static string DeleteKategorie = @"Delete From Kategorien where Id = '@Id'";
    public static string GatherKategorieById(int id) { return $"Select * from Kategorien where Id = {id};"; }

    public static string CreatePersonTable = "Create Table if not exists 'Person' (Id int, Vorname varchar(100), Nachname varchar(100), Primary Key(Id));";
    public static string InsertPerson = "Insert into Person (Id,Vorname,Nachname) values ('@Id','@Vorname','@Nachname');";
    public static string UpdatePerson = "Update Person set Vorname = '@Vorname', Nachname = '@Nachname' Where Id = '@Id';";
    public static string DeletePerson = "Delete From Person Where Id = '@Id';";
    public static string GatherPerson = "Select * from Person;";
    public static string GatherPersonbyId(int id) { return $"Select * from Person where Id = {id};"; }

    public static string CreateEinkommenTable = @"Create Table if not exists 'Einkommen' 
                                            (Id bigint AUTO_INCREMENT, PersonId int, Jahr int, Name varchar(2000), Wert float, EinnahmeHaeufigkeit int,StartDate datetime, EndDate datetime,
                                            Primary Key(Id),
                                            Foreign Key(Jahr) References Years(Jahr),
                                            Foreign Key(PersonId) References Person(Id));";
    public static string InsertEinkommen = "Insert into Einkommen (Id, PersonId, Jahr, Name, Wert, EinnahmeHaeufigkeit, StartDate, EndDate) " +
        "Values ('@Id', '@PersonId', '@Jahr', '@Name', '@Wert', '@EinnahmeHaeufigkeit','@StartDate', '@EndDate');";
    public static string UpdateEinkommen = "Update Einkommen set PersonId = '@PersonId', Jahr = '@Jahr', Name = '@Name', Wert = '@Wert', " +
        "EinnahmeHaeufigkeit = '@EinnahmeHaeufigkeit', StartDate = '@StartDate', EndDate = '@EndDate' where Id = '@Id';";
    public static string UpdateEinkommenMethod(Model.Einkommen selectedEinkommen, string starddate, string enddate)
    {
        int einnahmeHaufigkeit = (int)selectedEinkommen.EinnahmeHaeufigkeit;
        return $@"Update Einkommen set PersonId = '{selectedEinkommen.PersonId}', Jahr = '{selectedEinkommen.Jahr}', Name = '{selectedEinkommen.Name}', Wert = '{selectedEinkommen.Wert}', 
                EinnahmeHaeufigkeit = '{einnahmeHaufigkeit}', StartDate = '{starddate}', EndDate = '{enddate}' where Id = '{selectedEinkommen.Id}';";
    }
    public static string DeleteEinkommen = "Delete From Einkommen where Id = '@Id';";
    public static string GatherEinkommenFromPerson = "Select * from Einkommen where PersonId = @PersonId;";
}
