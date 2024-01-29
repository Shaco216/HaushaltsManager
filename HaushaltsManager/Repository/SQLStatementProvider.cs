namespace HaushaltsManager.Repository;

public static class SQLStatementProvider
{
    public static string GatherTableNames = "SELECT name FROM sqlite_master WHERE type = 'table';";
}
