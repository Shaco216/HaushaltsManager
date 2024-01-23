using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;

namespace HaushaltsManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DBCreator.DBCreator creator;
        const string filename = "Years";
        const string lastFilename = "DBFiles";
        string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        const string filetype = ".db";

        public MainWindow()
        {
            InitializeComponent();
            creator = DBCreator.DBCreator.GetInstance("Years","DbFiles",string.Empty,".db");
            creator.Constring = System.IO.Path.Combine(path,lastFilename,filename,filetype);
            creator.CreateDBFile();
        }

        private void CreateYear_Click(object sender, RoutedEventArgs e)
        {

            creator.CreateTable(creator.YearSQL="");
        }
    }
}