using HaushaltsManager.FunctionalClasses;
using HaushaltsManager.Model;
using HaushaltsManager.Repository;
using Microsoft.Win32;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Media.Imaging;

namespace HaushaltsManager
{
    /// <summary>
    /// Interaktionslogik für UpdateBeleg.xaml
    /// </summary>
    public partial class UpdateBeleg : Window
    {
        private readonly BasicRepository _rep;
        private readonly string _jahr;
        private readonly int _highestbelegId;
        private readonly MainWindow _mainWindow;
        static private string _path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        private Beleg _fromDB;

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

        public UpdateBeleg(Beleg beleg, BasicRepository rep, MainWindow mainWindow)
        {
            InitializeComponent();
            ToInsert = new();
            _rep = rep;
            this._mainWindow = mainWindow;
            BelegName.Text = beleg.Name;
            BelegBeschreibung.Text = beleg.Beschreibung;
            Euro.Text = beleg.Betrag.Split('.').First();
            if (beleg.Betrag.Contains("."))
            {
                Cent.Text = beleg.Betrag.Split('.').Last();
            }
            else 
            {
                Cent.Text = string.Empty;
            }
            Datum.Text = beleg.Datum.ToString();
            KategoriePicker.ItemsSource = rep.DoQueryCommand<Kategorie>(SQLStatementProvider.GatherKategories);
            PersonPicker.ItemsSource = rep.DoQueryCommand<Person>(SQLStatementProvider.GatherPerson);

            KategoriePicker.SelectedItem = ((IEnumerable<Kategorie>)KategoriePicker.ItemsSource).FirstOrDefault(x => x.Id == beleg.KategorieId);
            PersonPicker.SelectedItem = ((IEnumerable<Person>)PersonPicker.ItemsSource).FirstOrDefault(x => x.Id == beleg.PersonId);
            BelegImageVorher.Source = ImageLoader.LoadBitmapImage(beleg.Speicherpfad);
            //BelegImageVorher.Source = new BitmapImage(new Uri(beleg.Speicherpfad));
            TextImagePfad.Text = beleg.Speicherpfad;
            _fromDB = beleg;
        }

        private bool CheckIfAllDateSet()
        {
            string regexDate = @"\d\d\.\d\d\.\d{4}";
            Regex reg = new Regex(regexDate);
            bool insertedEnabled = false;
            if (ToInsert is not null && ToInsert.Id != 0 && ToInsert.Name != string.Empty && reg.IsMatch(ToInsert.Datum) && ToInsert.BetragNum > 0.0 && ToInsert.Jahr != 0 && ToInsert.KategorieId != 0)
            {
                insertedEnabled = true;
            }
            return insertedEnabled;
        }

        private void CheckIfAllIsInserted(object sender, RoutedEventArgs e)
        {
            InsertedEnabled = CheckIfAllDateSet();
        }

        private void BelegImageSave_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog();
            bool? haveFileName = openFileDialog.ShowDialog();
            string fileName = openFileDialog.FileName;
            if (haveFileName is true)
            {
                BelegImageNeu.Source = ImageLoader.LoadBitmapImage(fileName);
                //BelegImageNeu.Source = new BitmapImage(new Uri(fileName));
                fileName = openFileDialog.FileName;
                TextImagePfad.Text = fileName;
                ToInsert.Speicherpfad = fileName;
            }
        }

        private string SaveInBelegFolder(string path)
        {
            string fileName = path.Split(@"\").Last();
            string newPath = @$"{_path}\Haushaltsmanager\Belege";
            if (!Directory.Exists(newPath))
            {
                Directory.CreateDirectory(newPath);
            }
            string date = DateTime.Now.ToString().Replace("-", "_").Replace(":", "_").Replace(".", "_").Replace(" ", "_");
            string newPathWithFile = $@"{newPath}\{date}_{fileName}";
            if (!File.Exists(newPathWithFile))
            {
                return newPathWithFile;
            }
            return string.Empty;
        }

        private void BelegCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BelegUpdate_Click(object sender, RoutedEventArgs e)
        {
            string betrag = $"{Euro.Text}.{Cent.Text}";
            BelegImageVorher.Source = null;
            int rowsChanged = _rep.DoNonQueryCommand(
                SQLStatementProvider.UpdateBelege(_fromDB.Id, _fromDB.Jahr, BelegName.Text, BelegBeschreibung.Text, Datum.Text, ((Kategorie)KategoriePicker.SelectedItem).Id, betrag, TextImagePfad.Text, ((Person)PersonPicker.SelectedItem).Id));
            if (rowsChanged == 1 && _fromDB.Speicherpfad!.Equals(TextImagePfad.Text) is false)
            {
                BelegImageNeu.Source = null;
                File.Delete(_fromDB.Speicherpfad);
                string saveDirectoryPath = SaveInBelegFolder(TextImagePfad.Text);
                File.Copy(TextImagePfad.Text, saveDirectoryPath, false);
            }
            if (rowsChanged == 0)
            {
                return;
            }
            _mainWindow.LoadBeleg();
            this.Close();
        }
    }
}
