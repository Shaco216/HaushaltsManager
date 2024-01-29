﻿using System.Text;
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
using System.Windows.Controls.Primitives;
using System.Xml.Linq;
using HaushaltsManager.Repository;

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
        const string filetype = "db";
        BasicRepository rep;

        public MainWindow()
        {
            InitializeComponent();
            creator = DBCreator.DBCreator.GetInstance(filename,lastFilename,string.Empty,filetype);
            creator.Constring = System.IO.Path.Combine(path,lastFilename,filename+"."+filetype);
            creator.CreateDBFile();
            rep = new(creator.Constring);
        }

        private void CreateYear_Click(object sender, RoutedEventArgs e)
        {
            AddYear addYear = new AddYear();
            addYear.Title = "Neues Jahr hinzufügen";
            addYear.Width = 300;
            addYear.Height = 200;
            addYear.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            addYear.ShowDialog();
        }

        private void EditYear_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DeleteYear_Click(object sender, RoutedEventArgs e)
        {

        }
        private void UpdateItemSource()
        {
            ClickedYear.ItemsSource = SELECT name FROM sqlite_master WHERE type = "table";
        }
    }
}