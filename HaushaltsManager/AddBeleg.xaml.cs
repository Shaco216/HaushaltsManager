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

        public AddBeleg()
        {
            InitializeComponent();
        }
        public AddBeleg(BasicRepository repo, string jahr)
        {
            InitializeComponent();
            rep = repo;
            _jahr = jahr;
            KategoriePicker.ItemsSource = rep.DoQueryCommand<IEnumerable<Kategorie>>(SQLStatementProvider.GatherKategories);
        }

        private void BelegSave_Click(object sender, RoutedEventArgs e)
        {
            Beleg toSave = new Beleg()
            {
                Jahr = Convert.ToInt32(_jahr),
                Name = BelegName.Text,
                Beschreibung = BelegBeschreibung.Text,
                KategorieId = ((Kategorie)KategoriePicker.SelectedItem).Id,
                Datum = (DateTime)Datum.SelectedDate,
                Betrag = Convert.ToDouble($"{Euro},{Cent}")
            };
            rep.DoNonQueryCommand(SQLStatementProvider.InsertBeleg);
        }

        private void BelegCancel_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
