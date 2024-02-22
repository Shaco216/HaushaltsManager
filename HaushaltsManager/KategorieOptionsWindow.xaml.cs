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
        public KategorieOptionsWindow()
        {
            InitializeComponent();
        }
        public KategorieOptionsWindow(BasicRepository repo)
        {
            InitializeComponent();
            rep = repo;
            LoadKategories();
        }

        private void DeleteKategories_Click(object sender, RoutedEventArgs e)
        {
            Kategorie selectedKategorie = (Kategorie)LocatedKategories.SelectedItem;

            rep.DoNonQueryCommand(SQLStatementProvider.DeleteKategorie
                .Replace("@Id", selectedKategorie.Id.ToString()));
            LoadKategories();
        }

        private void UpdateKategories_Click(object sender, RoutedEventArgs e)
        {
            Kategorie selectedKategorie = (Kategorie)LocatedKategories.SelectedItem;
            rep.DoNonQueryCommand(SQLStatementProvider.UpdateKategorie
                .Replace("@Id", selectedKategorie.Id.ToString())
                .Replace("@KategorieName", selectedKategorie.Name)
                .Replace("@Beschreibung", selectedKategorie.Beschreibung));
            LoadKategories();
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
