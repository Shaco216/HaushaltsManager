using HaushaltsManager.Model;
using HaushaltsManager.Repository;
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

        public AddEinkommen(BasicRepository rep, Person person)
        {
            InitializeComponent();
            EinnahmeHaeufigkeit.ItemsSource = Enum.GetValues(typeof(EinnahmeFrequenz));
            EinnahmeHaeufigkeit.SelectedItem = EinnahmeFrequenz.None;
            this.rep = rep;
            this.person = person;
        }

        private void InsertEinkommen_Click(object sender, RoutedEventArgs e)
        {
            bool IsJahrNotLeer = EinkommenJahr.Text != null || EinkommenJahr.Text != string.Empty;
            bool IsNameNotLeer = EinkommenName.Text != null || EinkommenName.Text != string.Empty;
            bool IsWertNotLeer = EinkommenWert.Text != null || EinkommenName.Text != string.Empty;
            bool IsEinnahmeHaufigkeitNotNone = EinnahmeHaeufigkeit.SelectedItem is not null && ((EinnahmeFrequenz) Enum.Parse(typeof(EinnahmeFrequenz), EinnahmeHaeufigkeit.Text)) != EinnahmeFrequenz.None;

            if (IsJahrNotLeer && IsNameNotLeer && IsWertNotLeer && IsEinnahmeHaufigkeitNotNone)
            {
                //'@PersonId', '@Jahr', '@Name', '@Wert', '@EinnahmeHaeufigkeit','@StartDate', '@EndDate'
                string insertsql = SQLStatementProvider.InsertEinkommen.Replace("@PersonId", person.Id.ToString()
                    .Replace("@Jahr", EinkommenJahr.Text).Replace("@Name", EinkommenName.Text).Replace("@Wert", EinkommenWert.Text).Replace("@EinnahmeHaeufigkeit", EinnahmeHaeufigkeit.Text)
                    .Replace("@StartDate", StartDate.Text).Replace("@EndDate", EndDate.Text));
                rep.DoNonQueryCommand(insertsql);
            }
        }

        private void EinnahmeEinmaligStartDateChanged(object sender, RoutedEventArgs e)
        {
            if (StartDate.Text != string.Empty && DateTime.TryParse(StartDate.Text,out DateTime _))
            {
                if ((EinnahmeFrequenz)Enum.Parse(typeof(EinnahmeFrequenz), EinnahmeHaeufigkeit.Text) == EinnahmeFrequenz.Einmalig && StartDate.Text is not null || StartDate.Text == string.Empty)
                {
                    EndDate.Text = StartDate.Text;
                }
            }
        }

        private void CancelEinkommen_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
