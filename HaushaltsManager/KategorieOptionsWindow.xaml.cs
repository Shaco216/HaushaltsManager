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
    /// Interaktionslogik für KategorieOptionsWindow.xaml
    /// </summary>
    public partial class KategorieOptionsWindow : Window
    {
        BasicRepository rep;
        private readonly MainWindow _mainWindow;

        public KategorieOptionsWindow()
        {
            InitializeComponent();
        }
        public KategorieOptionsWindow(BasicRepository repo, MainWindow mainWindow)
        {
            InitializeComponent();
            rep = repo;
            _mainWindow = mainWindow;
            LoadKategories();
        }

        private void DeleteKategories_Click(object sender, RoutedEventArgs e)
        {
            Kategorie selectedKategorie = (Kategorie)LocatedKategories.SelectedItem;
            IEnumerable<Beleg> belegs = rep.DoQueryCommand<Beleg>(SQLStatementProvider.SelectBelegeFromKategorieId.Replace("@KategorieId", selectedKategorie.Id.ToString()));
            if (belegs.Count() < 0)
            {
                rep.DoNonQueryCommand(SQLStatementProvider.DeleteKategorie
                    .Replace("@Id", selectedKategorie.Id.ToString()));
                LoadKategories();
            }
            else
            {
                MessageBox.Show("Kategorie konnte nicht gelöscht werden, da noch Belege diese Kategorie verwenden.");
            }
        }

        private void UpdateKategories_Click(object sender, RoutedEventArgs e)
        {
            Kategorie selectedKategorie = (Kategorie)LocatedKategories.SelectedItem;
            rep.DoNonQueryCommand(SQLStatementProvider.UpdateKategorie
                .Replace("@Id", selectedKategorie.Id.ToString())
                .Replace("@KategorieName", selectedKategorie.Name)
                .Replace("@Beschreibung", selectedKategorie.Beschreibung));
            IEnumerable<Beleg> belegs = rep.DoQueryCommand<Beleg>(SQLStatementProvider.SelectBelegeFromKategorieId
                .Replace("@KategorieId", selectedKategorie.Id.ToString()));
            foreach (Beleg beleg in belegs)
            {
                rep.DoNonQueryCommand(SQLStatementProvider.UpdateBelegbyKategorie.Replace("@KategorieId", selectedKategorie.Id.ToString())
                    .Replace("@Id", beleg.Id.ToString()));
            }
            LoadKategories();
            _mainWindow.ClickedYear.ItemsSource = null;
        }

        private void InsertKategories_Click(object sender, RoutedEventArgs e)
        {
            int highestId = 0;
            bool lockat = LocatedKategories is not null;
            bool lockatsource = LocatedKategories.ItemsSource != null;
            bool lockatcount = ((IEnumerable<Kategorie>)LocatedKategories.ItemsSource).Count() > 1;
            if (lockat && lockatsource && lockatcount)
            {
                var kategories = ((IEnumerable<Kategorie>)LocatedKategories.ItemsSource);
                highestId = kategories.Max(x => x.Id);
            }
            AddKategorie addKategorie = new AddKategorie(rep, highestId, this);
            addKategorie.Show();
        }

        public void LoadKategories()
        {
            LocatedKategories.ItemsSource = rep.DoQueryCommand<Kategorie>(SQLStatementProvider.GatherKategories);
        }
    }
}
