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
    /// Interaktionslogik für KategorieOptionsWindow.xaml
    /// </summary>
    public partial class KategorieOptionsWindow : Window
    {
        BasicRepository rep;
        public KategorieOptionsWindow()
        {
            InitializeComponent();
        }
        public KategorieOptionsWindow(BasicRepository repo)
        {
            InitializeComponent();
            rep = repo;
            LocatedKategories = rep.DoQueryCommand<Kategorie>(SQLStatementProvider.)
        }

        private void CancelKategories_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SaveKategories_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
