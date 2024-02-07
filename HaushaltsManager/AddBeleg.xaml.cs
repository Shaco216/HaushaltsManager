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
    /// Interaktionslogik für AddBeleg.xaml
    /// </summary>
    public partial class AddBeleg : Window
    {
        private readonly BasicRepository rep;

        public AddBeleg()
        {
            InitializeComponent();
        }
        public AddBeleg(BasicRepository repo, string jahr)
        {
            InitializeComponent();
            rep = repo;
        }

        private void BelegSave_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BelegCancel_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
