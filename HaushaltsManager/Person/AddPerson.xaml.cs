using HaushaltsManager.Model;
using HaushaltsManager.Repository;
using System.Windows;

namespace HaushaltsManager
{
    /// <summary>
    /// Interaktionslogik für AddPerson.xaml
    /// </summary>
    public partial class AddPerson : Window
    {
        private readonly BasicRepository rep;
        private readonly PersonOptions personOptions;
        private readonly int highestId;

        public AddPerson()
        {
            InitializeComponent();
        }
        public AddPerson(BasicRepository rep, int highestId, PersonOptions personOptions)
        {
            InitializeComponent();
            this.rep = rep;
            this.personOptions = personOptions;
            this.highestId = highestId + 1;
        }

        private void InsertPerson_Click(object sender, RoutedEventArgs e)
        {
            Person person = new Person()
            {
                Id = highestId,
                Vorname = Vorname.Text,
                Nachname = Nachname.Text
            };
            rep.DoNonQueryCommand(SQLStatementProvider.InsertPerson.Replace("@Vorname", person.Vorname).Replace("@Id", person.Id.ToString()).Replace("@Nachname", person.Nachname));
            personOptions.OnLoad();
            this.Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
