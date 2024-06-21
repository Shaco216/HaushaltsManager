namespace HaushaltsManager.DBCreator;

public static class ConstringAllocator
{
    private static string _years = @$"Data Source = {Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}\Haushaltsmanager\Years.db".Replace(@"\",@"/");
    public static string Years { get { return _years; } }
}
