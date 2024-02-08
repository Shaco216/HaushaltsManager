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
            LocatedKategories.ItemsSource = rep.DoQueryCommand<Kategorie>(SQLStatementProvider.GatherKategories);
        }

        private void DeleteKategories_Click(object sender, RoutedEventArgs e)
        {
            Kategorie selectedKategorie = (Kategorie)LocatedKategories.SelectedItem;

            rep.DoNonQueryCommand(SQLStatementProvider.InsertKategorie
                .Replace("@Beschreibung",selectedKategorie.Beschreibung)
                .Replace("@KategorieName",selectedKategorie.Name));
        }

        private void UpdateKategories_Click(object sender, RoutedEventArgs e)
        {
            Kategorie selectedKategorie = (Kategorie)LocatedKategories.SelectedItem;
            rep.DoNonQueryCommand(SQLStatementProvider.UpdateKategorie.Replace("@"))
        }

        private void InsertKategories_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
