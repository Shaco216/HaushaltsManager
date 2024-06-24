using System.Windows;
using System.Windows.Controls;
using HaushaltsManager.Repository;
using HaushaltsManager.DBCreator;
using HaushaltsManager.Model;
using System.Drawing;
using System.Windows.Media.Imaging;

namespace HaushaltsManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DBCreator.DBCreator creator;
        const string filename = @"\Years";
        const string lastFilename = @"\Haushaltsmanager";
        const string filetype = "db";
        BasicRepository rep;
        IEnumerable<Beleg> _belege;
        public MainWindow()
        {
            InitializeComponent();
            creator = DBCreator.DBCreator.GetInstance(filename, lastFilename, string.Empty, filetype);
            creator.Constring = ConstringAllocator.Years;
            creator.CreateDBFile();
            rep = new(creator.Constring);
            creator.CreateTable(SQLStatementProvider.CreateYearsTable);
            creator.CreateTable(SQLStatementProvider.CreateBelegTable);
            creator.CreateTable(SQLStatementProvider.CreateArchivedBelegeTable);
            creator.CreateTable(SQLStatementProvider.CreateKategorieTable);
            creator.CreateTable(SQLStatementProvider.CreatePersonTable);
            creator.CreateTable(SQLStatementProvider.CreateEinkommenTable);
            UpdateItemSource();
        }

        private void CreateYear_Click(object sender, RoutedEventArgs e)
        {
            AddYear addYear = new AddYear(rep);
            addYear.Title = "Neues Jahr hinzufügen";
            addYear.Width = 300;
            addYear.Height = 200;
            addYear.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            addYear.ShowDialog();
            UpdateItemSource();
        }

        private void EditYear_Click(object sender, RoutedEventArgs e)
        {
            if (LocatedYears.SelectedItem is not null)
            {
                Year selectedYear = (Year)LocatedYears.SelectedItem;
                UpdateYear updateYear = new UpdateYear(rep, selectedYear.Jahr.ToString());
                updateYear.Title = $"Update {LocatedYears.SelectedItem}";
                updateYear.Width = 300;
                updateYear.Height = 200;
                updateYear.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                updateYear.ShowDialog();
            }
            UpdateItemSource();

        }

        private void DeleteYear_Click(object sender, RoutedEventArgs e)
        {
            if (LocatedYears.SelectedItem is not null)
            {
                Year selectedYear = (Year)LocatedYears.SelectedItem;
                rep.DoNonQueryCommand(SQLStatementProvider.DeleteYear.Replace("@Year", selectedYear.Jahr.ToString()));
                string belegsql = SQLStatementProvider.SelectBelegeFromYear
                    .Replace("@Year", selectedYear.Jahr.ToString());
                IEnumerable<Beleg> belege = rep.DoQueryCommand<Beleg>(belegsql);
                foreach (Beleg beleg in belege)
                {
                    string updatesql = SQLStatementProvider.DeleteBelegFromYear
                        .Replace("@Year", selectedYear.Jahr.ToString());
                    rep.DoNonQueryCommand(updatesql);
                }
                UpdateItemSource();
            }
        }
        private void UpdateItemSource()
        {
            var years = rep.DoQueryCommand<Year>(SQLStatementProvider.GatherYears);
            LocatedYears.ItemsSource = years;
            ClickedYear.ItemsSource = null;
        }

        private void CreateBeleg_Click(object sender, RoutedEventArgs e)
        {
            int highestbelegId = 0;
            if (_belege is not null && _belege.Count() > 0)
            {
                highestbelegId = _belege.Max(x => x.Id);
            }
            if (LocatedYears.SelectedItem is not null)
            {
                Year selectedYear = LocatedYears.SelectedItem as Year;
                string jahr = selectedYear.Jahr.ToString();
                AddBeleg addBeleg = new AddBeleg(rep, jahr, highestbelegId, this);
                addBeleg.Title = $"Beleg im Jahr {selectedYear.Jahr} hinzufügen";
                addBeleg.Width = 500;
                addBeleg.Height = 300;
                addBeleg.ShowDialog();
            }

        }

        private void UpdateBeleg_Click(object sender, RoutedEventArgs e)
        {
            if (LocatedYears.SelectedItem is not null && _belege.Count() > 0 && ClickedYear.SelectedItem is not null)
            {
                Year selectedYear = LocatedYears.SelectedItem as Year;
                Beleg selectedBeleg = ClickedYear.SelectedItem as Beleg;
                //string sqlcmd = SQLStatementProvider.UpdateBeleg.Replace("@Jahr", selectedYear.Jahr.ToString()).Replace("@Name", selectedBeleg.Name).Replace("@Beschreibung", selectedBeleg.Beschreibung)
                //    .Replace("@Datum", selectedBeleg.Datum.ToString()).Replace("@Betrag", selectedBeleg.Betrag.ToString()).Replace("@Id", selectedBeleg.Id.ToString())
                //    .Replace("@KategorieId", selectedBeleg.KategorieId.ToString());
                //rep.DoNonQueryCommand(sqlcmd);
                if (selectedBeleg is not null)
                {
                    UpdateBeleg updateBeleg = new(selectedBeleg, rep);
                    updateBeleg.Show();
                }
                LoadBeleg();
            }
        }

        private void DeleteBeleg_Click(object sender, RoutedEventArgs e)
        {
            if (LocatedYears.SelectedItem is not null
                && _belege.Count() > 0
                && ClickedYear.SelectedItem is not null)
            {
                Beleg selectedBeleg = ClickedYear.SelectedItem as Beleg;
                rep.DoNonQueryCommand(SQLStatementProvider.DeleteBeleg.Replace("@Id", selectedBeleg.Id.ToString()));
                LoadBeleg();
            }
        }

        private void _KategorieOptions_Click(object sender, RoutedEventArgs e)
        {
            KategorieOptionsWindow kategorieWindow = new(rep,this);
            kategorieWindow.ShowDialog();
        }

        public void LoadBeleg()
        {
            if (LocatedYears.SelectedItem is not null)
            {
                Year selectedYear = LocatedYears.SelectedItem as Year;
                string sql = SQLStatementProvider.SelectBelegeFromYear
                .Replace("@Year", selectedYear.Jahr.ToString());
                string pdk = $"Select Betrag from Belege where Jahr = 2002;";
                var t = rep.DoQueryCommand<float>(pdk);
                IEnumerable<Beleg> belege = rep.DoQueryCommand<Beleg>(sql);
                foreach(Beleg beleg in belege)
                {
                    Kategorie k = rep.DoQueryCommand<Kategorie>(SQLStatementProvider.GatherKategorieById(beleg.KategorieId)).First();
                    beleg.Kategorie = k.Name;

                    Person p = rep.DoQueryCommand<Person>(SQLStatementProvider.GatherPersonbyId(beleg.PersonId)).First();
                    beleg.Person = p.FullName;
                }
                ClickedYear.ItemsSource = belege;
                _belege = belege;
            }
        }

        private void LocatedYears_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LoadBeleg();
        }

        private void _PersonOptions_Click(object sender, RoutedEventArgs e)
        {
            PersonOptions personOptions = new PersonOptions(rep);
            personOptions.Show();
        }

        private void ClickedYear_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ClickedYear is not null && ClickedYear.SelectedItem is not null)
            {

                Beleg b = (ClickedYear.SelectedItem as Beleg)!;
                if (b.Speicherpfad is not null)
                {
                    BelegImage.Source = new BitmapImage(new Uri(b.Speicherpfad));
                }
            }
        }
    }
}