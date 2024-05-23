using HaushaltsManager.DBCreator;
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
    /// Interaktionslogik für AddYear.xaml
    /// </summary>
    public partial class AddYear : Window
    {
        BasicRepository repo;
        public AddYear()
        {
            InitializeComponent();
            repo = new(ConstringAllocator.Years); 
        }

        public AddYear(BasicRepository rep)
        {
            InitializeComponent();
            repo = rep;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            repo.DoNonQueryCommand(SQLStatementProvider.InsertYear.Replace("@Year", Yearselector.Text));
            this.Close();
        }
    }
}
