namespace HaushaltsManager.Repository;

public static class SQLStatementProvider
{
    //public static string GatherTableNames = "SELECT name FROM sqlite_master WHERE type = 'table';";
    public static string CreateBelegTable = $"Create Table if not exists 'Belege' " +
                    $"(Id BigInt AUTO_INCREMENT, " +
                    $"Jahr Int, " +
                    $"Name varchar(255), " +
                    $"Beschreibung varchar(2000), " +
                    $"Datum datetime, " +
                    $"KategorieId Int, " +
                    $"Betrag float, " +
                    $"Primary Key(Id)," +
                    $"Foreign Key(Jahr) References Years(Jahr)," +
                    $"Foreign Key(KategorieId) References Kategorien(Id));";
    public static string SelectBelegeFromYear = @"Select * from Belege where Jahr = @Year";


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
}
