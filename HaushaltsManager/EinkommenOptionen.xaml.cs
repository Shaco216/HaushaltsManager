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
        private readonly IEnumerable<Person> people;

        public EinkommenOptionen()
        {
            InitializeComponent();
        }

        public EinkommenOptionen(BasicRepository rep, IEnumerable<Person> people)
        {
            InitializeComponent();
            this.rep = rep;
            this.people = people;
            LocatedPersons.ItemsSource = people;
        }

        public void LoadEinkommenFromPerson() 
        {
            if(LocatedPersons.SelectedItem != null)
            {
                Person selectedPerson = LocatedPersons.SelectedItem as Person;
                ClickedPersonToEinkommen.ItemsSource = rep.DoQueryCommand<Einkommen>(SQLStatementProvider.GatherEinkommenFromPerson.Replace("@PersonId", selectedPerson.Id.ToString()));
            }
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
