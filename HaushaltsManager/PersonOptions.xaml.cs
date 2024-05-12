﻿using HaushaltsManager.Model;
using HaushaltsManager.Repository;
using System.Windows;

namespace HaushaltsManager
{
    /// <summary>
    /// Interaktionslogik für PersonOptions.xaml
    /// </summary>
    public partial class PersonOptions : Window
    {
        private readonly BasicRepository rep;

        public PersonOptions()
        {
            InitializeComponent();
        }

        public PersonOptions(BasicRepository basicRepository)
        {
            InitializeComponent();
            rep = basicRepository;
            OnLoad();
        }

        private void InsertPerson_Click(object sender, RoutedEventArgs e)
        {
            int highestId = ((IEnumerable<Person>)LocatedPerson.ItemsSource).Max(x => x.Id);
            AddPerson addPerson = new AddPerson(rep,highestId,this);
            addPerson.Show();
        }

        private void UpdatePerson_Click(object sender, RoutedEventArgs e)
        {
            if(LocatedPerson.SelectedItem != null)
            {
                Person selectedperson = LocatedPerson.SelectedItem as Person;
                rep.DoNonQueryCommand(SQLStatementProvider.UpdatePerson.Replace("@Vorname", selectedperson.Vorname).Replace("@Nachname", selectedperson.Nachname)
                    .Replace("@Id", selectedperson.Id.ToString()));
                OnLoad();
            }
        }

        private void DeletePerson_Click(object sender, RoutedEventArgs e)
        {
            if (LocatedPerson.SelectedItem != null)
            {
                Person selectedperson = LocatedPerson.SelectedItem as Person;
                rep.DoNonQueryCommand(SQLStatementProvider.DeletePerson.Replace("@Id", selectedperson.Id.ToString()));
                OnLoad();
            }
        }

        private void OpenEinkommenOptionen_Click(object sender, RoutedEventArgs e)
        {
            EinkommenOptionen einkommenOptionen = new EinkommenOptionen(rep);
            einkommenOptionen.Show();
        }

        public void OnLoad()
        {
            LocatedPerson.ItemsSource = rep.DoQueryCommand<Person>(SQLStatementProvider.GatherPerson);
        }
    }
}
