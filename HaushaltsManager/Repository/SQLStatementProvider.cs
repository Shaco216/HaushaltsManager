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
                    $"Foreign Key(Year))";

    public static string CreateYearsTable = "Create Table if not exists 'Years' (Year Int Not Null, Primary Key(Year));";

    public static string GatherYears = "Select Year from Years;";
    public static string InsertYear = "Insert Into Years (Year) Values (@year)";

    public static string CreateArchivedBelegeTable = @"Create Table if not exists 'ArchivedBelege' (Id BigInt, 
                                                    Jahr Int, Name varchar(255), Beschreibung varchar(255), Datum datetime,
                                                    Zeit datetime, KategorieId Int, Betrag float, Primary Key(Id,Jahr))";

}
