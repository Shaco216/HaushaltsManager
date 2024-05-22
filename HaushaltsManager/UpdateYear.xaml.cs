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
    /// Interaktionslogik für UpdateYear.xaml
    /// </summary>
    public partial class UpdateYear : Window
    {
        private readonly BasicRepository repo;
        private readonly string previousyear;

        public UpdateYear()
        {
            InitializeComponent();
        }
        public UpdateYear(BasicRepository rep, string prevYear)
        {
            InitializeComponent();
            repo = rep;
            previousyear = prevYear;
            PrevYear.Text = prevYear;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            string newyear = Yearselector.Text;
            string sqlcmd = SQLStatementProvider.UpdateYear
                    .Replace("@PrevYear", previousyear)
                    .Replace("@Year", newyear);
            repo.DoNonQueryCommand( sqlcmd );
            string belegsql = SQLStatementProvider.SelectBelegeFromYear
                .Replace("@Year", previousyear);
            IEnumerable<Beleg> belege = repo.DoQueryCommand<Beleg>(belegsql);
            foreach (Beleg beleg in belege)
            {
                string updatesql = SQLStatementProvider.UpdateBelegbyYear.Replace("@Id",beleg.Id.ToString())
                    .Replace("@Year",newyear);
                int rowsUpdated = repo.DoNonQueryCommand(updatesql);
            }
            this.Close();
        }
    }
}
