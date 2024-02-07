using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Windows.Controls.Primitives;
using System.Xml.Linq;
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
            if (LocatedYears.SelectedItem is not null)
            {
                Year selectedYear = LocatedYears.SelectedItem as Year;
                AddBeleg addBeleg = new AddBeleg(rep, selectedYear.Jahr.ToString());
                addBeleg.Title = "Beleg hinzufügen";
                addBeleg.Width = 300;
                addBeleg.Height = 200;
                addBeleg.ShowDialog();
            }

        }

        private void UpdateBeleg_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DeleteBeleg_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}