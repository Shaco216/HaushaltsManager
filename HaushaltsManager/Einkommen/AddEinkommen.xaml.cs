using HaushaltsManager.Model;
using HaushaltsManager.Repository;
using System.Globalization;
using System.Windows;

namespace HaushaltsManager.Einkommen
{
    /// <summary>
    /// Interaktionslogik für AddEinkommen.xaml
    /// </summary>
    public partial class AddEinkommen : Window
    {
        private readonly BasicRepository rep;
        private readonly Person person;
        private readonly long _maxId;

        public AddEinkommen(BasicRepository rep, Person person, long maxId)
        {
            InitializeComponent();
            EinnahmeHaeufigkeit.ItemsSource = Enum.GetValues(typeof(EinnahmeFrequenz));
            EinnahmeHaeufigkeit.SelectedItem = EinnahmeFrequenz.None;
            this.rep = rep;
            this.person = person;
            _maxId = maxId +1;
        }

        private void InsertEinkommen_Click(object sender, RoutedEventArgs e)
        {
            bool IsJahrNotLeer = EinkommenJahr.Text != null || EinkommenJahr.Text != string.Empty;
            bool IsNameNotLeer = EinkommenName.Text != null || EinkommenName.Text != string.Empty;
            bool IsWertNotLeer = EinkommenWert.Text != null || EinkommenName.Text != string.Empty;
            bool IsEinnahmeHaufigkeitNotNone = EinnahmeHaeufigkeit.SelectedItem is not null && ((EinnahmeFrequenz)Enum.Parse(typeof(EinnahmeFrequenz), EinnahmeHaeufigkeit.Text)) != EinnahmeFrequenz.None;

            if (IsJahrNotLeer && IsNameNotLeer && IsWertNotLeer && IsEinnahmeHaufigkeitNotNone)
            {
                string? endDatedt = null;
                string? startDatedt = null;
                if (EndDate.Text != string.Empty)
                {
                    endDatedt = GetDateTimeString(EndDate.Text);
                }
                if (StartDate.Text != string.Empty) 
                {
                    startDatedt = GetDateTimeString(StartDate.Text);
                }
                int einnahmeArt = (int)(EinnahmeFrequenz)Enum.Parse(typeof(EinnahmeFrequenz), EinnahmeHaeufigkeit.Text);
                string insertsql = SQLStatementProvider.InsertEinkommen.Replace("@PersonId", person.Id.ToString())
                    .Replace("@Jahr", EinkommenJahr.Text).Replace("@Name", EinkommenName.Text).Replace("@Wert", EinkommenWert.Text).Replace("@EinnahmeHaeufigkeit", einnahmeArt.ToString())
                    .Replace("@StartDate", startDatedt).Replace("@EndDate", endDatedt).Replace("@Id",_maxId.ToString());
                rep.DoNonQueryCommand(insertsql);
                this.Close();
            }
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

        private void EinnahmeEinmaligStartDateChanged(object sender, RoutedEventArgs e)
        {
            if (StartDate.Text != string.Empty && DateTime.TryParse(StartDate.Text, out DateTime _))
            {
                if ((EinnahmeFrequenz)Enum.Parse(typeof(EinnahmeFrequenz), EinnahmeHaeufigkeit.Text) == EinnahmeFrequenz.Einmalig && StartDate.Text is not null || StartDate.Text == string.Empty)
                {
                    EndDate.Text = StartDate.Text;
                }
            }
        }

        private void CancelEinkommen_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
