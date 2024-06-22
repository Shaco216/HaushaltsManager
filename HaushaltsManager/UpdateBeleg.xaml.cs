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
    /// Interaktionslogik für UpdateBeleg.xaml
    /// </summary>
    public partial class UpdateBeleg : Window
    {
        private readonly BasicRepository _rep;

        public UpdateBeleg(Beleg beleg, BasicRepository rep)
        {
            InitializeComponent();
            _rep = rep;
            BelegName.Text = beleg.Name;
            BelegBeschreibung.Text = beleg.Beschreibung;
            Euro.Text = beleg.BetragNum.ToString().Split('.').First();
            Cent.Text = beleg.BetragNum.ToString().Split('.').Last();
            Datum.Text = beleg.Datum.ToString();
            KategoriePicker.ItemsSource = rep.DoQueryCommand<Kategorie>(SQLStatementProvider.GatherKategories);
            PersonPicker.ItemsSource = rep.DoQueryCommand<Person>(SQLStatementProvider.GatherPerson);
            KategoriePicker.SelectedItem = rep.DoQueryCommand<Kategorie>(SQLStatementProvider.GatherKategorieById(beleg.KategorieId));
            PersonPicker.SelectedItem = rep.DoQueryCommand<Person>(SQLStatementProvider.GatherPersonbyId(beleg.PersonId));
            TextImagePfad.Text = beleg.Speicherpfad;
        }
    }
}
