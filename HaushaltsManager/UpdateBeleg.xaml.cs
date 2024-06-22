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
    /// Interaktionslogik für UpdateBeleg.xaml
    /// </summary>
    public partial class UpdateBeleg : Window
    {
        private readonly BasicRepository _rep;

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

        public UpdateBeleg(Beleg beleg, BasicRepository rep)
        {
            InitializeComponent();
            _rep = rep;
            BelegName.Text = beleg.Name;
            BelegBeschreibung.Text = beleg.Beschreibung;
            Euro.Text = beleg.BetragNum.ToString().Split('.').First();
            Cent.Text = beleg.BetragNum.ToString().Split('.').Last();
            Datum.Text = beleg.Datum.ToString();
            KategoriePicker.ItemsSource = rep.DoQueryCommand<Kategorie>(SQLStatementProvider.GatherKategories);
            PersonPicker.ItemsSource = rep.DoQueryCommand<Person>(SQLStatementProvider.GatherPerson);
            KategoriePicker.SelectedItem = rep.DoQueryCommand<Kategorie>(SQLStatementProvider.GatherKategorieById(beleg.KategorieId));
            PersonPicker.SelectedItem = rep.DoQueryCommand<Person>(SQLStatementProvider.GatherPersonbyId(beleg.PersonId));
            TextImagePfad.Text = beleg.Speicherpfad;
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
                fileName = openFileDialog.FileName;
                TextImagePfad.Text = fileName;
                ToInsert.Speicherpfad = fileName;
            }
        }
    }
}
