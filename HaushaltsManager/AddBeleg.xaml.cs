using HaushaltsManager.Model;
using HaushaltsManager.Repository;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace HaushaltsManager
{
    /// <summary>
    /// Interaktionslogik für AddBeleg.xaml
    /// </summary>
    public partial class AddBeleg : Window
    {
        private readonly BasicRepository rep;
        private readonly string _jahr;
        private readonly int _highestbelegId;
        private readonly MainWindow mainWindow;
        static private string _path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

        private Beleg _toInsert;

        public Beleg ToInsert
        {
            get { return _toInsert; }
            set { _toInsert = value; }
        }

        private bool _insertEnabled;

        public bool InsertedEnabled
        {
            get { return _insertEnabled; }
            set { _insertEnabled = value; }
        }

        public AddBeleg(BasicRepository repo, string jahr, int highestbelegId, MainWindow mainWindow)
        {
            InitializeComponent();
            InsertedEnabled = false;
            ToInsert = new();
            rep = repo;
            _jahr = jahr;
            _highestbelegId = ++highestbelegId;
            this.mainWindow = mainWindow;
            KategoriePicker.ItemsSource = rep.DoQueryCommand<Kategorie>(SQLStatementProvider.GatherKategories);
        }

        private string SaveInBelegFolder(string path)
        {
            string fileName = path.Split(@"\").Last();
            string newPath = @$"{_path}\DBFiles\Belege";
            if (!Directory.Exists(newPath))
            {
                Directory.CreateDirectory(newPath);
            }
            string date = DateTime.Now.ToString().Replace("-", "_").Replace(":", "_").Replace(".","_").Replace(" ","_");
            string newPathWithFile = $@"{newPath}\{date}_{fileName}";
            if (!File.Exists(newPathWithFile))
            {
                //File.Create(newPathWithFile).Dispose();
                return newPathWithFile;
                //Messenger.Send(new SetupDatabaseMessage());
            }
            return string.Empty;
        }

        private void BelegSave_Click(object sender, RoutedEventArgs e)
        {
            double betrag = Double.Parse($"{Euro.Text}.{Cent.Text}");

            ToInsert.Id = _highestbelegId;
            ToInsert.Jahr = Convert.ToInt32(_jahr);
            ToInsert.Name = BelegName.Text;
            ToInsert.Beschreibung = BelegBeschreibung.Text;
            ToInsert.KategorieId = ((Kategorie)KategoriePicker.SelectedItem).Id;
            ToInsert.Datum = ((DateTime)Datum.SelectedDate!).ToString();
            ToInsert.Betrag = betrag;
            //ToInsert.Speicherpfad = TextImagePfad.Text;
            ToInsert.Speicherpfad = SaveInBelegFolder(TextImagePfad.Text);
            if (ToInsert.Speicherpfad != string.Empty)
            {
                string sql = SQLStatementProvider.InsertBelege(ToInsert.Id, ToInsert.Jahr, ToInsert.Name, ToInsert.Beschreibung, ToInsert.Datum, ToInsert.KategorieId, ToInsert.Betrag, ToInsert.Speicherpfad);
                string sql1 = SQLStatementProvider.InsertBeleg
                    .Replace("@Id", ToInsert.Id.ToString())
                    .Replace("@Jahr", ToInsert.Jahr.ToString())
                    .Replace("@Name", ToInsert.Name)
                    .Replace("@Beschreibung", ToInsert.Beschreibung)
                    .Replace("@Datum", ToInsert.Datum)
                    .Replace("@KategorieId", ToInsert.KategorieId.ToString())
                    .Replace("@Betrag", ToInsert.Betrag.ToString())
                    .Replace("@Speicherpfad", ToInsert.Speicherpfad!.ToString());
                InsertedEnabled = CheckIfAllDateSet();
                if (InsertedEnabled)
                {
                    rep.DoNonQueryCommand(sql1);
                    File.Copy(TextImagePfad.Text, ToInsert.Speicherpfad.ToString(), false);
                    mainWindow.LoadBeleg();
                    this.Close();
                }
            }
        }

        private void BelegCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BelegImageSave_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog();
            bool? haveFileName = openFileDialog.ShowDialog();
            string fileName = openFileDialog.FileName;
            if (haveFileName is true)
            {
                fileName = openFileDialog.FileName;
                TextImagePfad.Text = fileName;
                ToInsert.Speicherpfad = fileName;
            }
        }

        private bool CheckIfAllDateSet()
        {
            string regexDate = @"\d\d\.\d\d\.\d{4}";
            Regex reg = new Regex(regexDate);
            bool insertedEnabled = false;
            if (ToInsert is not null && ToInsert.Id != 0 && ToInsert.Name != string.Empty && reg.IsMatch(ToInsert.Datum) && ToInsert.Betrag > 0.0 && ToInsert.Jahr != 0 && ToInsert.KategorieId != 0)
            {
                insertedEnabled = true;
            }
            return insertedEnabled;
        }

        private void CheckIfAllIsInserted(object sender, RoutedEventArgs e)
        {
            InsertedEnabled = CheckIfAllDateSet();
        }
    }
}
