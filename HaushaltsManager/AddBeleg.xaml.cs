using HaushaltsManager.Model;
using HaushaltsManager.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public AddBeleg()
        {
            InitializeComponent();
        }
        public AddBeleg(BasicRepository repo, string jahr, int highestbelegId, MainWindow mainWindow)
        {
            InitializeComponent();
            rep = repo;
            _jahr = jahr;
            this.highestbelegId = highestbelegId;
            this.mainWindow = mainWindow;
            KategoriePicker.ItemsSource = rep.DoQueryCommand<Kategorie>(SQLStatementProvider.GatherKategories);
        }

        private void BelegSave_Click(object sender, RoutedEventArgs e)
        {
            double betrag = Double.Parse($"{Euro.Text}.{Cent.Text}");
            Beleg toSave = new Beleg()
            {
                Id = highestbelegId,
                Jahr = Convert.ToInt32(_jahr),
                Name = BelegName.Text,
                Beschreibung = BelegBeschreibung.Text,
                KategorieId = ((Kategorie)KategoriePicker.SelectedItem).Id,
                Datum = ((DateTime)Datum.SelectedDate).ToString(),
                Betrag = betrag
            };
            string sql = SQLStatementProvider.InsertBeleg
                .Replace("@Id", toSave.Id.ToString())
                .Replace("@Jahr", toSave.Jahr.ToString())
                .Replace("@Name", toSave.Name)
                .Replace("@Beschreibung", toSave.Beschreibung)
                .Replace("@Datum", toSave.Datum)
                .Replace("@KategorieId", toSave.KategorieId.ToString())
                .Replace("@Betrag", toSave.Betrag.ToString());
            rep.DoNonQueryCommand(sql);
            mainWindow.LoadBeleg();
            this.Close();
        }

        private void BelegCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
