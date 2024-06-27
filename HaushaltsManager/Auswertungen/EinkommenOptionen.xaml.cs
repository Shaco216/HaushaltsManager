using HaushaltsManager.Model;
using HaushaltsManager.Repository;
using System.Windows;

namespace HaushaltsManager.Auswertungen
{
    /// <summary>
    /// Interaktionslogik für EinkommenOptionen.xaml
    /// </summary>
    public partial class EinkommenOptionen : Window
    {
        private readonly BasicRepository _rep;
        private List<Year> _years;
        private List<Person> _persons;
        private List<Kategorie> _kategories;
        private DateTime _datumVon;
        private DateTime _datumBis;
        public EinkommenOptionen(BasicRepository rep)
        {
            InitializeComponent();
            Years = new List<Year>();
            AddToLists();
            JahrAuswahl.ItemsSource = rep.DoQueryCommand<Year>(SQLStatementProvider.GatherYears).ToList().Add();
            JahrAuswahl.ItemsSource.

_rep = rep;
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
            Years.Add(new Year() { Jahr = 0});
            Years = _rep.DoQueryCommand<Year>(SQLStatementProvider.GatherYears).ToList();
        }
    }
}
