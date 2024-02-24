using HaushaltsManager.Model;
using HaushaltsManager.Repository;
using System.Windows;

namespace HaushaltsManager
{
    /// <summary>
    /// Interaktionslogik für EinkommenOptionen.xaml
    /// </summary>
    public partial class EinkommenOptionen : Window
    {
        private readonly BasicRepository rep;

        public EinkommenOptionen()
        {
            InitializeComponent();
        }

        public EinkommenOptionen(BasicRepository rep)
        {
            this.rep = rep;
            LoadPersons();
        }

        public void LoadEinkommenFromPerson() 
        {
            if(LocatedPerson.SelectedItem != null)
            {
                Person selectedPerson = LocatedPerson.SelectedItem as Person;
                ClickedPersonToEinkommen.ItemsSource = rep.DoQueryCommand<IEnumerable<Einkommen>>(SQLStatementProvider.GatherEinkommenFromPerson.Replace("@PersonId", selectedPerson.Id.ToString()));
            }
        }

        public void LoadPersons()
        {
            LocatedPerson.ItemsSource = rep.DoQueryCommand<IEnumerable<Person>>(SQLStatementProvider.GatherPerson);
        }

        private void InsertEinkommen_Click(object sender, RoutedEventArgs e)
        {

        }

        private void UpdateEinkommen_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DeleteEinkommen_Click(object sender, RoutedEventArgs e)
        {

        }

        private void LocatedPerson_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            LoadEinkommenFromPerson();
        }
    }
}
