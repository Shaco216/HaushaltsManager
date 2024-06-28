using HaushaltsManager.Model;
using HaushaltsManager.Repository;
using System.Windows;

namespace HaushaltsManager.Auswertungen
{
    /// <summary>
    /// Interaktionslogik für EinkommenOptionen.xaml
    /// </summary>
    public partial class AuswertungOptionen : Window
    {
        private readonly BasicRepository _rep;
        private List<Year> _years;
        private List<Person> _persons;
        private List<Kategorie> _kategories;
        private DateTime _datumVon;
        private DateTime _datumBis;
        public AuswertungOptionen(BasicRepository rep)
        {
            InitializeComponent();
            Years = new List<Year>();
            Persons = new List<Person>();
            Kategories = new List<Kategorie>();
            _rep = rep;
            AddToLists();
            JahrAuswahl.ItemsSource = Years;
            PersonAuswahl.ItemsSource = Persons;
            KategorieAuswahl.ItemsSource = Kategories;
            SelectFirstItems();
        }

        private void SelectFirstItems()
        {
            JahrAuswahl.SelectedItem = Years[0];
            PersonAuswahl.SelectedItem = Persons[0];
            KategorieAuswahl.SelectedItem = Kategories[0];
        }

        public List<Year> Years
        {
            get { return _years; }
            set { _years = value; }
        }

        public List<Person> Persons
        {
            get { return _persons; }
            set { _persons = value; }
        }

        public List<Kategorie> Kategories
        {
            get { return _kategories; }
            set { _kategories = value; }
        }

        public DateTime DatumVon
        {
            get { return _datumVon; }
            set { _datumVon = value; }
        }

        public DateTime DatumBis
        {
            get { return _datumBis; }
            set { _datumBis = value; }
        }

        private void AddToLists()
        {
            Years.Add(new Year() { Jahr = 0 }); //none
            Years.AddRange(_rep.DoQueryCommand<Year>(SQLStatementProvider.GatherYears));
            Years.Add(new Year { Jahr = 99999 }); //all
            Persons.Add(new Person() { Vorname = "none" });
            Persons.AddRange(_rep.DoQueryCommand<Person>(SQLStatementProvider.GatherPerson));
            Persons.Add(new Person() { Vorname = "all" });
            Kategories.Add(new Kategorie() { Name = "none" });
            Kategories.AddRange(_rep.DoQueryCommand<Kategorie>(SQLStatementProvider.GatherKategories));
            Kategories.Add(new Kategorie() { Name = "all" });
        }
    }
}
