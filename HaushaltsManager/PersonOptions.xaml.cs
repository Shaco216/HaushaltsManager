using HaushaltsManager.Model;
using HaushaltsManager.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace HaushaltsManager
{
    /// <summary>
    /// Interaktionslogik für PersonOptions.xaml
    /// </summary>
    public partial class PersonOptions : Window
    {
        private readonly BasicRepository basicRepository;

        public PersonOptions()
        {
            InitializeComponent();
        }

        public PersonOptions(BasicRepository basicRepository)
        {
            InitializeComponent();
            this.basicRepository = basicRepository;
            OnLoad();
        }

        private void InsertPerson_Click(object sender, RoutedEventArgs e)
        {
            int highestId = ((IEnumerable<Person>)LocatedPerson.ItemsSource).Max(x => x.Id);
            AddPerson addPerson = new AddPerson(basicRepository,highestId,this);
            addPerson.Show();
        }

        private void UpdatePerson_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DeletePerson_Click(object sender, RoutedEventArgs e)
        {

        }

        private void OpenEinkommenOptionen_Click(object sender, RoutedEventArgs e)
        {
            EinkommenOptionen einkommenOptionen = new EinkommenOptionen(basicRepository);
            einkommenOptionen.Show();
        }

        public void OnLoad()
        {
            LocatedPerson.ItemsSource = basicRepository.DoQueryCommand<IEnumerable<Person>>(SQLStatementProvider.GatherPerson);
        }
    }
}
