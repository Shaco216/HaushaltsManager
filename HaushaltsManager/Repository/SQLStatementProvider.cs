namespace HaushaltsManager.Repository;

public static class SQLStatementProvider
{
    //public static string GatherTableNames = "SELECT name FROM sqlite_master WHERE type = 'table';";
    public static string CreateBelegTable = $"Create Table if not exists'Belege' " +
                    $"(Id BigInt AUTO_INCREMENT, " +
                    $"Jahr Int, " +
                    $"Name varchar(255), " +
                    $"Beschreibung varchar(255), " +
                    $"Datum datetime, " +
                    $"Zeit datetime, " +
                    $"KategorieId Int, " +
                    $"Betrag float, " +
                    $"Primary Key(Id)," +
                    $"Foreign Key(Jahr) References Years(Jahr));";

    public static string CreateYearsTable = "Create Table if not exists 'Years' (Jahr int Not Null, Primary Key(Jahr));";

    public static string GatherYears = "Select * from Years;";
    public static string InsertYear = "Insert Into Years (Jahr) Values (@Year);";
    public static string UpdateYear = "Update Years set Jahr = @Year where @PrevYear;"; 

    public static string CreateArchivedBelegeTable = @"Create Table if not exists 'ArchivedBelege' (Id BigInt, 
                                                    Jahr Int, Name varchar(255), Beschreibung varchar(255), Datum datetime,
                                                    Zeit datetime, KategorieId Int, Betrag float, Primary Key(Id,Jahr));";

}
