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
                rep.DoNonQueryCommand(SQLStatementProvider.InsertEinkommen)
            }
        }

        private void EinnahmeEinmaligStartDateChanged(object sender, RoutedEventArgs e)
        {
            if((EinnahmeFrequenz)Enum.Parse(typeof(EinnahmeFrequenz), EinnahmeHaeufigkeit.Text) == EinnahmeFrequenz.Einmalig && StartDate.Text is not null || StartDate.Text == string.Empty)
            {
                EndDate.Text = StartDate.Text;
            }
        }
    }
}
