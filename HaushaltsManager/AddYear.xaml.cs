using HaushaltsManager.DBCreator;
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
        DBCreator.DBCreator dbcreator;
        int selectedYear;
        public AddYear()
        {
            InitializeComponent();
            dbcreator = DBCreator.DBCreator.GetInstance("Years", "DbFiles", string.Empty, ".db");
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            selectedYear = Convert.ToInt32(Yearselector.Text);
            dbcreator.Constring = ConstringAllocator.Years;
            dbcreator.YearSQL = dbcreator.YearSQL.Replace("nnnn",Yearselector.Text);
            dbcreator.CreateTable(dbcreator.YearSQL);
            this.Close();
        }

        public bool SaveEnabled { get; set; } = false;
        public int SelectedYear { get { return selectedYear; } set { selectedYear = value; if (selectedYear != 0) SaveButton.IsEnabled = true; } }
    }
}
