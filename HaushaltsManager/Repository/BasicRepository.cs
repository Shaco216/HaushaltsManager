using Dapper;
using System.Data.SQLite;
using System.Windows;

namespace HaushaltsManager.Repository;

public class BasicRepository
{

    SQLiteConnection _constring;
    public BasicRepository(string conString)
    {
        _constring = new SQLiteConnection(conString);
    }
    public IEnumerable<T> DoQueryCommand<T>(string cmd)
    {
        try
        {
            using var connection = new SQLiteConnection(_constring);
            connection.Open();
            var items = connection.Query<T>(cmd);
            connection.Close();
            return items;
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
            return new List<T>();
        }
    }
    public int DoNonQueryCommand(string cmd)
    {
        try
        {
            using var connection = new SQLiteConnection(_constring);
            connection.Open();
            SQLiteCommand SQLcmdlet = connection.CreateCommand();
            SQLcmdlet.CommandText = cmd;
            int rows = SQLcmdlet.ExecuteNonQuery();
            connection.Close();
            return rows;
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
            return default;
        }
    }
}
