using HaushaltsManager.Einkommen;
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
        private Person _selectedPerson;

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
            if (LocatedPersons.SelectedItem != null)
            {
                _selectedPerson = LocatedPersons.SelectedItem as Person;
                ClickedPersonToEinkommen.ItemsSource = rep.DoQueryCommand<Model.Einkommen>(SQLStatementProvider.GatherEinkommenFromPerson.Replace("@PersonId", _selectedPerson.Id.ToString()));
            }
        }

        private void InsertEinkommen_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedPerson is not null)
            {
                AddEinkommen addEinkommen = new AddEinkommen(rep, _selectedPerson);
                addEinkommen.Title = $"Einkommen von {_selectedPerson.Vorname} {_selectedPerson.Nachname}";
                addEinkommen.Show();

            }
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
