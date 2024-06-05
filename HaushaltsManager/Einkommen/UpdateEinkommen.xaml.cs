using HaushaltsManager.Model;
using HaushaltsManager.Repository;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;

namespace HaushaltsManager.Einkommen
{
    /// <summary>
    /// Interaktionslogik für UpdateEinkommen.xaml
    /// </summary>
    public partial class UpdateEinkommen : Window
    {
        private readonly BasicRepository _rep;

        public Model.Einkommen Einkommen { get; set; }

        public UpdateEinkommen(BasicRepository rep, Model.Einkommen einkommen)
        {
            InitializeComponent();
            EinnahmeHaeufigkeit.ItemsSource = Enum.GetValues(typeof(EinnahmeFrequenz));
            EinnahmeHaeufigkeit.SelectedItem = einkommen.EinnahmeHaeufigkeit;
            EinkommenWert.Text = einkommen.Wert.ToString();
            EinkommenName.Text = einkommen.Name;
            EinkommenJahr.Text = einkommen.Jahr.ToString();
            StartDate.Text = einkommen.StartDate.ToShortDateString();
            EndDate.Text = einkommen.EndDate.ToShortDateString();
            _rep = rep;
            Einkommen = einkommen;
        }

        private void CancelEinkommen_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void UpdateEinkommen_Click(object sender, RoutedEventArgs e)
        {
            Model.Einkommen newEinkommen = new Model.Einkommen()
            {
                Name = EinkommenName.Text,
                Id = Einkommen.Id,
                Jahr = Convert.ToInt32(EinkommenJahr.Text),
                EinnahmeHaeufigkeit = (EinnahmeFrequenz)Enum.Parse(typeof(EinnahmeFrequenz), EinnahmeHaeufigkeit.Text),
                Wert = Convert.ToDouble(EinkommenWert.Text),
                PersonId = Einkommen.PersonId
            };
            _rep.DoNonQueryCommand(SQLStatementProvider.UpdateEinkommenMethod(newEinkommen, GetDateTimeString(StartDate.Text), GetDateTimeString(EndDate.Text)));
            this.Close();
        }

        private string GetDateTimeString(string date)
        {
            DateTime dt = DateTime.MinValue;
            if (date.Contains("."))
            {
                dt = DateTime.ParseExact(date, "dd.MM.yyyy", CultureInfo.InvariantCulture);
            }
            else if (date.Contains("-"))
            {
                dt = DateTime.ParseExact(date, "dd-MM-yyyy", CultureInfo.InvariantCulture);
            }
            else if (date.Contains(":"))
            {
                dt = DateTime.ParseExact(date, "dd:MM:yyyy", CultureInfo.InvariantCulture);
            }
            else if (date.Contains("/"))
            {
                dt = DateTime.ParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }
            return dt.ToString("yyyy-MM-dd");
        }

        private void UpdateEinnahmeEinmaligStartDateChanged(object sender, RoutedEventArgs e)
        {
            if (StartDate.Text != string.Empty && DateTime.TryParse(StartDate.Text, out DateTime _))
            {
                if ((EinnahmeFrequenz)Enum.Parse(typeof(EinnahmeFrequenz), EinnahmeHaeufigkeit.Text) == EinnahmeFrequenz.Einmalig && StartDate.Text is not null || StartDate.Text == string.Empty)
                {
                    EndDate.Text = StartDate.Text;
                }
            }
        }
    }
}
