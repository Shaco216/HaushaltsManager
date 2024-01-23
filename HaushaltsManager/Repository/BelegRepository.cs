using Dapper;
using HaushaltsManager.Model;
using System.Data.SQLite;

namespace HaushaltsManager.Repository;

public class BelegRepository : IBelegRepository
{
    private string _constring;

    public BelegRepository(string constring)
    {
        _constring = constring;
    }

    private IEnumerable<T> DoQueryCommand<T>(string cmd)
    {
        using var connection = new SQLiteConnection(_constring);
        connection.Open();
        var items = connection.Query<T>(cmd);
        connection.Close();
        return items;
    }
    private int DoNonQueryCommand(string cmd)
    {
        using var connection = new SQLiteConnection(_constring);
        connection.Open();
        SQLiteCommand SQLcmdlet = connection.CreateCommand();
        SQLcmdlet.CommandText = cmd;
        int rows = SQLcmdlet.ExecuteNonQuery();
        connection.Close();
        return rows;
    }

    public void InsertBeleg(Beleg beleg)
    {
        const string sql = "Insert Into ";
    }
}
