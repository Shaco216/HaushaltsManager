﻿using System.Windows;
using System.Windows.Controls;
using HaushaltsManager.Repository;
using HaushaltsManager.DBCreator;
using HaushaltsManager.Model;

namespace HaushaltsManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DBCreator.DBCreator creator;
        const string filename = @"\Years";
        const string lastFilename = @"\DBFiles";
        const string filetype = "db";
        BasicRepository rep;
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
                UpdateItemSource();
            }
        }
        private void UpdateItemSource()
        {
            var years = rep.DoQueryCommand<Year>(SQLStatementProvider.GatherYears);
            LocatedYears.ItemsSource = years;
        }

        private void CreateBeleg_Click(object sender, RoutedEventArgs e)
        {
            var locbel = (IEnumerable<Beleg>)ClickedYear.ItemsSource;
            int highestbelegId = locbel.Max(x => x.Id);
            if (LocatedYears.SelectedItem is not null)
            {
                Year selectedYear = LocatedYears.SelectedItem as Year;
                AddBeleg addBeleg = new AddBeleg(rep, selectedYear.Jahr.ToString(),highestbelegId,this);
                addBeleg.Title = $"Beleg im Jahr {selectedYear.Jahr} hinzufügen";
                addBeleg.Width = 500;
                addBeleg.Height = 300;
                addBeleg.ShowDialog();
            }

        }

        private void UpdateBeleg_Click(object sender, RoutedEventArgs e)
        {
            if(LocatedYears.SelectedItem is not null && ClickedYear.SelectedItem is not null)
            {
                Year selectedYear = LocatedYears.SelectedItem as Year;
                Beleg selectedBeleg = ClickedYear.SelectedItem as Beleg;
                rep.DoNonQueryCommand(SQLStatementProvider.UpdateBeleg.Replace("@Jahr", selectedYear.Jahr.ToString()).Replace("@Name", selectedBeleg.Name).Replace("@Beschreibung", selectedBeleg.Beschreibung)
                    .Replace("@Datum", selectedBeleg.Datum.ToString()).Replace("@Betrag", selectedBeleg.Betrag.ToString()).Replace("@Id",selectedBeleg.Id.ToString()));
                LoadBeleg();
            }
        }

        private void DeleteBeleg_Click(object sender, RoutedEventArgs e)
        {
            if(LocatedYears.SelectedItem is not null && ClickedYear.SelectedItem is not null)
            {
                Beleg selectedBeleg = ClickedYear.SelectedItem as Beleg;
                rep.DoNonQueryCommand(SQLStatementProvider.DeleteBeleg.Replace("@Id", selectedBeleg.Id.ToString()));
                LoadBeleg();
            }
        }

        private void _KategorieOptions_Click(object sender, RoutedEventArgs e)
        {
            KategorieOptionsWindow kategorieWindow = new(rep);
            kategorieWindow.ShowDialog();
        }

        public void LoadBeleg()
        {
            Year selectedYear = LocatedYears.SelectedItem as Year;
            string sql = SQLStatementProvider.SelectBelegeFromYear
            .Replace("@Year", selectedYear.Jahr.ToString());
            IEnumerable<Beleg> belege = rep.DoQueryCommand<Beleg>(sql);
            ClickedYear.ItemsSource = belege;
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
    }
}