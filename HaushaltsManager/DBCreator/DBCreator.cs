using Dapper;
using HaushaltsManager.Repository;
using System.Data.SQLite;
using System.Globalization;
using System.Windows;

namespace HaushaltsManager.DBCreator
{
    public class DBCreator
    {
        private string _yearSQL = SQLStatementProvider.CreateYearsTable;
        //private string _yearSQL = $"Create Table 'nnnn' " +
        //            $"(Id Int AUTO_INCREMENT, " +
        //            $"Name varchar(255), " +
        //            $"Beschreibung varchar(255), " +
        //            $"Datum datetime, " +
        //            $"Zeit datetime, " +
        //            $"KategorieId Int, " +
        //            $"Betrag float, " +
        //            $"Primary Key(Id))";

        static private string _path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        static private string _filename = string.Empty;
        static private string _lastFoldername = string.Empty;
        static bool localInstance = false;
        private string? _constring;
        static private DBCreator dbCreator;
        public string YearSQL { get { return _yearSQL; } set { _yearSQL = value; } }
        private DBCreator(string Filename, string Lastfoldername, string Path, string Dateiendung)
        {
            _filename = Filename + "." + Dateiendung;
            _lastFoldername = Lastfoldername;
            if (Path != null && Path != string.Empty)
            {
                _path = Path;
            }
        }

        static public DBCreator GetInstance(string Filename, string Lastfoldername, string Path, string Dateiendung)
        {
            if (dbCreator == null)
            {
                dbCreator = new DBCreator(Filename, Lastfoldername, Path, Dateiendung);
            }
            return dbCreator;
        }
        public void CreateDBFile()
        {
            if (!localInstance)
            {

                _path = @$"{_path}\{_lastFoldername}";
                if (!System.IO.Directory.Exists(_path))
                {
                    System.IO.Directory.CreateDirectory(_path);
                }
                _path = $@"{_path}\{_filename}";
                if (!System.IO.File.Exists(_path))
                {
                    System.IO.File.Create(_path).Dispose();
                    //Messenger.Send(new SetupDatabaseMessage());
                }
            }
            else
            {
                _path = @".\";
                if (!System.IO.File.Exists(_path + _filename))
                {
                    System.IO.File.Create(_path + _filename).Dispose();
                    //Messenger.Send(new SetupDatabaseMessage());
                }
            }
        }
        public string? Constring
        {
            get { return _constring; }
            set { _constring = value; }
        }

        private IEnumerable<T> DoQueryCommand<T>(string cmd)
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
        private int DoNonQueryCommand(string cmd)
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

        public void CreateTable(string sqlcmd)
        {
            DoNonQueryCommand(sqlcmd);
        }

        public string GetFullPath()
        {
            if (localInstance)
            {
                return System.IO.Path.Combine(@".\" + _filename);
            }
            return System.IO.Path.Combine(_path, _lastFoldername, _filename);
        }
    }
}
