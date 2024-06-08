using HaushaltsManager.Model;
using HaushaltsManager.Repository;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
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
        private readonly int highestbelegId;
        private readonly MainWindow mainWindow;

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
            ToInsert = null;
            rep = repo;
            _jahr = jahr;
            this.highestbelegId = highestbelegId++;
            this.mainWindow = mainWindow;
            KategoriePicker.ItemsSource = rep.DoQueryCommand<Kategorie>(SQLStatementProvider.GatherKategories);
        }

        private void BelegSave_Click(object sender, RoutedEventArgs e)
        {
            double betrag = Double.Parse($"{Euro.Text}.{Cent.Text}");
            ToInsert = new Beleg()
            {
                Id = highestbelegId,
                Jahr = Convert.ToInt32(_jahr),
                Name = BelegName.Text,
                Beschreibung = BelegBeschreibung.Text,
                KategorieId = ((Kategorie)KategoriePicker.SelectedItem).Id,
                Datum = ((DateTime)Datum.SelectedDate!).ToString(),
                Betrag = betrag,
                Speicherpfad = TextImagePfad.Text

            };
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
                mainWindow.LoadBeleg();
                this.Close();
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
            string fileName = string.Empty;
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
